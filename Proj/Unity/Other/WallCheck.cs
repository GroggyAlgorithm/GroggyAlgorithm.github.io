using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour {

    [HideInInspector] public WalkingEnemy walkingEnemy;
    // Start is called before the first frame update
    void Start() {
        walkingEnemy = GetComponentInParent<WalkingEnemy>();
        
    }

    // Update is called once per frame
    void Update() {
        
    }


	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Groud") {
            walkingEnemy.moveDirection *= -1;
		}
	}



}
