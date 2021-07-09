using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//Purpose:  
public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenu;

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
            DisplayPauseMenu();
        }
    }

    public void DisplayPauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0; // Stop time while pause menu is active
    }
}
