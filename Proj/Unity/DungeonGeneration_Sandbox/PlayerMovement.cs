using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MovingObjects2D {
	
	Rigidbody2D rb2D; //The rigidbody 2d component attachted to this object
	[HideInInspector]
	public float horizontal = 0; //The horizontal input
	[HideInInspector]
	public float vertical = 0; //The vertical input
	private Transform t; //The transform attachted to this object
	

	private void Awake() {
		t = this.transform; //set the transform variable
		rb2D = this.GetComponent<Rigidbody2D>(); //set the rigid body 2d variable
		

		rb2D.gravityScale = 0; //set the gravity scale to 0
		rb2D.freezeRotation = true; //freeze rotation so it doesn't spin out of control
		
	}

	private void Update() {

		//Get the input on the x and y axis
		//horizontal = Input.GetAxisRaw("Horizontal");
		//vertical = Input.GetAxisRaw("Vertical");


		//if (horizontal == -1) {
		//	isFacingRight = false;
		//}
		//else if (horizontal == 1) {
		//	isFacingRight = true;
		//}


		//FlipX();
	}

	private void FixedUpdate() {
		//Use rigidbody 2D as the physics movement
		MoveRB2D(horizontal,vertical, rb2D);
	}





	//Returns the velocity of the object
	public Vector2 GetVelocity() {
		return rb2D.velocity;
	}


}
