using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    
    public Transform targetTransform;
    public Transform cameraTransform;
    public float CameraFollowSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, targetTransform.position, Time.deltaTime * CameraFollowSpeed);
    }




}
