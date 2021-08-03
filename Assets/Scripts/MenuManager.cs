using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//Purpose: to manage game pause menu
public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenu; // Declare gameObject for pause menu

    // Start is called before the first frame update
    void Awake()
    {
        // Find and assign object then hide the menu
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        // Open pause menu using 'esc' key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("esc pressed");
            DisplayPauseMenu();
        }
    }

    public void DisplayPauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0; // Stop time while pause menu is active
    }
}
