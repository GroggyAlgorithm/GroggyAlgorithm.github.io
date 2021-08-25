using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableScript : MonoBehaviour{


    public float DisableTime = 4f;


    // Start is called before the first frame update
    void Start(){
        Destroy(this.gameObject, DisableTime);
    }


}
