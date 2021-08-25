using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //Library allows us to query collections

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharAnimations))]
[RequireComponent(typeof(Combat))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(EnemyMovement))]
public class EnemyController : MonoBehaviour {

    [HideInInspector]
    public Health health; //The health script for this object
    [HideInInspector]
    public Combat combat; //Combat script for this object
    [HideInInspector]
    public CharAnimations charAnimations; //The char animation script for this object
    [HideInInspector]
    public EnemyMovement movement; //The enemies movement script


    public Grid WalkableGrid; //The grid that can be walked on

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    public Transform target;

    //public float raycastDistance = 0.025f;
    //private RaycastHit2D rayHitMain;
    //private RaycastHit2D rayHitRight;
    //private RaycastHit2D rayHitLeft;


    //private Vector3 moveDirection = new Vector3();
    public bool inPersuit = false;


    void Awake() {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0;
        rb2D.freezeRotation = true;

        health = this.gameObject.GetComponent<Health>();
        charAnimations = this.gameObject.GetComponent<CharAnimations>();
        health.charAnimations = charAnimations;
        charAnimations.animator = this.gameObject.GetComponent<Animator>();
        combat = this.gameObject.GetComponent<Combat>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        movement = this.gameObject.GetComponent<EnemyMovement>();
        movement.charAnimations = charAnimations;
        movement.combat = combat;
        movement.rb2D = rb2D;

    }

	private void Start() {
        StartCoroutine(combat.CoolDown());
	}



    // Update is called once per frame
    void Update() {

        //SetAttackDirection();
        if (Mathf.Abs(movement.range.y) <= combat.mainAttackRange && Mathf.Abs(movement.range.x) <= combat.mainAttackRange) {
            combat.doMainAttack = true;
        }


    }



    void SetAttackDirection() {

        if (movement.range.y > 0) combat.degrees = 30;
        else if (movement.range.y < 0) combat.degrees = -30;
        else combat.degrees = 0;


        if (Mathf.Abs(movement.range.y) <= combat.mainAttackRange && Mathf.Abs(movement.range.x) <= combat.mainAttackRange) {
            combat.doMainAttack = true;
        }
    }







    private void OnTriggerEnter2D(Collider2D collision) {


        
        //if(collision.gameObject.layer == 12) { 
        if (combat.attackableLayers.ContainsLayer(collision.gameObject.layer)) {
            movement.inPersuit = true;
            movement.target = collision.gameObject.transform;
            //StartCoroutine(movement.MoveEnemy());
            
		}


	}




	private void OnTriggerExit2D(Collider2D collision) {
        if (target != null) {
            if (collision.gameObject.transform == target.transform) {
                //StopCoroutine(movement.MoveEnemy());
                movement.inPersuit = false;
                //movement.moveDirection.x = 0;
                //movement.moveDirection.y = 0;
                //movement.target = this.gameObject.transform;
            }
        }
	}



}
