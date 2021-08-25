using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    [HideInInspector] public Vector3 m_Velocity = Vector3.zero;
    private Rigidbody2D r2d;
    private Transform t;
    private int health = 1;
    //private Collider2D collider;
    public float bulletForce = 12f;
    //private Animator animatorController;
    public bool facingLeft = true;


    public GameObject hurtBox;
    private HurtScript hurtScript;

    public GameObject deathPoof;

    private float gravityScale = 1;

	private void Awake() {
        r2d = GetComponent<Rigidbody2D>();
        t = GetComponent<Transform>();
        hurtScript = GetComponent<HurtScript>();
	}

	// Start is called before the first frame update
	void Start(){
        //animatorController = GetComponent<Animator>();
        r2d.freezeRotation = true;
        r2d.constraints = RigidbodyConstraints2D.FreezePositionY;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        facingLeft = t.localScale.x > 0;
        r2d.gravityScale = gravityScale;
        hurtScript = hurtBox.GetComponent<HurtScript>();
        hurtScript.Health = health;
        //health = hurtScript.Health;
        hurtScript.onlyPlayerHurtsThis = false;
        SpriteDirection();
    }



    void Hurt() {
        if (hurtScript.Health <= 0) {
            Instantiate(deathPoof, (new Vector3(r2d.position.x, r2d.position.y, 0)), Quaternion.identity);
            Destroy(gameObject);
		}
	}




	private void Update() {
        Hurt();
	}



	// Update is called once per frame
	void FixedUpdate(){
        if (facingLeft == true) {
            //var flipX = t.localScale.x;
            //flipX *= -1;
            Vector3 targetVelocity = new Vector2(bulletForce *-1, r2d.velocity.y);
            // And then smoothing it out and applying it to the character
            r2d.velocity = Vector3.SmoothDamp(r2d.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
		else {
			Vector3 targetVelocity = new Vector2(bulletForce, r2d.velocity.y);
            // And then smoothing it out and applying it to the character
            r2d.velocity = Vector3.SmoothDamp(r2d.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }

    }


    void SpriteDirection() {
        if(facingLeft == false) {
            var flip = t.localScale;
            flip.x *= -1;
            t.localScale = flip;
        }
	}

}
