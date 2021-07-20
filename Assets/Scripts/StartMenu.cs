using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Button startButton;
    public Button leaderboardButton;
    public Button quitButton;
    // Start is called before the first frame update
    void Start()
    {
        Component[] buttonComponents;
        buttonComponents = GetComponentsInChildren<Button>();
        startButton = buttonComponents[0].GetComponent<Button>();
        leaderboardButton = buttonComponents[1].GetComponent<Button>();
        quitButton = buttonComponents[2].GetComponent<Button>();

        // Add a listener for a click event for each button
        startButton.onClick.AddListener(delegate { Debug.Log("Start"); StartGame(); });
        leaderboardButton.onClick.AddListener(delegate { Debug.Log("Leaderboard"); /*StartGame();*/ });
        quitButton.onClick.AddListener(delegate { Debug.Log("Quit"); Application.Quit(); });
    }

    // Update is called once per frame
    private void StartGame()
    {
        SceneManager.LoadScene("CarSelection", LoadSceneMode.Single);
    }
}
