using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour{

    private float Gravity = 1;
    public int Health = 1;
    public bool canShoot = true;
    public bool spawnObjectOnDeath = false;
    public GameObject deathSpawnObject;
    public GameObject spawnObjectLocation;

    private Rigidbody2D r2d; //2d rigid body item attached to object
    //private Collider2D mainCollider; // Collider2d item attached to object, NOTE- may change to boxCollider and do circle collider variable as well
    private Transform t; //Varaible for transform
    private Animator animatorController;
    //public float shootStartTime = 0f;
    //public float shootEndTime = 1f;
    //public float coolDownTimeStart = 0f;
    //public float coolDownTimeEnd = 1f;
    private float bulletSpawnX;
    private float bulletSpawnY;
    public float startTime = 0f;
    public float endTime = 1.3f;

    
    [HideInInspector]public bool shootLeft = false;



    //public GameObject bullet;
    //private GameObject bulletClone;
    //private Bullet bulletScript;


    public GameObject hurtBox;
    public GameObject deathPoof;
    private HurtScript hurtScript;



    // Start is called before the first frame update
    void Start() {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();
		//if (shootLeft == false) {
		//	var flip = t.localScale;
		//	flip.x *= -1;
		//	t.localScale = flip;
		//}
		r2d.freezeRotation = true;
		r2d.constraints = RigidbodyConstraints2D.FreezePositionY;
		//r2d.constraints = RigidbodyConstraints2D.FreezeAll;
		r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        //mainCollider = GetComponent<Collider2D>();
        r2d.gravityScale = Gravity;

        //bullet = GetComponent<GameObject>();
        //bulletScript = bullet.GetComponent<Bullet>();
        hurtScript = hurtBox.GetComponent<HurtScript>();
        hurtScript.Health = Health;
        
    }



    


    void Hurt() {
		if (hurtScript.Health <= 0 && spawnObjectOnDeath == false) {
			Instantiate(deathPoof, (new Vector3(r2d.position.x, r2d.position.y, 0)), Quaternion.identity);
			gameObject.SetActive(false);
		}
        else if (hurtScript.Health <= 0 && spawnObjectOnDeath == true) {
			Instantiate(deathPoof, (new Vector3(spawnObjectLocation.transform.position.x,spawnObjectLocation.transform.position.y ,0)), Quaternion.identity);
			Instantiate(deathSpawnObject, (new Vector3(r2d.position.x, r2d.position.y, 0)), Quaternion.identity);
			gameObject.SetActive(false);
		}




        //if (isHurt == true && Health > 0) {
        //    //do hurt thing
        //}

    }









    








    // Update is called once per frame
    void Update() {
        //if (shootLeft == false) {
        //    var flip = t.localScale;
        //    flip.x *= -1;
        //    t.localScale = flip;
        //}

        Hurt();


    }




    //private void FixedUpdate() {
    //    Bounds colliderBounds = mainCollider.bounds;
    //    Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, 0.1f, 0);
    //    //isGrounded = Physics2D.OverlapCircle(groundCheckPos, 0.23f, groundLayer);

    //}

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
