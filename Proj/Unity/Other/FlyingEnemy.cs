using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour {

    private HurtScript hurtScript;
    public GameObject hurtBox;

    public Camera mainCamera;   //Unsure if we'll use this here or, more likely, in the game manager
    Vector3 cameraPos;         // Same as above, more likely will use in game manager or camera manager
    private Animator animatorController; //Reference to the animator controller

    public Collider2D hurtCollider;
    public Collider2D attackCollider;

    private PlayerMovement playerMovement;

    private bool playerNear = false;

    private float startingPointx;
    private float startingPointy;


    public GameObject deathPoof;
    private Pathfinding.AIDestinationSetter aiDestinationSetter;


    public Transform target;
    public float speed = 700f;
    public float nextWaypoint = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedPathEnd = false;
    Seeker seeker;


    //private starPath path;
    private GameObject player1;
    public AIPath aIPath;
    IAstarAI ai;










    private GameManager gameManager;

    public int Health = 1;
    public bool isHurt = false;
    private float time;
    private bool isDead = false;

    public float movementTime = 5; //Time it takes object to move in seconds
    public float inverseMoveTime; //Reciprical of move time can be used to make movement more effecient
    public float jumpHeight = 10f; //How high moving object can jump, public so can be edited
                                   //public float gravityScale = 1.5f; //Gravity


    //public LayerMask groundLayer; //Layer that ground is on

    public LayerMask interactionsLayer; //Layer interactions happen on

    private bool facingRight = true; // true if object is facing right. may use in the inheriting objects




    private float moveDirection = 0; //move direction float to determine the way sprite should be facing

    private Rigidbody2D r2d; //2d rigid body item attached to object
    private Collider2D mainCollider; // Collider2d item attached to object, NOTE- may change to boxCollider and do circle collider variable as well
    private BoxCollider2D boxCollider; //boxCollider attacheted to item
    private Transform t; //Varaible for transform

    private GameObject player;

    // Start is called before the first frame update
    void Awake() {

        t = transform;
        r2d = GetComponent<Rigidbody2D>();

        animatorController = GetComponent<Animator>();

        hurtScript = hurtBox.GetComponent<HurtScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;

        // mainCollider = GetComponent<Collider2D>();

        // //boxCollider = GetComponent<BoxCollider2D>();

        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        // //r2d.gravityScale = gravityScale;
        // facingRight = t.localScale.x > 0;

        hurtScript.Health = Health;

        startingPointx = r2d.position.x;
        startingPointy = r2d.position.y;







    }


    private void Start() {
        seeker = GetComponent<Seeker>();
        r2d = GetComponent<Rigidbody2D>();


        InvokeRepeating("UpdatePath", 0f, 0.5f);


    }


    void UpdatePath() {
        if (seeker.IsDone() && playerNear == true) {
            seeker.StartPath(r2d.position, target.position, PathComplete);
        }
        else if (seeker.IsDone() && playerNear == false) {
            seeker.StartPath(r2d.position, (new Vector3(startingPointx, startingPointy, 0)), PathComplete);
        }
        else if (seeker.IsDone() && playerNear == true && target.position.y < r2d.position.y) {
            seeker.StartPath(r2d.position, (new Vector3(r2d.position.x, r2d.position.y + 3, 0)), PathComplete);

        }
    }



    void PathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }



    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log("Something is in area");

        if (collision.gameObject.tag == "Player") {
            target = collision.gameObject.transform;
        }


        if (collision.gameObject.transform == target) {
           // Debug.Log("Player in bounds");
            animatorController.SetTrigger("HurtTrigger");
            //playerNear = true;
            Invoke("PlayerNear", 0.6f);
            Attack();

        }

        //      if(collision.tag == "Enemies") {
        //          //Hurt();
        //}



    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.transform == target) {
            animatorController.SetTrigger("DiveOverTrigger");
            playerNear = false;
            speed = 700f;
        }
    }






    void Hurt() {
        if (hurtScript.Health <= 0) {
            Instantiate(deathPoof, t.position, Quaternion.identity);
            DisableObject();
            isDead = true;
            animatorController.SetTrigger("DeathTrigger");
            
        }
    }



    void DisableObject() {
        gameObject.SetActive(false);
    }



    void PlayerNear() {
        if (target.position.y < r2d.position.y) {
            playerNear = true;
        }
        //if (playerNear == false) {
        //	playerNear = true;
        //}
        //else {
        //	playerNear = false;
        //}
    }


    void Attack() {
        //if(playerNear == true && target.position.y < r2d.position.y) {
        //if(playerNear == true) {
        animatorController.SetTrigger("DiveTrigger");
        speed = 3000f;
        //}
    }

    void StopAttack() {
        if (r2d.position.y < target.position.y + 2) {
            animatorController.SetTrigger("DiveOverTrigger");
            speed = 700f;
        }
    }


    // Update is called once per frame
    void FixedUpdate() {

        

        if (path == null) {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count) {
            reachedPathEnd = true;
            return;
        }
        else {
            reachedPathEnd = false;
        }

        StopAttack();

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - r2d.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        //if (playerNear == true) {
        r2d.AddForce(force);
        //}
        //else {
        //          currentWaypoint = startingPoint;
        //          r2d.AddForce(force);
        //}
        float distance = Vector2.Distance(r2d.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypoint) {
            currentWaypoint++;
        }

        if (r2d.velocity.x >= 0.01f) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (r2d.velocity.x <= -0.01f) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }


        //      if (hurtScript.HurtTrigger == true) {
        //          Hurt();
        //}


            Hurt();


        //      if (playerMovement.jump == true) {
        //          Hurt();
        //}


    }
}