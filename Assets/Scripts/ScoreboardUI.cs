using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Display Scoreboard
public class ScoreboardUI : MonoBehaviour
{
    public TextMeshProUGUI rankText, nameText, scoreText;
    public Button mainMenuButton, clearButton;
    private ScoreboardDataManager sbDataManager;
    private string displayScore = "";
    private string scoreStr;

    private ArrayList carList = new ArrayList();
    private ArrayList TrackList = new ArrayList();


    private void Start()
    {
        Component[] textComponents = GetComponentsInChildren<TextMeshProUGUI>(); // store all text in an array
        rankText = textComponents[0].GetComponent<TextMeshProUGUI>();
        nameText = textComponents[1].GetComponent<TextMeshProUGUI>();
        scoreText = textComponents[2].GetComponent<TextMeshProUGUI>();

        Component[] buttonComponents = GetComponentsInChildren<Button>(); // store all buttons in an array
        //mainMenuButton = buttonComponents[0].GetComponent<Button>();
        //clearButton = buttonComponents[1].GetComponent<Button>();


        sbDataManager = FindObjectOfType<ScoreboardDataManager>(); // set reference to dataManager
        mainMenuButton.onClick.AddListener(delegate { CloseScoreboard(); });
        clearButton.onClick.AddListener(delegate { ClearScoreboard(); });
        SetupBoard();

        carList.Add("R32");
        carList.Add("Roadster");
        TrackList.Add("Forest");
        TrackList.Add("City");

        
    }

    private void CloseScoreboard()
    {
        SceneManager.LoadScene("0StartScene", LoadSceneMode.Single);
    }

    private void ClearScoreboard()
    {
        sbDataManager.DeleteFile("/scoreboard.data");
        SetupBoard();
    }


    private void SetupBoard()
    {
        rankText.text = "";
        nameText.text = "";
        scoreText.text = "";
        List<ScoreboardEntry> tempDataList = new List<ScoreboardEntry>();
        tempDataList = sbDataManager.LoadData("/scoreboard.data");



        for (int i = 0; i < tempDataList.Count; i++)
        {
            rankText.text = rankText.text + (i + 1).ToString() + "\n";

            nameText.text = nameText.text + $"({TrackList[TrackSelection.trackSelection]})" + $"({carList[CarSelection.carSelection]})" + tempDataList[i].name + "\n";
            



            int _finalTime = tempDataList[i].score;
            Debug.Log($"Final time: {_finalTime}");

            int finalTimeLength = _finalTime.ToString().Length;
            int missingZero = 7 - finalTimeLength;
            string finalTimeZero;
            string finalTimeString = _finalTime.ToString();

            for (int j = 0; j < missingZero; j++) {
                finalTimeZero = "0" + finalTimeString;
                finalTimeString = finalTimeZero;
            }

            Debug.Log(finalTimeString);
            displayScore = "";
            displayScore = AddColons(finalTimeString);
            Debug.Log(displayScore);
            scoreText.text = displayScore + "\n";
        }
    }

    string AddColons(string str) {
        string retString;
        retString = str[0] + str[1] + ":" + str[2] + str[3] + ":" + str[4] + str[5] + str[6];
        return retString;
    }

    ArrayList GetTimeArray(int num)
    {
        List<int> listOfInts = new List<int>();
        while (num > 0)
        {
            listOfInts.Add(num % 10);
            num = num / 10;
        }
        listOfInts.Reverse();
        ArrayList returnList = new ArrayList(listOfInts);
        return returnList;
    }


    /*
    private string DisplayTime(int _time) {
        string _timeOut;

        if ((_time.ToString().Length) <= 5)
        {
            scoreStr = _time.ToString();
            _timeOut = (scoreStr[0] + scoreStr[1] + ":" + scoreStr[2] + scoreStr[3] + scoreStr[4]);
            
        }

        else if ((tempDataList[i].score.ToString().Length) == 6)
        {
            scoreStr = _time.ToString();
            _timeOut = (scoreStr[0] + ":" + scoreStr[1] + scoreStr[2] + ":" + scoreStr[3] + scoreStr[4] + scoreStr[5]);
            
        }
        return _timeOut;
    }*/

}
