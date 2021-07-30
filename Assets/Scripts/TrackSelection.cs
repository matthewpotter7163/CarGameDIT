using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrackSelection : MonoBehaviour
{

    public Button cityButton;
    public Button forestButton;
    public Button backButton;

    public static int trackSelection = 2;
    // Start is called before the first frame update
    void Start()
    {
        Component[] buttonComponents;
        buttonComponents = GetComponentsInChildren<Button>();
        cityButton = buttonComponents[0].GetComponent<Button>();
        forestButton = buttonComponents[1].GetComponent<Button>();
        backButton = buttonComponents[2].GetComponent<Button>();


        // Add a listener for a click event for each button
        cityButton.onClick.AddListener(delegate { Debug.Log("city"); goToCityScene(); });
        forestButton.onClick.AddListener(delegate { Debug.Log("forest"); goToForestScene(); });
        backButton.onClick.AddListener(delegate { Debug.Log("Back"); backToMenu(); });

    }

    private void backToMenu()
    {
        SceneManager.LoadScene("0StartScene", LoadSceneMode.Single);
    }

    private void goToForestScene()
    {
        SceneManager.LoadScene("ForestTrack", LoadSceneMode.Single);
        trackSelection = 0;
    }

    private void goToCityScene()
    {
        SceneManager.LoadScene("CityTrack", LoadSceneMode.Single);
        trackSelection = 1;
    }



    // Update is called once per frame
    void Update()
    {

    }
}
