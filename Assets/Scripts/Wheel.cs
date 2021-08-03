using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wheel : MonoBehaviour
{

    public TextMeshProUGUI speed; // Onscreen text for speed
    public TextMeshProUGUI RPM; // Onscreen text for RPM
    public TextMeshProUGUI Torque;
    public TextMeshProUGUI stopwatch; // On screen text for timer
    private float lapTime = 0.0f; // variable for laptime 
    

    [SerializeField] private Rigidbody rb;

    public bool wheelFL; // Front Left wheel
    public bool wheelFR; // Front right wheel
    public bool wheelRL; // Rear Left wheel
    public bool wheelRR; // Rear Right wheel

    // Declare variables for suspension 
    [Header("Suspension")]
    public float restLength;
    public float springTravel;
    public float springStiffness;
    public float damperStiffness;

    private float minLength;
    private float maxLength;
    private float lastLength;
    private float springLength;
    private float springVelocity;
    private float springForce;
    private float damperForce;

    private Vector3 suspensionForce;
    private Vector3 wheelVelocityLS; // Local Space

    // Declare variables for forces
    [SerializeField] private float Fh; // Horizontal Force
    [SerializeField] private float Fv; // Vertical Force
    [SerializeField] private float verticalAxisInput;

    // Declare variables for Wheel
    [Header("Wheel")]
    public float wheelRadius;
    private float wheelAngle;
    public float steerAngle;
    public float steerTime;

    // Declare variables for the engine like torque and horsepower (note: not all of these are being used currently, but may be used in the future)
    [Header("Torque/HP")]
    public float mMaxHorsePower = 276;
    public float mMaxPowerRpm = 6250;
    public float mP1Ratio = 1.0f; // 1 for gasoline, 0.6 for indirect injection diesel, 0.87 for direct injection diesel
    public float mP2Ratio = 1.0f; // 1 for gasoline, 1.4 for indirect injection diesel, 1.13 for direct injection diesel
    public float mP3Ratio = 1.0f; // 1 for most engines
    public float mIdleRpm = 950;
    public float mIdleThrottleFactor = 0.1f;
    public float mRedLineRpm = 7250;
    public float mRedLineThrottleFactor = 0.0f;

    private float mHpWattConvertingConstant = 745.699872f;
    private float mEngineMaxPower;
    private float mEngineMaxPowerAngularVelocity;
    private float mP1;                              // Engine Torque and Power Equation parameter 1
    private float mP2;                              // Engine Torque and Power Equation parameter 2
    private float mP3;                              // Engine Torque and Power Equation parameter 3
    private float mThrottleFactor = 0; // 0 = no throttle, 1 = full throttle

    [SerializeField] private float mAngularVelocity;
    [SerializeField] private AnimationCurve torqueCurve; // graph of torque vs rpm
    [SerializeField] private float engineRPM; // value for rpm of engine


    private float RPMtoRadsValue = 0.10472f; // Value to convert from RPM to rads/s

    private double engineTorque; // Returned Value from Torque Curve
    private float engineTorqueFloat;

    private float fhMultiplicationFactor = 0.03f; // Value to reduce overall horizontal force in the forward direction
    private float fhMultiplicationFactorReverse = 0.03f; // Value to reduce overall horizontal force in the backwards direction

    public Transform wheelTransform;
    [SerializeField] private float wheelSpeed;

    



    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();

        minLength = restLength - springTravel;
        maxLength = restLength + springTravel;

        mEngineMaxPower = mMaxHorsePower * mHpWattConvertingConstant;
        mEngineMaxPowerAngularVelocity = (mMaxPowerRpm * RPMtoRadsValue);
        mP1 = mP1Ratio * (mEngineMaxPower / mEngineMaxPowerAngularVelocity);
        mP2 = mP2Ratio * (mEngineMaxPower / Mathf.Pow(mEngineMaxPowerAngularVelocity, 2));
        mP3 = -mP3Ratio * (mEngineMaxPower / Mathf.Pow(mEngineMaxPowerAngularVelocity, 3));

        mAngularVelocity = mIdleRpm * RPMtoRadsValue;

        

        
    }


    private void Update()
    {
        wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, steerTime * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);

        Debug.DrawRay(transform.position, -transform.up * springLength, Color.green);
        speed.text = (Math.Round((rb.velocity.magnitude * 3.6), MidpointRounding.ToEven).ToString());
        RPM.text = "RPM: " + engineRPM;
        Torque.text = "Torque: " + engineTorque;

        //lapTime = LapTimer(lapTime, startCollider);



    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLength + wheelRadius))
        {
            lastLength = springLength;
            springLength = hit.distance - wheelRadius;
            springLength = Mathf.Clamp(springLength, minLength, maxLength);
            springVelocity = (lastLength - springLength) / Time.deltaTime;
            springForce = springStiffness * (restLength - springLength);
            damperForce = damperStiffness * springVelocity;

            suspensionForce = (springForce + damperForce) * transform.up;

            wheelVelocityLS = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point));

            verticalAxisInput = Input.GetAxis("Vertical");

            engineTorqueFloat = GetEngineTorque();

            if (verticalAxisInput > 0)
            {
                Fh = (springForce * engineTorqueFloat * verticalAxisInput) * fhMultiplicationFactor;

                if ((rb.velocity.magnitude * 3.6) > 251.0f)
                {
                    Fh = 0.0f;
                }
            }

            else if (verticalAxisInput < 0)
            {
                Fh = (springForce * engineTorqueFloat * verticalAxisInput) * fhMultiplicationFactorReverse;
            }
            else
            {
                Fh = 0f;
            }

            Fv = wheelVelocityLS.x * springForce;

            rb.AddForceAtPosition(suspensionForce + (Fh * transform.forward) + (Fv * -transform.right), hit.point);
            wheelSpeed = WheelRotationSpeed();
            wheelTransform.Rotate(0.0f, 0.0f, wheelSpeed, Space.Self);
        }
    }



    /// <summary>
    /// Returns the current engine torque, based on the engine's current angular velocity of the engine and the throttle factor.
    /// </summary>
    /// <returns></returns>
    public float GetEngineTorque()
    {
        // Engine Idle Throttle
        if (mAngularVelocity <= (mIdleRpm * RPMtoRadsValue) && mThrottleFactor < mIdleThrottleFactor)
        {
            mThrottleFactor = mIdleThrottleFactor;
        }

        // Engine Red Line
        if (mAngularVelocity >= (mRedLineRpm * RPMtoRadsValue) && mThrottleFactor > mRedLineThrottleFactor)
        {
            mThrottleFactor = mRedLineThrottleFactor;
        }

        float maxEngineTorque;
        maxEngineTorque = GetMaxEngineTorque();
        if (maxEngineTorque < 0)
        {
            maxEngineTorque = 0;
        }
        //GUIDebugger.AddDebugLine("Max Engine Torque", maxEngineTorque);
        //GUIDebugger.AddDebugLine("Throttle Factor", mThrottleFactor);

        return maxEngineTorque * mThrottleFactor;
    }


    public float GetMaxEngineTorque()
    {
        // Engine Torque Formula
        // Te = Pe / We = P1 + P2*We + P3*We^2
        // Te = Torque Engine
        // Pe = Power Engine
        // We = Angular Velocity Engine
        // P1..P3 = Constants
        return mP1 + mP2 * mAngularVelocity + mP3 * Mathf.Pow(mAngularVelocity, 2);
    }

    private float WheelRotationSpeed()
    {
        float wheelRotSpeed;
        wheelRotSpeed = (rb.velocity.magnitude / (0.2f * 2 * (float)Math.PI));
        return wheelRotSpeed;
    }



    
}


