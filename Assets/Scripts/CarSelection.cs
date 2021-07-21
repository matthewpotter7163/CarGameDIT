using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    
    public Button r32Button;
    public Button roadsterButton;
    public Button backButton;

    public string carSelection; 

    // Start is called before the first frame update
    void Start()
    {
        Component[] buttonComponents;
        buttonComponents = GetComponentsInChildren<Button>();
        r32Button = buttonComponents[0].GetComponent<Button>();
        roadsterButton = buttonComponents[1].GetComponent<Button>();
        backButton = buttonComponents[2].GetComponent<Button>();
        

        // Add a listener for a click event for each button
        r32Button.onClick.AddListener(delegate { Debug.Log("r32"); goToTrackSelection(); carSelection = "r32";  });
        roadsterButton.onClick.AddListener(delegate { Debug.Log("roadster"); goToTrackSelection(); carSelection = "roadster"; });
        backButton.onClick.AddListener(delegate { Debug.Log("Back"); backToMenu(); });
        
    }

    private void backToMenu() 
    {
        SceneManager.LoadScene("0StartScene", LoadSceneMode.Single);
    }

    private void goToTrackSelection() 
    {
        SceneManager.LoadScene("TrackSelection", LoadSceneMode.Single);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
