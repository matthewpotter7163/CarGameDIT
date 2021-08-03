using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Purpose: Select car to play with based on button press
public class CarSelection : MonoBehaviour
{
    //Declare buttons for each car and back button
    public Button r32Button;
    public Button roadsterButton;
    public Button backButton;

    public static int carSelection; // variable for car selection


    // Start is called before the first frame update
    void Start()
    {
        // get button components
        Component[] buttonComponents;
        buttonComponents = GetComponentsInChildren<Button>();
        r32Button = buttonComponents[0].GetComponent<Button>();
        roadsterButton = buttonComponents[1].GetComponent<Button>();
        backButton = buttonComponents[2].GetComponent<Button>();
        

        // Add a listener for a click event for each button
        r32Button.onClick.AddListener(delegate { Debug.Log("r32"); goToTrackSelection(); carSelection = 0;  }); // select r32
        roadsterButton.onClick.AddListener(delegate { Debug.Log("roadster"); goToTrackSelection(); carSelection = 1; }); // select roadster
        backButton.onClick.AddListener(delegate { Debug.Log("Back"); backToMenu(); }); // go back to menu
        
    }

    // load menu scene
    private void backToMenu() 
    {
        SceneManager.LoadScene("0StartScene", LoadSceneMode.Single);
    }
    // load strack selection scene
    private void goToTrackSelection() 
    {
        SceneManager.LoadScene("TrackSelection", LoadSceneMode.Single);
    }
    
}
