using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EffectObjects : MonoBehaviour {

    public float activeTime = 0.25f; //The amount of time the object is active
    public bool setCustomActiveTime = false; //If true, the active time is the publicly set active time
    private Animator animator; //The connected animator
    //public AnimationClip animationClip;


	private void Awake() {

        animator = this.GetComponent<Animator>(); //get reference to the animator
       var clips = animator.runtimeAnimatorController.animationClips; //Get a reference to the clip

        //If the bool is set to false...
        if (setCustomActiveTime == false) {
            //activeTime = animationClip.length + 0.1f;
            activeTime = clips[0].length + 0.01f; // set the active time to slightly more than the clip length
            //Debug.Log("Clip " + clips[0].name + " has set the destory time to " + activeTime); //Debugging message
        }
	}


    // Update is called once per frame
    void Update() {
        Destroy(this.gameObject, activeTime); //Destroy this game object after the specified time
    }



}
