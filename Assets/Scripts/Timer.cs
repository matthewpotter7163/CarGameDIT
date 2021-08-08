using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// Purpose: Script to get time of lap
public class Timer : MonoBehaviour
{
    // declare timer text 
    public TextMeshProUGUI timerText;
    // declare game time variable for timer to base on
    private float gameTime;
    public float timerSpeed = 1.0f; // declare time multiplier for debug purposes
    // Declare controllers for both cars
    
    public CarController r32Controller;

    //public ArrayList lapTimes = new ArrayList();

    bool r32Active = false;
    

    bool timerStatusR32 = false;
    
    // Declare game object to enter score into
    public GameObject scoreEnter;
    // Declare variable for time at end of lap to share with other classes
    public int finalTime;
    // Declare variables for parts of timer
    private int minInt;
    private int secInt;
    private int milInt;

    /*
    public static readonly Dictionary<int, bool> carActiveDict = new Dictionary<int, bool>() {
        {0, r32Active}, {1, roadsterActive}
    };
    
    public static readonly Dictionary<int, bool> carTimerDict = new Dictionary<int, bool>() {
        {0, timerStatusR32}, {1, timerStatusRoadster}
    };*/


    // Start is called before the first frame update
    void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>(); // Find timerText on awake
    }

    void Start()
    {
        // find score entry object
        scoreEnter = GameObject.Find("ScoreEntryUI");
        // find active car
        int activeCar = CarSelection.carSelection;

        if (activeCar == 0)
        {
            r32Active = true;
        }
        /*
        else if (activeCar == 1)
        {
            roadsterActive = true;
        }*/

        //roadsterController = GameObject.Find("Roadster").GetComponent<CarControllerRoadster>();
        r32Controller = GameObject.Find("R32 GTR").GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        // if game is over, set score enter to true
        bool _gameOver = GameObject.Find("R32 GTR").GetComponent<CarController>().gameOver;
        if (_gameOver == true)
        {
            scoreEnter.SetActive(true);
        }

        timerStatusR32 = r32Controller.timerOn;
        //timerStatusRoadster = roadsterController.timerOnRoadster;

        TimerStatus(); // Run function to get time

    }
    // Function to get time 
    private void TimerStatus()
    {

        
        if (r32Active )
        {


            if (timerStatusR32 )
            {
                // game time = deltatime for gameplay purposes 
                gameTime += Time.deltaTime * timerSpeed;

                // Calculate minutes
                float minFloat = Mathf.Floor((gameTime % 3600) / 60);
                string minutes = minFloat.ToString("00");
                minInt = (int)Mathf.Round(minFloat);
                // Calculate Seconds
                float secFloat = Mathf.Floor((gameTime % 60));
                string seconds = secFloat.ToString("00");
                secInt = (int)Mathf.Round(secFloat);
                // Calculate Milliseconds
                float milFloat = Mathf.Floor((gameTime * 1000) % 1000);
                string milliseconds = milFloat.ToString("000");
                milInt = (int)Mathf.Round(milFloat);
                // Create final time by adding all of the part together
                finalTime = int.Parse(minInt.ToString() + secInt.ToString() + milInt.ToString());
                Debug.Log("finaltime:" + finalTime);
                // Display text in timer on screen
                timerText.text = ($"{minutes}:{seconds}:{milliseconds}");
            }
        }
    }

    /*
Debug.Log("r32 status:" + r32Active);
if (r32Active == true)
{


    if (timerStatusR32 == true)
    {
        gameTime += Time.deltaTime * timerSpeed;

        float minFloat = Mathf.Floor((gameTime % 3600) / 60);
        string minutes = minFloat.ToString("00");
        minInt = (int)Mathf.Round(minFloat);

        float secFloat = Mathf.Floor((gameTime % 60));
        string seconds = secFloat.ToString("00");
        secInt = (int)Mathf.Round(secFloat);

        float milFloat = Mathf.Floor((gameTime * 1000) % 1000);
        string milliseconds = milFloat.ToString("000");
        milInt = (int)Mathf.Round(milFloat);

        finalTime = int.Parse(minInt.ToString() + secInt.ToString() + milInt.ToString());
        Debug.Log("finaltime:" + finalTime);

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

else if (r32Active == false)
{
    Debug.Log("r32 is not active");
}


else if (roadsterActive == true)
{


    if (timerStatusRoadster == true)
    {
        gameTime += Time.deltaTime * timerSpeed;
        string minutes = Mathf.Floor((gameTime % 3600) / 60).ToString("00");
        string seconds = Mathf.Floor((gameTime % 60)).ToString("00");
        string milliseconds = Mathf.Floor((gameTime * 1000) % 1000).ToString("000");
        timerText.text = ($"{minutes}:{seconds}:{milliseconds}");
    }


    /*if (timerSplit == true)
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

else if (roadsterActive == false)
{
    Debug.Log("roadster is not active");
}
*/
}
