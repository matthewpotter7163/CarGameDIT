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
    public TMP_InputField formInput;
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
        nameInput = inputTextComponents[0].GetComponent<TMP_InputField>(); // Get user name input field
        formInput = inputTextComponents[1].GetComponent<TMP_InputField>(); // Get form room input field

        Component[] buttonComponents = GetComponentsInChildren<Button>(); 
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
        if (nameInput.text.Length < 32 /*&& ValidateHomeroom()*/ ) { }
        dataManager.SaveData(nameInput.text, playerTime , formInput.text, "/scoreboard.data");
        SceneManager.LoadScene("1ScoreboardScene", LoadSceneMode.Single);
    }

    /*
    private bool ValidateHomeroom() {
        if (formInput.text[0] == "M" || formInput.text[0] == "B" || formInput.text[0] == "P" || formInput.text[0] == "J" || formInput.text[0] == "B") {
            bool firstLetterVal = true;
        }

        if (firstLetterVal && formInput.text.Length <= 4) {
            bool HomeroomVal = true;
        }

        return HomeroomVal;
    }*/
}
