using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomiseMusic : MonoBehaviour
{
    public bool randomPlay = false; // checkbox for random play
    public AudioClip[] clips;
    private AudioSource audioSource;
    int clipOrder = 0; // for ordered playlist
    public bool isPlaying = false;
    private bool musicStatusReceive = true;

    private bool nextSongReceive = false;
    private bool nextSongHappened = false;
    private bool prevSongReceive = false;
    private bool prevSongHappened = false;

    private AudioClip currentSong;
    private AudioClip prevSong;

    private StartMenu startMenu;

    private Button nextButton;
    private Button prevButton;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;

        startMenu = GameObject.Find("Canvas").GetComponent<StartMenu>();

        nextButton = startMenu.nextSongButton;
        prevButton = startMenu.previousSongButton;

        

    }

    void Update()
    {
        musicStatusReceive = startMenu.pauseMusicSend;
        nextSongReceive =startMenu.nextSongSend;
        prevSongReceive = startMenu.prevSongSend;

       



        if (!audioSource.isPlaying)
        {

            

            if (musicStatusReceive == false) {

                nextSongReceive = false;
                prevSongReceive = false;
                // if random play is selected

                if (currentSong == null)
                {

                    if (randomPlay == true)
                    {
                        audioSource.clip = GetRandomClip();
                        audioSource.Play();



                        // if random play is not selected
                    }

                    else
                    {
                        audioSource.clip = GetNextClip();
                        audioSource.Play();
                    }
                }

                else {
                    audioSource.clip = currentSong;
                    audioSource.Play();
                }
             
            }
          
        }

        else if (audioSource.isPlaying) {

            if (musicStatusReceive == true)
            {
                currentSong = audioSource.clip;
                audioSource.Pause();
                Debug.Log("Pause");
            }

            while (nextSongReceive == true) 
            {
                
                prevSong = audioSource.clip;
                audioSource.clip = GetNextClip();
                
                audioSource.Play();
                Debug.Log("next");
                nextSongReceive = !nextSongReceive;
            }

            if (prevSongReceive == true) 
            {
                prevSongReceive = false;
                audioSource.clip = prevSong;
                
                audioSource.Play();
                Debug.Log("prev");
                
                
            }
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

    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);
    }
}
