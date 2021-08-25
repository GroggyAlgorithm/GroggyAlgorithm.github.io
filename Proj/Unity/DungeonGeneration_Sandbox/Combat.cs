using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {
    //Variables
    public Transform mainAttackPoint; //The point of attack for the main attack
    public LayerMask attackableLayers; //The layers that can be attacked
    public float mainAttackRange = 2.0f; //The range of effect for the attack
    public float mainAttackCoolDown = 0.3f; //Cool down time for main attack
    public GameObject mainAttackEffect; //The object that controls the main attack 
    [HideInInspector]
    public EffectObjects effects; //The effect objects script attached to the instantiated effect objects
    [HideInInspector]
    public bool canAttack = true; //If object can attack
    [HideInInspector]
    public bool attacking = false;
    public bool doMainAttack = false;
    [HideInInspector]
    public float degrees = 0.0f; //The degrees for the object

    private float rotationZ = 0;
    private bool weaponRotateMaxed = false;


    public int WeaponSpeed = 5;


    public GameObject[] Weapons;


    private void Awake() {
        effects = mainAttackEffect.GetComponent<EffectObjects>(); //Get reference to effect objects script
        degrees = Weapons[0].transform.rotation.z;

    }

	private void Update() {
        //if (Weapons != null) {
        //var layerPos = Weapons[0].GetComponent<SpriteRenderer>();
        //if (this.transform.localScale.x < 0) {
        //    layerPos.sortingOrder = this.gameObject.GetComponent<SpriteRenderer>().sortingOrder - 1;
        //}
        //else {
        //    layerPos.sortingOrder = this.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
        //}

        //Weapons[0].GetComponent<SpriteRenderer>().sortingOrder = layerPos.sortingOrder;





            //var weaponRotation = new Quaternion(Weapons[0].transform.rotation.x, Weapons[0].transform.rotation.y, (this.transform.localScale.x * Weapons[0].transform.rotation.z), Weapons[0].transform.rotation.w);

        //    if (attacking) {
        //        if (canAttack) {
        //            weaponRotation.z += Time.deltaTime;

        //        }
        //        else weaponRotation.z -= Time.deltaTime;
        //    }
        //    else {
        //        weaponRotation.z = -120 * this.transform.localScale.x;

        //    }


        //    Weapons[0].transform.rotation = weaponRotation;
        //}
        //}
    }

	public void MainAttack() {
        if (canAttack) {
            effects.gameObject.transform.localScale = this.gameObject.transform.localScale; //Set the game objects local scale to this game objects local scale

            //Instantiate the main effect animation object.
            var newEffect = Instantiate(mainAttackEffect, mainAttackPoint.position, Quaternion.identity);
            newEffect.transform.parent = this.transform; //Set this as the parent for the spawned effects

            //Create a circler collider detection in the affectable area
            Collider2D[] layersHit = Physics2D.OverlapCircleAll(mainAttackPoint.position, mainAttackRange);

            //Loop through the layers hit on the layer mask
            foreach (var hit in layersHit) {

                if (attackableLayers.ContainsLayer(hit.gameObject.layer) && hit.gameObject != this.gameObject && hit.gameObject != Weapons[0]) {

                    //Get a reference to the hit objects health
                    if (hit.gameObject.GetComponent<Health>() != null) {
                        var hitHealth = hit.gameObject.GetComponent<Health>();
                        hitHealth.isDamaged = true; //Set the damaged bool
                        Debug.Log(hit.gameObject.name + " health " + hitHealth.GetHealth()); //Debug log
                    }
                }
                //Debug.Log("Hit object " + hit.name);

            }
            attacking = true;
        }
    }

    //Overload
    public void MainAttack(float degrees) {
        
        if (canAttack) {
            effects.gameObject.transform.localScale = this.gameObject.transform.localScale; //Set the game objects local scale to this game objects local scale
            //Instantiate the main effect animation object.
            var newEffect = Instantiate(mainAttackEffect, mainAttackPoint.position, Quaternion.identity);
            newEffect.transform.parent = this.transform; //Set this as the parent for the spawned effects

            newEffect.transform.rotation = Quaternion.Euler(Vector3.forward * degrees);

           


            //Create a circler collider detection in the affectable area
            Collider2D[] layersHit = Physics2D.OverlapCircleAll(mainAttackPoint.position, mainAttackRange);

            //Loop through the layers hit on the layer mask
            foreach (var hit in layersHit) {

                if (attackableLayers.ContainsLayer(hit.gameObject.layer) && hit.gameObject != this.gameObject && hit.gameObject != Weapons[0]) {

                    //Get a reference to the hit objects health
                    if (hit.gameObject.GetComponent<Health>() != null) {
                        var hitHealth = hit.gameObject.GetComponent<Health>();
                        hitHealth.isDamaged = true; //Set the damaged bool
                        Debug.Log(hit.gameObject.name + " health " + hitHealth.GetHealth()); //Debug log
                    }
                }
                //Debug.Log("Hit object " + hit.name);

            }
          //  attacking = true;
        }
    }





    public IEnumerator CoolDown() {


        for (; ; ) {

            

            if (doMainAttack) {
                doMainAttack = false;
                
                
                //Weapons[0].transform.rotation = Quaternion.LookRotation(Vector3.forward);
                //Weapons[0].transform.rotation = Quaternion.RotateTowards(Weapons[0].transform.rotation, preRotation);
                //Weapons[0].transform.rotation = Quaternion.RotateTowards(Weapons[0].transform.rotation, new Quaternion(0, 0, 360, 0), 5 * Time.deltaTime);
                MainAttack();
                canAttack = false;

                for (var i = mainAttackCoolDown; i > 0; i -= Time.deltaTime) {
                    if (weaponRotateMaxed == false) {

                        rotationZ -= WeaponSpeed * this.transform.localScale.x;

                        if (rotationZ > 120 || rotationZ < -120) {
                            rotationZ = degrees;
                            weaponRotateMaxed = true;
                            Weapons[0].transform.rotation = Quaternion.Euler(0, 0, degrees);
                        }
                        else {
                            Weapons[0].transform.rotation = Quaternion.Euler(0, 0, rotationZ);
                        }

                    }

                        


                    yield return new WaitForSeconds(0.001f);
				}


                //Weapons[0].transform.rotation = Quaternion.RotateTowards(Weapons[0].transform.rotation, new Quaternion(0, 0, -120, 0), 5 * Time.deltaTime);

                //attacking = false;

                //yield return new WaitForSeconds(mainAttackCoolDown);
                canAttack = true;
                weaponRotateMaxed = false;
            }

            yield return new WaitForSeconds(0.1f); 
        }

	}



}
