using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public static GameManager instance = null; //The instance of this game manager


	private void Awake() {

        //Check if there's only one instance of this game manager
		if(instance == null) {
            instance = this;
		}
        else if(instance != this) {
            Destroy(gameObject);
		}
	}


	// Start is called before the first frame update
	void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }









}
