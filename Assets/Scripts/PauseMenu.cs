using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//Purpose:  
public class PauseMenu : MonoBehaviour
{
    public Button resumeButton;
    public Button restartButton;
    public Button mainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        Component[] buttonComponents;
        buttonComponents = GetComponentsInChildren<Button>(); // store all the buttons in an array
        resumeButton = buttonComponents[0].GetComponent<Button>(); // Get First Button
        restartButton = buttonComponents[1].GetComponent<Button>(); // Get Second Button
        mainMenuButton = buttonComponents[2].GetComponent<Button>(); // Get Third Button

        // Add a listener for a click event for each button
        resumeButton.onClick.AddListener(delegate { Debug.Log("Resume"); ResumeGame(); });
        restartButton.onClick.AddListener(delegate { Debug.Log("Restart"); RestartGame(); });
        mainMenuButton.onClick.AddListener(delegate { Debug.Log("MainMenu"); LoadMainMenu(); });
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Start time when pause menu active is false
        gameObject.SetActive(false);
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("CityTrack", LoadSceneMode.Single);
    }

    private void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("0StartScene", LoadSceneMode.Single);
    }


    // Update is called once per frame
    void Update()
    {
        // close pause menu using 'esc' key press
        if (gameObject.activeSelf == true && Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }
}
