using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : MonoBehaviour {

    private Rigidbody2D r2d; //2d rigid body item attached to object
    private Transform t; //Varaible for transform
    public GameObject hurtBox;
    public GameObject deathPoof;
    private HurtScript hurtScript;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    [HideInInspector] public Vector3 m_Velocity = Vector3.zero;

    [HideInInspector]public bool facingLeft = true; // true if object is facing right. may use in the inheriting objects
    [HideInInspector] public float moveDirection = -1f; //move direction float to determine the way sprite should be facing

    public float movementTime = 5f; //Time it takes object to move in seconds
    public float gravityScale = 3f; //Gravity
    public float startTime = 0f;
    public float endTime = 1.3f;
    public float pauseTime = 2.6f;
    private float objectYLocation;
    private float objectXLocation;
    public bool walk = true;
    [HideInInspector]public bool attack = false;

    public int DamageAmount = 1;
    public int Health = 1;


    public GameObject wallCheck;
    private WallCheck wallCheckScript;

    private Animator animator;

	private void Awake() {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        hurtScript = hurtBox.GetComponent<HurtScript>();
        r2d.gravityScale = gravityScale;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.freezeRotation = true;
        wallCheckScript = wallCheck.GetComponent<WallCheck>();
        animator = GetComponent<Animator>();
	}



	// Start is called before the first frame update
	void Start() {
        hurtScript.Health = Health;
        
    }

    // Update is called once per frame
    void Update() {
        Health = hurtScript.Health;
        Hurt();
        //endTime = Random.Range(0.25f, pauseTime - 0.25f);
        //pauseTime = Random.Range(endTime + 0.25f, endTime * 2);
        if (attack == false) {
            WalkTimer();
        }
        objectXLocation = r2d.position.x;
        objectYLocation = r2d.position.y;
    }





    void Hurt() {
        if (Health <= 0) {
            Instantiate(deathPoof, (new Vector3(objectXLocation, objectYLocation, 0)), Quaternion.identity);
            gameObject.SetActive(false);
        }
    }


	private void FixedUpdate() {
        animator.SetFloat("xVelocity",(Mathf.Abs(r2d.velocity.x)));
        if (walk == true) {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2((movementTime*moveDirection), r2d.velocity.y);
            // And then smoothing it out and applying it to the character
            r2d.velocity = Vector3.SmoothDamp(r2d.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
		else {
            //r2d.velocity = Vector3.zero;
            r2d.velocity = new Vector3(0, r2d.velocity.y, 0);
		}
        Direction();
        transform.localScale = new Vector3((moveDirection*-1), 1, 0);

    }


    void Direction() {
        if (r2d.velocity.x < 0) {
            facingLeft = false;
        }
        else if (r2d.velocity.x > 0) {
            facingLeft = true;
        }


    }
    

    void WalkTimer() {
        startTime += Time.deltaTime;
        if (startTime < endTime) {
            walk = true;
        }
        else if (startTime >= endTime && startTime < (pauseTime)) {
            walk = false;
            
        }
        else if (startTime >= (pauseTime)) {
            moveDirection *= -1;
            startTime = 0;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "InstantDeath") {
            hurtScript.Health -= hurtScript.Health;
        }
    }

	private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "InstantDeath") {
            hurtScript.Health -= hurtScript.Health;
        }
    }


}
