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
    void Start()
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

        if (timerStatus == true){
            gameTime += Time.deltaTime * timerSpeed;
            string minutes = Mathf.Floor((gameTime % 3600) / 60).ToString("00");
            string seconds = (gameTime % 60).ToString("00");
            string milliseconds = Mathf.Floor((gameTime * 1000) % 1000).ToString("000");
            timerText.text = ($"{minutes}:{seconds}:{milliseconds}");
        }

        if (timerSplit == true) {
            lapTimes.Add(timerText.text); 
            timerText.text = ("00:00:000");
            timerSplit = false;
            foreach (string laptime in lapTimes) {
                Debug.Log(laptime);
            }

            
            
        }
        
    }
}
