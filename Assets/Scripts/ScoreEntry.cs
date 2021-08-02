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

    private bool formVal = false;
    private bool nameVal = false;
    private bool inputValidation = true;

    [SerializeField]
    private GameObject nameErrorText;
    [SerializeField]
    private GameObject formErrorText;


    // Start is called before the first frame update
    void Start()
    {


        nameErrorText = GameObject.Find("NameErrorMessage");
        nameErrorText.SetActive(false);
        formErrorText = GameObject.Find("FormErrorMessage");
        formErrorText.SetActive(false);


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


        formVal = ValidateHomeroom(formInput.text);
        nameVal = CheckName(nameInput.text);

        if (formVal == true && nameVal == true)
        {
            //SaveData takes the players score and a file name to save to e.g. "filename.dat"
            dataManager.SaveData(nameInput.text, playerTime, formInput.text, "/scoreboard.data");
            SceneManager.LoadScene("1ScoreboardScene", LoadSceneMode.Single);
             
        }
        else if (formVal == true && nameVal == false)
        {
            nameErrorText.SetActive(true);
            formErrorText.SetActive(false);
            Debug.Log("Invalid name");
        }
        else if (formVal == false && nameVal == true)
        {
            formErrorText.SetActive(true);
            nameErrorText.SetActive(false);
            Debug.Log("Invalid homeroom");
        }
        else if (formVal == false && nameVal == false)
        {
            nameErrorText.SetActive(true);
            formErrorText.SetActive(true);
            Debug.Log("Invalid name and form");
        }
    }


    private bool ValidateHomeroom(string formInputVal) {
        bool firstLetterVal = false;
        formInputVal = formInputVal.ToUpper();

        if ((formInputVal[0] == 'M' || formInputVal[0] == 'B' || formInputVal[0] == 'P' || formInputVal[0] == 'J' || formInputVal[0] == 'B') && formInputVal.Length <= 4) {
            firstLetterVal = true;
        }

        else {
            firstLetterVal = false;
        }

        

        return firstLetterVal;
    }

    //Checking for a valid name input
    private bool CheckName(string playerName)
    {
        bool isValid;
        

        //Check that name input is empty or less than 12 characters long (Boundary check)
        if (playerName.Length < 12)
        {
            isValid = true;
        }
        else
        {
            isValid = false;
        }

        //If the name input is blank substitute it for a dash on the scoreboard to avoid confusion of user
        if (playerName == "")
        {
            isValid = false;
        }

        return isValid;
    }

}
