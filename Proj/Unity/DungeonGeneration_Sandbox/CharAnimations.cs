using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharAnimations: MonoBehaviour {
	[HideInInspector]
	public Animator animator; //The objects animator
	//private static string currentState = ""; //The currently active animation state

	//Struct of the animationStates with same named parameters
	public struct AnimStates {
		public const string idle = "Idle";
		public const string run = "Run";
		public const string damaged = "Damaged";
		public const string MainAttack = "MainAttack";

	} 

	void Awake() {
		if (animator == null) {
			animator = this.GetComponent<Animator>();
		}

	}



	//Controls the Damaged animation
	public void DamagedAnimation(bool play) {
		if (play == true) {
			animator.SetTrigger(AnimStates.damaged);
		}

	}


	//Controls the Damaged animation
	public void DamagedAnimation() {
		animator.SetTrigger(AnimStates.damaged);
	}


	//Controls the run animation
	public void RunAnimation(float movement) {
		if (movement != 0) {
			animator.SetBool("Run", true);
		}
		else {
			animator.SetBool("Run", false);
		}
	}



	//Controls the run animation
	public void RunAnimation(float horizontal, float vertical) {
		if (horizontal != 0 || vertical != 0) {
			animator.SetBool("Run", true);
		}
		else {
			animator.SetBool("Run", false);
		}
	}


	//Controls the Main Attack Animation
	public void MainAttackAnimation(bool play) {
		if (play == true) {
			animator.SetTrigger("MainAttack");
		}
	}


	//Controls the sprite direction change on the x axis
	public void FlipX(float xAxis) {

		//Variables
		Vector3 localScale = transform.localScale; //the objects local scale

		// If the input is moving the player right...
		if (xAxis > 0) {
			// ... flip the player.
			localScale.x = 1;
		}
		// else if the input is moving the player left...
		else if (xAxis < 0) {
			// ... flip the player.
			localScale.x = -1;
		}

		transform.localScale = localScale;
	}


}
