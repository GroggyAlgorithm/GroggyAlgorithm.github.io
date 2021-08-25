using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(SpriteRenderer))] //Makes sure there is always a sprite renderer
public class TilingBG : MonoBehaviour{

    public int offsetX = 2;

    // Use the check if tile needs instantiated to left or right
    public bool hasTileRight = false;
    public bool hasTileLeft = false;

    //used if object is not tileable
    public bool reverseScale = false;


    // Width of our Element
    private float spriteWidth = 0f;



    private Camera mainCamera;

    private Transform t; 


	private void Awake() {
        mainCamera = Camera.main;

        t = transform;

	}



	// Start is called before the first frame update
	void Start(){

        SpriteRenderer spriterenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriterenderer.sprite.bounds.size.x;
        //Instantiate(t);

    }

    // Update is called once per frame
    void Update(){
        if (hasTileRight == false || hasTileLeft == false) { //Does it need tiles on sides?
            //Calculates half of the cameras view width(extend) in world coordinates 
            float camHorizontalExtend = mainCamera.orthographicSize * Screen.width / Screen.height;
            //calculate x pos of where the camera can see the edge of sprite
            float edgeVisiblePosRight = (t.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePosLeft = (t.position.x - spriteWidth / 2) + camHorizontalExtend;


            // Checking if edge can be seen and calling make new position function
            if (mainCamera.transform.position.x >= edgeVisiblePosRight - offsetX && hasTileRight == false) {

                MakeNewTile(1);
                hasTileRight = true;

			}
            else if (mainCamera.transform.position.x <= edgeVisiblePosLeft + offsetX && hasTileLeft == false) {
                MakeNewTile(-1);
                hasTileLeft = true;


			}


		}


    }


    void MakeNewTile (int Direction) { //Function that creates new tile on side required
        // Direction, negative = left but positive = right

        // Calculates new position for the new tile
        Vector3 newPos = new Vector3 (t.position.x + spriteWidth * Direction, t.position.y, t.position.z);

        // Instantiating new tile and storing in variable
        Transform newTile = Instantiate(t,newPos,t.rotation) as Transform; // as Transform can also be put in parenthesis before like this (Transform)

        //if Not tileable(mirrorable) reverse the x axis of the object
        if (reverseScale == true) {
            newTile.localScale = new Vector3(newTile.localScale.x*-1, newTile.localScale.y,newTile.localScale.z);
		}

        newTile.parent = t.parent; //parent the object 

        if (Direction > 0) {
            newTile.GetComponent<TilingBG>().hasTileLeft = true;

		}
		else {
            newTile.GetComponent<TilingBG>().hasTileRight = true;
		}

	}








}
