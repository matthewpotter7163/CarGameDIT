using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    void Update()
    {
        musicStatusReceive = GameObject.Find("Canvas").GetComponent<StartMenu>().pauseMusicSend;
        nextSongReceive = GameObject.Find("Canvas").GetComponent<StartMenu>().nextSongSend;
        prevSongReceive = GameObject.Find("Canvas").GetComponent<StartMenu>().prevSongSend;

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
                nextSongReceive = false;
                prevSong = audioSource.clip;
                audioSource.clip = GetNextClip();
                
                audioSource.Play();
                Debug.Log("next");
                
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
