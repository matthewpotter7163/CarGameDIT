using UnityEngine;
using System.Collections;

// Purpose: Script to follow car with camera
public class CarSmoothFollow : MonoBehaviour
{
	public Rigidbody parentRigidbody; // get rigid body of target car
	public Transform target; // get transform of target car
	// Declare configuration variables for the camera
	public float distance = 20.0f; 
	public float height = 5.0f;
	public float heightDamping = 2.0f;
	public float lookAtHeight = 0.0f;
	public float rotationSnapTime = 0.3F;
	public float distanceSnapTime;
	public float distanceMultiplier;

	// Declare internal variables to get current position, wanted position etc
	private Vector3 lookAtVector;
	private float usedDistance;
	private float wantedRotationAngle;
	private float wantedHeight;

	private float currentRotationAngle;
	private float currentHeight;

	private Quaternion currentRotation;
	private Vector3 wantedPosition;

	private float yVelocity = 0.0F;
	private float zVelocity = 0.0F;

	// On start, look at target 
	void Start()
	{

		lookAtVector = new Vector3(0, lookAtHeight, 0);

	}

	// On update, update position, rotation and look angle to match car
	void FixedUpdate()
	{
		// get target height and current height
		wantedHeight = target.position.y + height;
		currentHeight = transform.position.y;
		// get target rotation and current rotation
		wantedRotationAngle = target.eulerAngles.y;
		currentRotationAngle = transform.eulerAngles.y;

		currentRotationAngle = Mathf.SmoothDampAngle(currentRotationAngle, wantedRotationAngle, ref yVelocity, rotationSnapTime);
		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		// get target position and current position
		wantedPosition = target.position;
		wantedPosition.y = currentHeight;

		usedDistance = Mathf.SmoothDampAngle(usedDistance, distance + (parentRigidbody.velocity.magnitude * distanceMultiplier), ref zVelocity, distanceSnapTime);

		wantedPosition += Quaternion.Euler(0, currentRotationAngle, 0) * new Vector3(0, 0, -usedDistance);

		transform.position = wantedPosition;

		transform.LookAt(target.position + lookAtVector);

	}

}
