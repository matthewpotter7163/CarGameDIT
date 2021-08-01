using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

//Purpose: Get Information via UI from the user (Name & Score)
public class ScoreEntry : MonoBehaviour
{
    // declare datamanager
    private ScoreboardDataManager dataManager;
    // declare UI elements
    public TMP_Dropdown optionDropdown;
    public TMP_InputField nameInput;
    public Button submitButton;
    public GameObject ScoreEntryPanel;

    //declare variable for time
    private int playerTime;
    //declare object to find time on
    public GameObject timerObject;
    

    // Start is called before the first frame update
    void Start()
    {

        Component[] inputTextComponents = GetComponentsInChildren<TMP_InputField>(); // Get the input text as a child
        nameInput = inputTextComponents[0].GetComponent<TMP_InputField>(); // Get the button text as a child

        Component[] buttonComponents = GetComponentsInChildren<Button>(); ;
        submitButton = buttonComponents[0].GetComponent<Button>(); // store all the buttons in an array

        submitButton.onClick.AddListener(delegate { Submit(); });

        dataManager = FindObjectOfType<ScoreboardDataManager>();

        ScoreEntryPanel = gameObject;

        ScoreEntryPanel.SetActive(false);

        
    }

    //Update playerTime variable based on the the time displayed in timer element
    void Update() {

        playerTime = GameObject.Find("Timer").GetComponent<Timer>().finalTime;
        
    }

    void Submit()
    {
        // SaveData takes the players score and a file name e.g. "/filename.dat"
        dataManager.SaveData(nameInput.text, playerTime , "/scoreboard.data");
        SceneManager.LoadScene("1ScoreboardScene", LoadSceneMode.Single);
    }
}
