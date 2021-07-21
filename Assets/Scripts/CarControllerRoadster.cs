using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading;

public class CarControllerRoadster : MonoBehaviour
{
    //yes

    public RoadsterWheel[] wheels;

    [Header("Forces")]
    private float Flong;
    private float Ftraction;
    private float Fdrag;
    private float Frr;

    [Header("Car Specs")]
    public float wheelBase;  // in meters
    public float rearTrack;  // in meters
    public float turnRadius; // in meters 

    [Header("Inputs")]
    public float steerInput;

    [SerializeField] private float ackermannAngleLeft;
    [SerializeField] private float ackermannAngleRight;

    public bool timerOnRoadster = false;
    public bool timerSplitRoadster = false;
    public int lapCountRoadster = 0;

    private float lapTimeRoadster = 0.0f;
    private bool checkpoint1Roadster = false;

    public bool roadsterIsActive = false;

    private void Start()
    {
        lapTimeRoadster = 0.0f;

        if (this.isActiveAndEnabled)
        {
            roadsterIsActive = true;
            Debug.Log("roadster is active");
        }
    }



    void FixedUpdate()
    {
        steerInput = Input.GetAxis("Horizontal");
        if (steerInput > 0) // is turning right
        {
            ackermannAngleLeft = (Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2)))) * steerInput;
            ackermannAngleRight = (Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2)))) * steerInput;
        }

        else if (steerInput < 0) // is turning left
        {
            ackermannAngleLeft = (Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2)))) * steerInput;
            ackermannAngleRight = (Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2)))) * steerInput;
        }

        else
        {
            ackermannAngleLeft = 0;
            ackermannAngleRight = 0;
        }

        foreach (RoadsterWheel w in wheels)
        {
            if (w.wheelFL)
            {
                w.steerAngle = ackermannAngleLeft;
            }
            if (w.wheelFR)
            {
                w.steerAngle = ackermannAngleRight;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("TimerTrigger"))
        {
            if (checkpoint1Roadster == true)
            {
                timerOnRoadster = false;
                
                //timerSplit = true;

            }

            else
            {
                timerOnRoadster = true;
                checkpoint1Roadster = false;
                lapCountRoadster = 1;
                Debug.Log("timer on");
                
            }

        }

        if (other.CompareTag("checkpoint1Roadster"))
        {
            checkpoint1Roadster = true;
        }

    }
}