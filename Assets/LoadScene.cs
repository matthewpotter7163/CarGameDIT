using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public GameObject[] Cars;
    public GameObject[] Cameras;

    public int carSelectionLoad = 2;

    // Start is called before the first frame update

    private void Awake()
    {

        carSelectionLoad = CarSelection.carSelection;
        Debug.Log(carSelectionLoad + "yes");

        LoadCar(carSelectionLoad);
    }


    void Start()
    {

        


    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void LoadCar(int selection) {
        Cars[selection].gameObject.SetActive(true);
        Cameras[selection].gameObject.SetActive(true);
    }
}
