using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {


    public int currentHealth = 5; //The current health of the game object

    public bool isDead = false; //Bool for id the object is dead

    [HideInInspector]
    public CharAnimations charAnimations; //The characters animations

    public bool isDamaged = false; //Bool for if the object is damaged


    public Material deathMaterial;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    public float deathFadeTime = 1.0f; //Fading for shader



    private void Awake() {
		charAnimations = this.gameObject.GetComponent<CharAnimations>(); //Get a reference to the character animations
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

    }


	//Return if the object is dead
	public bool Dead() {
        return isDead;
	}

    //Setter for the dead bool
    public void Dead(bool dead) {
        isDead = dead;
	}


    //Returns the objects current health
    public int GetHealth() {
        return currentHealth;
	}


    //Sets the health of the object
    public void SetHealth(int newHealth) {
        currentHealth = newHealth;
	}


    public void CheckDamage() {
        if(currentHealth <= 0) {
            isDamaged = false;
            isDead = true; //Set the object to dead
        }

        if(isDamaged == true) {
            currentHealth -= 1; //Remove 1 from the health
            //charAnimations.DamagedAnimation(true); //Set the damaged animation
            charAnimations.DamagedAnimation();
            isDamaged = false;

        }
	}


    public IEnumerator HealthCheck() {

        for (;;) {

            CheckDamage();

            if (isDead && deathFadeTime > 0) {
                spriteRenderer.material = deathMaterial;


                while (deathFadeTime > 0) {
                    deathFadeTime -= Time.deltaTime;
                    deathMaterial.SetFloat("_Fade", deathFadeTime);
                    yield return null;
                }

                deathFadeTime = 0;
                this.gameObject.SetActive(false);

            }


            yield return new WaitForSeconds(0.1f);

        }
    }

	private void Start() {
        StartCoroutine(HealthCheck());
	}




	private void Update() {
        //CheckDamage();
		
	}


}
