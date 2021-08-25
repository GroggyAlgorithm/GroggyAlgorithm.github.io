using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjects2D : MonoBehaviour {

	//Variables
    public float movementSpeed = 15.0f; // Movement speed of object
    public float movementLimiter = 0.8f; // Used to limit movement when moving diagnally or otherwise
	public bool isFacingRight = false; //boolean for if object is facing right or no

    
    [SerializeField]
    [Range(0, .5f)] 
    private float MovementSmoothing = .25f;  // How much to smooth out the movement

	[SerializeField]
	[Range(0, .5f)]
	private float DecelerationSmoothing = .25f;  // How much to smooth out the movement


	[HideInInspector] public Vector3 velocity = new Vector3(); //The velocity for this object

	public float GetMovementSmoothing() {

		return MovementSmoothing;

	}

	public float GetDecelerationSmoothing() {

		return DecelerationSmoothing;

	}


	public void MoveRB2D(float horizontal, float vertical, Rigidbody2D rb2D) {

		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed);

		if (horizontal != 0 && vertical != 0) { // if the character is moving diagnally it needs to be slowed down
			targetVelocity *= movementLimiter;
		}


		// And then smoothing it out and applying it to the character
		rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref velocity, MovementSmoothing);
		//m_Rigidbody2D.AddForce(Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing));


		if (horizontal == 0 && vertical == 0) { // If the input is 0, decelerate
			targetVelocity *= 0;
			rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref velocity, DecelerationSmoothing);
		}


	}





	//Controls the sprite direction change on the x axis
	public void FlipX() {

		//Variables
		Vector3 localScale = transform.localScale; //the objects local scale

		// If the input is moving the player right...
		if (isFacingRight) {
			// ... flip the player.
			localScale.x = 1;
		}
		// else if the input is moving the player left...
		else if (!isFacingRight) {
			// ... flip the player.
			localScale.x = -1;
		}

		transform.localScale = localScale;
	}
}
