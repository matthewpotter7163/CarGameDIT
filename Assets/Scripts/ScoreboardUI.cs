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
            displayScore = 
            scoreText.text = scoreText.text + tempDataList[i].score.ToString() + "\n";
        }
    }

}
