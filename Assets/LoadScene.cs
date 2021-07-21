using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public GameObject r32;
    public GameObject r32Camera;
    public GameObject roadster;
    public GameObject roadsterCamera;

    public string carLoad;

    // Start is called before the first frame update
    void Start()
    {
       
            
        
    }

    // Update is called once per frame
    void Update()
    {
        carLoad = GameObject.Find("Canvas").GetComponent<CarSelection>().carSelection;

        if (carLoad == "r32")
        {
            r32.SetActive(true);
            r32Camera.SetActive(true);
            roadster.SetActive(false);
            roadsterCamera.SetActive(false);
        }

        else if (carLoad == "roadster")
        {
            roadster.SetActive(true);
            roadsterCamera.SetActive(true);
            r32.SetActive(false);
            r32Camera.SetActive(false);


        }
    }
}
