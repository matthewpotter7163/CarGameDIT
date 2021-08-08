using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomiseMusic : MonoBehaviour
{
    public bool randomPlay = false; // checkbox for random play
    public AudioClip[] clips; // list of audio clips to play
    private AudioSource audioSource; // declare audio source
    int clipOrder = 0; // for ordered playlist
    public bool isPlaying = false; // variable to start/stop music
    private bool musicStatusReceive = true; // declare variable to know whether to pause or play based on input from pause button

    /*
    For skipping between songs, but doesn't work for now

    private bool nextSongReceive = false;
    private bool nextSongHappened = false;
    private bool prevSongReceive = false;
    private bool prevSongHappened = false;
    
    private AudioClip prevSong;
    private Button nextButton;
    private Button prevButton;
    */

    private AudioClip currentSong; // Declare audio clip for currently play song
    private StartMenu startMenu; // Declare start menu to get pause/play

    // On start, find audioSource and start menu
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        startMenu = GameObject.Find("Canvas").GetComponent<StartMenu>();

        //nextButton = startMenu.nextSongButton;
        //prevButton = startMenu.previousSongButton;
    }

    // On update, check for pause/play and pause/play music based on this
    void Update()
    {
        musicStatusReceive = startMenu.pauseMusicSend;
        //nextSongReceive =startMenu.nextSongSend;
        //prevSongReceive = startMenu.prevSongSend;
        
        // If music is not playing, do this
        if (!audioSource.isPlaying)
        {
            // if user presses play
            if (musicStatusReceive == false) {

                //nextSongReceive = false;
                // prevSongReceive = false;
                // if random play is selected

                // if there is no song currently selected to play
                if (currentSong == null)
                {
                    // if randomise playlist is true, find random song to play
                    if (randomPlay == true)
                    {
                        audioSource.clip = GetRandomClip();
                        audioSource.Play();
                        
                    }

                    // if random play is not selected, get next song in list
                    else
                    {
                        audioSource.clip = GetNextClip();
                        audioSource.Play();
                    }
                }
                // if song was already playing, continue playing that song
                else {
                    audioSource.clip = currentSong;
                    audioSource.Play();
                }
             
            }
          
        }

        // if music is playing
        else if (audioSource.isPlaying) {
            // if user presses pause
            if (musicStatusReceive == true)
            {
                // current song = audio clip that is playing now
                currentSong = audioSource.clip;
                audioSource.Pause(); // pause audio
                Debug.Log("Pause");
            }
            /*
            while (nextSongReceive == true) 
            {
                
                prevSong = audioSource.clip;
                audioSource.clip = GetNextClip();
                
                audioSource.Play();
                Debug.Log("next");
                nextSongReceive = !nextSongReceive;
            }*/
            /*
            if (prevSongReceive == true) 
            {
                prevSongReceive = false;
                audioSource.clip = prevSong;
                
                audioSource.Play();
                Debug.Log("prev");
                
                
            }*/
        }

     
    }

    // function to get a random clip
    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    // function to get the next clip in order, then repeat from the beginning of the list.
    private AudioClip GetNextClip()
    {
        if (clipOrder >= clips.Length - 1)
        {
            clipOrder = 0;
        }
        else
        {
            clipOrder += 1;
        }
        return clips[clipOrder];
    }
    // don't destroy audioplayer on load of new scene so that music will keep playing
    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }
}
