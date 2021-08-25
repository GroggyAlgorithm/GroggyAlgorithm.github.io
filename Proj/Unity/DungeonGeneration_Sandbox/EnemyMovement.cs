using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMovement : MovingObjects2D {

    [HideInInspector]
    public Vector2 here = Vector2.zero; //Our current position
    [HideInInspector]
    public Vector2 there = Vector2.zero; //The Targets position
    [HideInInspector]
    public Vector2 range = Vector2.zero; //The range between here and the target
    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;
    [HideInInspector]
    public bool inPersuit = false;
    [HideInInspector]
    public CharAnimations charAnimations; //The char animation script for this object
    [HideInInspector]
    public Combat combat; //The char animation script for this object

    public Transform target;

    [HideInInspector]
    public Rigidbody2D rb2D;


    public float nextWaypoint = 3f;
    bool reachedPathEnd = false;
    Vector3 startingPosition = Vector3.zero; //The starting position of the enemey
    int currentWayPoint = 0;
    private Pathfinding.AIDestinationSetter aIDestinationSetter; //The destination setter from the pathfinding pack
    Seeker seeker; //The seeker script
    Path path; //The path toward the goal


	private void Awake() {
        seeker = this.GetComponent<Seeker>();
        
	}

    // Start is called before the first frame update
    void Start() {
        startingPosition = rb2D.position;
        StartCoroutine(UpdatePath());
    }



    void PathComplete(Path p) {
        if(!p.error) {
            path = p;
            currentWayPoint = 0;
		}
	}

	

    // Update is called once per frame
    void Update() {

        

        if(target != null) {



            if (inPersuit) {

                if (target.transform.position.x < this.transform.position.x) isFacingRight = false;
                else isFacingRight = true;

                FlipX();

                //if (target.transform.position.x > this.transform.position.x) moveDirection.x = 1;
                //else if (target.transform.position.x < this.transform.position.x) moveDirection.x = -1;
                //else {
                //    moveDirection.x = 0;
                //}
                //if (target.transform.position.y >= this.transform.position.y) moveDirection.y = 1;
                //else if (target.transform.position.y < this.transform.position.y) moveDirection.y = -1;
                //else {
                //    moveDirection.y = 0;
                //}
            }
            
        }

    }

    void GetDirection() {
        here = this.transform.position;
        there = target.transform.position;

        there.x -= combat.mainAttackRange;
        there.y -= combat.mainAttackRange;
    }

    void SetDirection() {
        range.x = there.x - here.x;
        range.y = there.y - here.y;

        //degrees = Mathf.Cos(y / x) * Mathf.Rad2Deg;


        if (Mathf.Abs(range.x / 1.25f) > combat.mainAttackRange) {
            if (range.x > 0) {
                moveDirection.x = 1;
            }
            else if (range.x < 0) {
                moveDirection.x = -1;
            }
            else {
                moveDirection.x = 0;
            }
            
        }
        else moveDirection.x = 0;

        if (Mathf.Abs(range.y / 1.25f) > combat.mainAttackRange) {
            if (range.y > 0) moveDirection.y = 1;
            else if (range.y < 0) moveDirection.y = -1;
            else moveDirection.y = 0;
        }
        else moveDirection.y = 0;


        


    }

    public IEnumerator MoveEnemy() {

        for(; ; ) {

            GetDirection();
            SetDirection();
            
            yield return new WaitForSeconds(0.1f);
        }


	}


    public IEnumerator UpdatePath() {

        for (; ; ) {
            if (seeker.IsDone()) {

                if (inPersuit) {
                    seeker.StartPath(rb2D.position, target.position, PathComplete);
                }
                else {
                    seeker.StartPath(rb2D.position, startingPosition, PathComplete);
                }

            }

            yield return new WaitForSeconds(0.5f);
        }

    }



    private void FixedUpdate() {
        if (path == null) return;
        if (currentWayPoint >= path.vectorPath.Count) {
            reachedPathEnd = true;
            return;
        }
        else {
            reachedPathEnd = false;
        }
        
        moveDirection = ((Vector2)path.vectorPath[currentWayPoint] - rb2D.position).normalized;
        
        var distance = Vector2.Distance(rb2D.position, path.vectorPath[currentWayPoint]);
        if(distance < nextWaypoint) {
            currentWayPoint += 1;
		}
  //      else {
  //          moveDirection = Vector3.zero;
		//}

        

        MoveRB2D(moveDirection.x, moveDirection.y, rb2D);
	}









}
