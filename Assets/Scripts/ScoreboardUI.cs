using System;
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
    private string displayScore;
    private string scoreStr;


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
            nameText.text = nameText.text + tempDataList[i].name + "\n";


            int _finalTime = tempDataList[i].score;
            Debug.Log($"Final time: {_finalTime}");

            if (((_finalTime.ToString()).Length) <= 5)
            {
                scoreStr = _finalTime.ToString();
                displayScore = (scoreStr[0] + scoreStr[1] + ":" + scoreStr[2] + scoreStr[3] + scoreStr[4]);

            }

            else if (((_finalTime.ToString()).Length) == 6)
            {
                scoreStr = _finalTime.ToString();
                displayScore = (scoreStr[0] + ":" + scoreStr[1] + scoreStr[2] + ":" + scoreStr[3] + scoreStr[4] + scoreStr[5]);

            }

            //displayScore = DisplayTime(_finalTime);

            scoreText.text = scoreText.text + displayScore + "\n";
        }
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
