using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : MonoBehaviour {
    private Animator animatorController; //Reference to the animator controller
    public LayerMask groundLayer; //Layer that ground is on
    private Rigidbody2D r2d; //2d rigid body item attached to object
    public Collider2D mainCollider; // Collider2d item attached to object, NOTE- may change to boxCollider and do circle collider variable as well
    private Transform t; //Varaible for transform
    public GameObject hurtBox;
    public GameObject attackBox;
    public GameObject deathPoof;
    private HurtScript hurtScript;
    private DamageScript damageScript;

    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    [HideInInspector] public Vector3 m_Velocity = Vector3.zero;


    private bool facingRight = true; // true if object is facing right. may use in the inheriting objects
    private bool isGrounded = false; //Asks if object is on ground

    private float frogChangeMoveDirection = 1f;
    private int directionCounter = 1; //Make a Range, mathf.random
    private float moveDirection = 0f; //move direction float to determine the way sprite should be facing

    public bool isHurt = false;
    public float movementTime = 5f; //Time it takes object to move in seconds
    //public float jumpHeight = 1.1f; //How high moving object can jump, public so can be edited
    //public float jumpLength = 0f;
    public float gravityScale = 3f; //Gravity
    public float startTime = 0f;
    public float endTime = 1.3f;
    private float coolDownReset;
    private float coolDownTimeStart = 0f;
    private float coolDownTimeEnd = 5f;


    private float objectYLocation;
    private float objectXLocation;


    public int DamageAmount = 1;
    public int Health = 1;



    public float jumpForce = 12f; //Force for knockback
    public float jumpLength = 0.2f; //How long force applied
    public float jumpCount;
    //public bool knockBackLeft; //Direction for knockBack



    // Start is called before the first frame update
    void Start() {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        coolDownReset = coolDownTimeEnd;
        animatorController = GetComponent<Animator>();

        mainCollider = GetComponent<Collider2D>();
        hurtScript = hurtBox.GetComponent<HurtScript>();
        hurtScript.Health += Health;

        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
        //halfTime = endTime / 2;
        
    }

    // Update is called once per frame
    void Update() {
        animatorController.SetFloat("yVelocity", r2d.velocity.y);

        Health = hurtScript.Health;
        MoveDirection();
        Hurt();
        objectXLocation = r2d.position.x;
        objectYLocation = r2d.position.y;
        JumpTimer();
    }

    void MoveDirection() {
        if (moveDirection != 0) {
            if (moveDirection < 0 && !facingRight) {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }

            if (moveDirection > 0 && facingRight) {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }
    }

    void Hurt() {
		if (Health <=0) {
            Instantiate(deathPoof,(new Vector3(objectXLocation,objectYLocation,0)),Quaternion.identity);
            gameObject.SetActive(false);
		}
        if(isHurt == true && Health > 0) {
            //do hurt thing
		}

    }





	private void FixedUpdate() {
        Bounds colliderBounds = mainCollider.bounds;
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, 0.1f, 0);
        isGrounded = Physics2D.OverlapCircle(groundCheckPos, 0.23f, groundLayer);

    }




    void JumpTimer() {


        endTime = jumpLength;

        if (directionCounter > 2) {
            frogChangeMoveDirection = 1f;
        }
        else {
            frogChangeMoveDirection = -1f;
        }

        if (directionCounter >= 6) {
            directionCounter = 0;
        }





        if (coolDownTimeStart < coolDownTimeEnd) {
            coolDownTimeStart += Time.deltaTime;
            startTime = 0f;
            directionCounter++;
        }
        else {
            if (startTime < endTime) {
                startTime += Time.deltaTime;
                moveDirection = frogChangeMoveDirection;
                if (facingRight) {
                    // knockback velocity
                    Vector3 targetVelocity = new Vector2(-jumpForce, jumpForce);
                    // And then smoothing it out and applying it to the character
                    r2d.velocity = Vector3.SmoothDamp(r2d.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
                }
                if (!facingRight) {
                    //knock back velocity
                    Vector3 targetVelocity = new Vector2(jumpForce, jumpForce);
                    // And then smoothing it out and applying it to the character
                    r2d.velocity = Vector3.SmoothDamp(r2d.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
                }

            }
            else {
                moveDirection = 0f;
                coolDownTimeStart = 0f;
            }

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
