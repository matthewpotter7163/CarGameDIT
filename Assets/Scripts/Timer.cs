using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    private bool timerStatus;
    public TextMeshProUGUI timerText;
    private float gameTime;
    public float timerSpeed = 1.0f; // for debug purposes
    public GameObject car;

    public ArrayList lapTimes = new ArrayList();
    
    

    // Start is called before the first frame update
    void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        //bool ts = bool.Find("timerOn");

        //bool timerStatus = GameObject.Find("R32 GTR").GetComponent<CarController>().timerOn;

    }

    // Update is called once per frame
    void Update()
    {

        
        bool timerStatus = GameObject.Find("R32 GTR").GetComponent<CarController>().timerOn;
        bool timerSplit = GameObject.Find("R32 GTR").GetComponent<CarController>().timerSplit;

        bool _timerStatusRoadster = GameObject.Find("Roadster").GetComponent<CarControllerRoadster>().timerOnRoadster;
        bool _timerSplitRoadster = GameObject.Find("Roadster").GetComponent<CarControllerRoadster>().timerSplitRoadster;

        bool r32Active = GameObject.Find("R32 GTR").GetComponent<CarController>().r32IsActive;
        bool roadsterActive = GameObject.Find("Roadster").GetComponent<CarControllerRoadster>().roadsterIsActive;

        if (r32Active == true)
        {
            if (timerStatus == true)
            {
                gameTime += Time.deltaTime * timerSpeed;
                string minutes = Mathf.Floor((gameTime % 3600) / 60).ToString("00");
                string seconds = (gameTime % 60).ToString("00");
                string milliseconds = Mathf.Floor((gameTime * 1000) % 1000).ToString("000");
                timerText.text = ($"{minutes}:{seconds}:{milliseconds}");
            }

            if (timerSplit == true)
            {
                lapTimes.Add(timerText.text);
                timerText.text = ("00:00:000");
                timerSplit = false;
                foreach (string laptime in lapTimes)
                {
                    Debug.Log(laptime);
                }



            }
        }

        else if (r32Active == false) {
            Debug.Log("r32 is not active");
        }

        if (roadsterActive == true)
        {

            Debug.Log("Roadster is active");
            if (_timerStatusRoadster == true)
            {
                gameTime += Time.deltaTime * timerSpeed;
                string minutes = Mathf.Floor((gameTime % 3600) / 60).ToString("00");
                string seconds = (gameTime % 60).ToString("00");
                string milliseconds = Mathf.Floor((gameTime * 1000) % 1000).ToString("000");
                timerText.text = ($"{minutes}:{seconds}:{milliseconds}");
            }

            if (_timerSplitRoadster == true)
            {
                lapTimes.Add(timerText.text);
                timerText.text = ("00:00:000");
                _timerSplitRoadster = false;
                foreach (string laptime in lapTimes)
                {
                    Debug.Log(laptime);
                }
            }
        }

        else if (roadsterActive == false)
        {
            Debug.Log("roadster is not active");
        }


    }
}
