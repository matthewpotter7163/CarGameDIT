using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class PlayerMovement : MonoBehaviour
{

    /* public MenuManager menuManager;

    [SerializeField] private float speed;
    [SerializeField] private int score;

    public int Score
    {
        get => score;
    }


    // Start is called before the first frame update
    void Start()
    {
        speed = 10.0f;
        menuManager = FindObjectOfType<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

    }

    public void Movement()
    {
        transform.position = transform.position + new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
    }

    // ontrigger to detect a reward using "Reward" tag
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Reward"))
        {
            score++;
            Debug.Log("Score: " + score);
            Debug.Log("Collide: " + other);
            Destroy(other.gameObject);

        }

        if (other.CompareTag("NPC"))
        {
            menuManager.DisplayScoreEntryUI();
        }

    }

    */
}
