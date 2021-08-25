using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpThrough : MonoBehaviour {
	// Start is called before the first frame update

	public Collider2D colliderToDisable;

	private CharacterController2D playerController;
	private PlayerMovement playerMovement;

	private bool disableCollider = false;
	private int collisionCount = 0;

	private void Update() {
		if (Input.GetButtonDown("Crouch") || Input.GetButtonDown("Jump")) {
			colliderToDisable.isTrigger = true;
		}
		else if (Input.GetButtonUp("Crouch") || Input.GetButtonUp("Jump")) {
			colliderToDisable.isTrigger = false;
		}
		else if (Input.GetButtonDown("Vertical")) {
			colliderToDisable.isTrigger = true;
		}
		//if (collisionCount != 0) {
		//	colliderToDisable.isTrigger = true;
		//}
		//if(Input.GetButtonUp("Jump") || Input.GetButtonUp("Crouch")) {
		//	colliderToDisable.isTrigger = false;
		//}
		//else {
		//	colliderToDisable.isTrigger = false;
		//}
	}





	//private void OnCollisionEnter2D(Collision2D collision) {
	//	if (collision.gameObject.tag == "Player") {
	//		colliderToDisable.isTrigger = true;
	//	}
	//}
	//private void OnCollisionStay2D(Collision2D collision) {
	//	if (collision.gameObject.tag == "Player") {
	//		colliderToDisable.isTrigger = false;
	//	}
	//}




	//private void OnCollisionExit2D(Collision2D collision) {
	//	if (collision.gameObject.tag == "Player") {
	//		colliderToDisable.isTrigger = true;
	//		collisionCount++;

	//	}
	//}


	//private void OnTriggerEnter2D(Collider2D collision) {
	//	if (collision.gameObject.tag == "Player") {
	//		playerController = collision.gameObject.GetComponent<CharacterController2D>();
	//		playerMovement = colliderToDisable.gameObject.GetComponent<PlayerMovement>();
	//		if (playerController.m_Grounded || playerMovement.crouch == true) {
	//			//colliderToDisable.enabled = false;
	//			colliderToDisable.isTrigger = false;
	//		}
	//	}
	//}



	//private void OnCollisionStay2D(Collision2D collision) {
	//	if (colliderToDisable.gameObject.tag == "Player") {
	//		colliderToDisable.isTrigger = false;
	//		collisionCount++;
	//	}
	//}


	//private void OnTriggerStay2D(Collider2D collision) {
	//	if (collision.gameObject.tag == "Player") {
	//		collisionCount++;
	//	}
	//}
	//private void OnTriggerEnter2D(Collider2D collision) {
	//	if(collision.gameObject.tag == "Player") {

	//	}
	//}



	//private void OnTriggerEnter2D(Collider2D collision) {
	//	if (collision.gameObject.tag == "Player") {
	//		colliderToDisable.isTrigger = true;
	//	}
	//}

	//private void OnTriggerExit2D(Collider2D collision) {
	//	if(collision.gameObject.tag == "Player") {
	//		colliderToDisable.isTrigger = false;
	//		collisionCount++;
	//	}
	//}

}
