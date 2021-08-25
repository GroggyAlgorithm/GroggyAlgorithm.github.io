using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharAnimations))]
[RequireComponent(typeof(Combat))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour {

    //Variables
    [HideInInspector]
    public Health health; //The health script for this object
    [HideInInspector]
    public Combat combat; //Combat script for this object
    [HideInInspector]
    public CharAnimations charAnimations; //The char animation script for this object
    private Rigidbody2D rb2D; //The rigidbody 2D for this object
    //private bool mainAttack = false; //boolean for if the main attack is being triggered
    [HideInInspector]
    public PlayerMovement playerMovement; //The movement script for this object


	private void Awake() {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0;
        rb2D.freezeRotation = true;

        health = this.gameObject.GetComponent<Health>();
        charAnimations = this.gameObject.GetComponent<CharAnimations>();
        health.charAnimations = charAnimations;
        charAnimations.animator = this.gameObject.GetComponent<Animator>();
        combat = this.gameObject.GetComponent<Combat>();
        health.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        playerMovement = this.gameObject.GetComponent<PlayerMovement>();
    }


	// Start is called before the first frame update
	void Start() {
        StartCoroutine(combat.CoolDown());



    }

    // Update is called once per frame
    void Update() {
        playerMovement.horizontal = Input.GetAxisRaw("Horizontal");
        playerMovement.vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Fire1")) { combat.doMainAttack = true; }
        //if (Input.GetButtonDown("Fire1")) { combat.MainAttack(); }

        charAnimations.RunAnimation(playerMovement.horizontal, playerMovement.vertical);
        charAnimations.FlipX(playerMovement.horizontal);



    }


	//private void FixedUpdate() {
		
 //       //If the main attack is true, call the combat function.
 //       if (mainAttack) {
 //           combat.MainAttack(); //Since it uses Physics2D, we're calling it in the fixed update for physics calculations
 //           mainAttack = false;
	//	}
	//}



}
