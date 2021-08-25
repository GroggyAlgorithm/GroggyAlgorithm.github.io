using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/**********************************************************************************************************************************************
 Class		public class CameraController : MonoBehaviour
 Abstract	Adds and controls additional functionality to the camera
**********************************************************************************************************************************************/
public class CameraController : MonoBehaviour {

	[SerializeField]
	protected Transform trackingTarget; //The target that the camera will be following

	[SerializeField]
	float xOffset; //Camera offset on the x axis

	[SerializeField]
	float yOffset = 2.0f; //Camera offset on the y axis. Currently offset by 2 on the y axis.

	[SerializeField]
	protected float followSpeed; //The speed the camera will follow with

	[SerializeField]
	protected bool isXLocked = false; //boolean for if the camera is locked on the x axis

	[SerializeField]
	protected bool isYLocked = false; //boolean for if the camera is locked on the y axis


	[SerializeField]
	[Range(0, 5f)] 
	private float MovementSmoothing = .25f;  // How much to smooth out the movement


	[HideInInspector]
	public Vector3 Velocity = Vector3.zero; // Velocity reference


	public float pixelToUnits = 40f; //for converting for camera
	private float smoothingTempHolder = 0; //Temporarily holds the smoothing for the camera

	Vector3 cameraPosition; //The camera position
	private Camera mainCamera; //reference to the main camera







	private void Awake() {

		mainCamera = Camera.main; //On awake, get reference to the main camera

	}


	void Start() {
		smoothingTempHolder = MovementSmoothing; //on start, set the smoothing temp to the movement smoothing
	}



	//Return the smoothing for the camera
	public float GetSmoothing() {

		return MovementSmoothing;

	}


	//Return the camera target
	public Transform GetTarget() {
		return trackingTarget;
	}



	//Sets the tracking target to a new target
	public void SetTarget(Transform newTarget) {
		trackingTarget = newTarget;
	}





	//Function for rounding to the nearest pixel
	public float RoundToNearestPixel(float unityUnits) {
		float valueInPixels = unityUnits * pixelToUnits;
		valueInPixels = Mathf.Round(valueInPixels);
		float roundedUnityUnits = valueInPixels * (1 / pixelToUnits);
		return roundedUnityUnits;
	}

	private void Update() {
		//Update every update frame and set the cameras position to the main cameras viewport point
		cameraPosition = mainCamera.WorldToViewportPoint(trackingTarget.transform.position);
	}



	
	void FixedUpdate() {

		//On the physics update, set variables for the targets x position and y position
		float xTarget = trackingTarget.position.x + xOffset;
		float yTarget = trackingTarget.position.y + yOffset;

		//Get new positions by lerping between cameras position and targets position
		float xNew = Mathf.Lerp(transform.position.x, xTarget, (Time.deltaTime * followSpeed));
		float yNew = Mathf.Lerp(transform.position.y, yTarget, (Time.deltaTime * followSpeed));


		//Set the new target position
		Vector3 targetPosition = new Vector3(RoundToNearestPixel(xTarget), RoundToNearestPixel(yTarget), transform.position.z);




		//Incomplete code, I need to review

		//if(xTarget < (-cameraHeight)) {
		if (cameraPosition.y < 0 || cameraPosition.y > 1 || cameraPosition.x < 0 || cameraPosition.x > 1) {
			//Debug.LogWarning("Camera bounds reached");
			MovementSmoothing /= 2;
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref Velocity, MovementSmoothing);
		}
		else {
			MovementSmoothing = smoothingTempHolder;
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref Velocity, MovementSmoothing);
		}


		//if() {
		//	Debug.LogWarning("Camera x bounds reached");
		//	m_MovementSmoothing = 0;
		//}


		//transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref m_Velocity, m_MovementSmoothing);

	}


}