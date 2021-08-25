using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour{



    public Transform[] backgrounds; //Array of backgrounds (background midground foreground) to be parallaxed
    private float[] parallaxScales; //proportion of camera movement to move each from
    public float smoothing = 0.15f; //how smooth parallax will move

    private Transform mainCamera; //reference to the main camera's transform
    private Vector3 previousCamPos; //position of camera in previous frame'



    //Awake is called before start but after all objects are all set up
	private void Awake() {
        mainCamera = Camera.main.transform; //sets reference to main camera



	}



	// Start is called before the first frame update
	void Start(){
        previousCamPos = mainCamera.position; //starting out, sets the previous camera position to start at the main camera position


        parallaxScales = new float[backgrounds.Length]; //sets the amount of items of the parallaxScales to the item amount in backgrounds


        for (int i = 0; i < backgrounds.Length; i++) { //i = 0 and while i is less than backgrounds.Length, add 1 to i


            parallaxScales[i] = backgrounds[i].position.z * -1; // you want it multiplied by -1 because ultimately, the value needs to be negative to work
            //properly. The farther away, the less the parallaxing needs to be.



		}


    }

    // Update is called once per frame
    void Update(){


        for(int i = 0; i < backgrounds.Length; i++) { //i = 0 and while i is less than backgrounds.Length, add 1 to i

            // you want the parallax to be the difference(how much has moved) * by the value in the parallaxScales array
            float parallax = (previousCamPos.x - mainCamera.position.x) * parallaxScales[i];


            // set a target x position which is the current position plus the parallax
            float targetBackgroundPosX = backgrounds[i].position.x + parallax; //taking parallax and storing it to a position

            // create a targer position which is the background's current position with it's target x position
            Vector3 targetBackgroundPos = new Vector3(targetBackgroundPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between current pos and the target pos using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, targetBackgroundPos, smoothing * Time.deltaTime); 
            //delta time converts frames to seconds
        }

        //set the previous camera position to the camera position at end of frame
        previousCamPos = mainCamera.position;

        
    }
}
