using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading;

// Class to control forces acting on car object, control the steering angle and start/stop the timer
public class CarController : MonoBehaviour
{
    
    //List for wheel objects
    public Wheel[] wheels;

    [Header("Forces")]
    private float Flong; // Declare Longitudinal force
    private float Ftraction; // Declare Traction force
    private float Fdrag; // Declare Drag force
 

    [Header("Car Specs")]
    public float wheelBase;  // in meters
    public float rearTrack;  // in meters
    public float turnRadius; // in meters 

    [Header("Inputs")]
    public float steerInput; // Declare variable for steering input

    [SerializeField] private float ackermannAngleLeft; // Declare float for steering angle left wheel
    [SerializeField] private float ackermannAngleRight; // Declare float for steering angle right wheel

    public bool timerOn = false; // Declare variable to turn on timer

    //public int lapCount = 0; // Declare variable for lap count

    private float lapTime = 0.0f; // Declare float for lap time

    private bool checkpoint1 = false; // Declare bool to confirm whether checkpoint1 has been hit
    public bool r32IsActive = false; // Decalre bool to confirm whether r32 is active

    public bool gameOver = false; // Declare bool to confirm whether game is over 

    // Check if r32 is enabled on start
    private void Start()
    {
        lapTime = 0.0f;

        if (this.isActiveAndEnabled)
        {
            r32IsActive = true;
            Debug.Log("r32 is active"); 
        }

    }


    // Update for steering angle 
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

        foreach (Wheel w in wheels)
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


            if (checkpoint1 == true)
            {
                timerOn = false;
                gameOver = true;
                Time.timeScale = 0f;
                

                //timerSplit = true;

            }

            else
            {
                timerOn = true;
                checkpoint1 = false;
                lapCount = 1;
                Debug.Log("timer on");
                Debug.Log("lapCount");
            }

        }

        if (other.CompareTag("Checkpoint1"))
        {
            checkpoint1 = true;
        }

    }
}