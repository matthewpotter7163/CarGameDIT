using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Button startButton;
    public Button leaderboardButton;
    public Button quitButton;
    public Button playMusicButton;
    public Button nextSongButton;
    public Button previousSongButton;

    private bool musicStatus;
    private bool pauseMusic;
    private bool playMusic;
    private bool nextSong;
    private bool prevSong;

    public bool pauseMusicSend;
    public bool nextSongSend;
    public bool prevSongSend;

    
    private Image playButtonImage;
    public bool pauseImage = true; //true - show pause image, false - show play image
    public Sprite[] playSprites;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {


        Component[] buttonComponents;
        buttonComponents = GetComponentsInChildren<Button>();
        startButton = buttonComponents[0].GetComponent<Button>();
        leaderboardButton = buttonComponents[1].GetComponent<Button>();
        quitButton = buttonComponents[2].GetComponent<Button>();

        playMusicButton = buttonComponents[3].GetComponent<Button>();
        nextSongButton = buttonComponents[4].GetComponent<Button>();
        previousSongButton = buttonComponents[5].GetComponent<Button>();

        // Add a listener for a click event for each button
        startButton.onClick.AddListener(delegate { Debug.Log("Start"); StartGame(); });
        leaderboardButton.onClick.AddListener(delegate { Debug.Log("Leaderboard"); GoToLeaderboard(); });
        quitButton.onClick.AddListener(delegate { Debug.Log("Quit"); Application.Quit(); });

        playMusicButton.onClick.AddListener(delegate { Debug.Log("play"); pauseMusicSend = PlayMusic(); PlayPauseImage(); });
        nextSongButton.onClick.AddListener(delegate { Debug.Log("next"); nextSongSend = NextSong(); });
        previousSongButton.onClick.AddListener(delegate { Debug.Log("previous"); prevSongSend = PrevSong();});

        playButtonImage = playMusicButton.image;
    }

    private void Update()
    {
        musicStatus = GameObject.Find("MusicPlayer").GetComponent<AudioSource>().isPlaying;
        
    }

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

    // Update is called once per frame
    private void StartGame()
    {
        SceneManager.LoadScene("CarSelection", LoadSceneMode.Single);
    }

    private void GoToLeaderboard() {
        SceneManager.LoadScene("1ScoreboardScene", LoadSceneMode.Single);
    }

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

    private bool NextSong()
    {
        nextSong = true;
        return nextSong;
        
    }

    private bool PrevSong()
    {
        prevSong = true;
        return prevSong;
        
    }
}
