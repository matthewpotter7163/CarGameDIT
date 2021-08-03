using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Declare buttons
    public Button startButton;
    public Button leaderboardButton;
    public Button quitButton;
    public Button playMusicButton;
    public Button nextSongButton;
    public Button previousSongButton;
    // Declare bools for music controls
    private bool musicStatus;
    private bool pauseMusic;
    private bool playMusic;
    private bool nextSong;
    private bool prevSong;
    // Declare bools to send to other classes
    public bool pauseMusicSend;
    public bool nextSongSend;
    public bool prevSongSend;

    
    private Image playButtonImage; // declare image to use for 
    public bool pauseImage = true; //true - show pause image, false - show play image
    public Sprite[] playSprites; // sprites for pause/play button


    // Start is called before the first frame update
    void Start()
    {

        // Get button components for menu
        Component[] buttonComponents;
        buttonComponents = GetComponentsInChildren<Button>();
        startButton = buttonComponents[0].GetComponent<Button>();
        leaderboardButton = buttonComponents[1].GetComponent<Button>();
        quitButton = buttonComponents[2].GetComponent<Button>();
        playMusicButton = buttonComponents[3].GetComponent<Button>();
        
        //nextSongButton = buttonComponents[4].GetComponent<Button>();
        //previousSongButton = buttonComponents[5].GetComponent<Button>();

        // Add a listener for a click event for each button
        startButton.onClick.AddListener(delegate { Debug.Log("Start"); StartGame(); });
        leaderboardButton.onClick.AddListener(delegate { Debug.Log("Leaderboard"); GoToLeaderboard(); });
        quitButton.onClick.AddListener(delegate { Debug.Log("Quit"); Application.Quit(); });
        playMusicButton.onClick.AddListener(delegate { Debug.Log("play"); pauseMusicSend = PlayMusic(); PlayPauseImage(); });
        //nextSongButton.onClick.AddListener(delegate { Debug.Log("next"); nextSongSend = NextSong(); });
        //previousSongButton.onClick.AddListener(delegate { Debug.Log("previous"); prevSongSend = PrevSong();});

        playButtonImage = playMusicButton.image; // Get image 
    }
    // on update check if music is playing or not 
    private void Update()
    {
        musicStatus = GameObject.Find("MusicPlayer").GetComponent<AudioSource>().isPlaying;
        
    }
    // Function to switch image to play or pause based on whether or not music is playing 
    private void PlayPauseImage() {

        pauseImage = !pauseImage;

        if (pauseImage)
        {
            playButtonImage.sprite = playSprites[0];
        }

        else if (pauseImage == false)
        {
            playButtonImage.sprite = playSprites[1];
        }                                                
    }

    // load car selection screen
    private void StartGame()
    {
        SceneManager.LoadScene("CarSelection", LoadSceneMode.Single);
    }
    // load leaderboard scene
    private void GoToLeaderboard() {
        SceneManager.LoadScene("1ScoreboardScene", LoadSceneMode.Single);
    }
    // function for pausing/playing music based on whether or not music is playing
    private bool PlayMusic()
    {

        if (musicStatus == true)
        {
            pauseMusic = true;


        }

        else if (musicStatus == false)
        {
            pauseMusic = false;
        }


        return pauseMusic;
    }
    /*
    private bool NextSong()
    {
        nextSong = true;
        return nextSong;
        
    }

    private bool PrevSong()
    {
        prevSong = true;
        return prevSong;
        
    }*/
}
