using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Purpose: Takes data from ScoreboardDataManager and displays it through the UI system. Also can clear the data.
public class ScoreboardUI : MonoBehaviour
{
    // Declare UI elements
    public TextMeshProUGUI rankText, nameText, scoreText, formText;
    public Button mainMenuButton, clearButton;

    // Declare a reference to the ScoreboardDataManager
    private ScoreboardDataManager sbDataManager;

    //Declare variables to display the score
    private string displayScore = "";
    private string scoreStr;

    //Declare arrays for cars and lists
    private ArrayList carList = new ArrayList();
    private ArrayList trackList = new ArrayList();


    private void Start()
    {
        Component[] textComponents = GetComponentsInChildren<TextMeshProUGUI>(); 
        //get UI elements
        rankText = textComponents[0].GetComponent<TextMeshProUGUI>();
        nameText = textComponents[1].GetComponent<TextMeshProUGUI>();
        scoreText = textComponents[2].GetComponent<TextMeshProUGUI>();
        formText = textComponents[3].GetComponent<TextMeshProUGUI>();


        Component[] buttonComponents = GetComponentsInChildren<Button>(); // store all buttons in an array
        //mainMenuButton = buttonComponents[0].GetComponent<Button>();
        //clearButton = buttonComponents[1].GetComponent<Button>();


        sbDataManager = FindObjectOfType<ScoreboardDataManager>(); // set reference to dataManager

        // Add listeners for button components to close and clear scoreboard
        mainMenuButton.onClick.AddListener(delegate { CloseScoreboard(); });
        clearButton.onClick.AddListener(delegate { ClearScoreboard(); });
        SetupBoard();
        // add cars and tracks to lists 
        carList.Add("R32");
        carList.Add("Roadster");
        trackList.Add("Forest");
        trackList.Add("City");

        
    }

    // Exit scoreboard and Load StartScene
    private void CloseScoreboard()
    {
        SceneManager.LoadScene("0StartScene", LoadSceneMode.Single);
    }

    // Delete the file which data is stored in, so leaderboard is cleared, and setup board again
    private void ClearScoreboard()
    {
        sbDataManager.DeleteFile("/scoreboard.data");
        SetupBoard();
    }

    // Initialise scoreboard with scores from file
    private void SetupBoard()
    {
        rankText.text = "";
        nameText.text = "";
        scoreText.text = "";
        formText.text = "";
        List<ScoreboardEntry> tempDataList = new List<ScoreboardEntry>();
        tempDataList = sbDataManager.LoadData("/scoreboard.data");


        // go through each data point in the list
        for (int i = 0; i < tempDataList.Count; i++)
        {
            // set rank and name text
            rankText.text = rankText.text + (i + 1).ToString() + "\n";
            nameText.text = nameText.text + tempDataList[i].name + "\n";

            int _finalTime = tempDataList[i].score;

            Debug.Log($"Final time: {_finalTime}");

            // add zeroes to start of time for formatting based on initial length of time
            int finalTimeLength = _finalTime.ToString().Length;
            int missingZero = 7 - finalTimeLength;
            string finalTimeZero;
            string finalTimeString = _finalTime.ToString();

            for (int j = 0; j < missingZero; j++) {
                finalTimeZero = "0" + finalTimeString;
                finalTimeString = finalTimeZero;
            }

           // format by adding colons
            displayScore = AddColons(finalTimeString);
            // set form and time text
            scoreText.text = scoreText.text + displayScore + "\n";
            formText.text = formText.text + tempDataList[i].formRoom + "\n";
        }
    }
    // function to add colons to time string
    string AddColons(string str) {
        string retString;
        Debug.Log(str);
        retString = str[0] + "" +  str[1] + ":" + str[2] + str[3] + ":" + str[4] + str[5] + str[6];
        Debug.Log(str[0] + "" + str[1]);
        return retString;
    }


}
