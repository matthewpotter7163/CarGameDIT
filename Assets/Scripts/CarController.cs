using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{

    public Wheel[] wheels;

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

    public bool timerOn = false;
    private float lapTime = 0.0f;

    private void Start()
    {
        lapTime = 0.0f;
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


       /* while (timerOn = true)
        {
            //lapTime += Time.deltaTime;
            Debug.Log("Time +1");
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("TimerTrigger"))
        {
            timerOn = true;
            Debug.Log("timer start");
        }

        else
        {
            timerOn = false;
        }

    }
                                }