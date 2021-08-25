using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimations : MonoBehaviour {

	Animator animator; //The players animator


	void Awake() {
		animator = this.GetComponent<Animator>();
		
	}

    // Update is called once per frame
    void Update() {

		RunAnimation(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		MainAttackAnimation(Input.GetButtonDown("Fire1"));
		

	}


	public void RunAnimation(float horizontal, float vertical) {
		if (horizontal != 0 || vertical != 0) {
			animator.SetBool("Run", true);
		}
		else {
			animator.SetBool("Run", false);
		}
	}


	public void MainAttackAnimation(bool play) {
		if (play == true) {
			animator.SetTrigger("MainAttack");
		}
	}





}
