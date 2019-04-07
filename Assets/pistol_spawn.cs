using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistol_spawn : MonoBehaviour{
	public float rotationSpeed = 1.0f;
	public bool isVisible = true;
	
	//Borrowed from the ThrowObject example from the "Sound Effects & Scripting" tutorial
	private AudioSource source;
	public AudioClip ammoPickupSound;
	
	public float respawnTimerFINAL = 15.0f;
	public float respawnTimer = 15.0f;
	public int ammoToGive = 10;
	
	public int currentWeapon = 0;
	
	// When the game starts
	void Start(){
		source = GetComponent<AudioSource>();
		
		if(transform.name.Contains("Pistol")){
			respawnTimerFINAL = 15.0f;
			respawnTimer = 15.0f;
			ammoToGive = 10;
			currentWeapon = 0;
		}
		if(transform.name.Contains("Shotgun")){
			respawnTimerFINAL = 15.0f;
			respawnTimer = 15.0f;
			ammoToGive = 10;
			currentWeapon = 1;
		}
		else if(transform.name.Contains("Machine Gun")){
			respawnTimerFINAL = 15.0f;
			respawnTimer = 15.0f;
			ammoToGive = 25;
			currentWeapon = 2;
		}
	}
	
    // Update is called once per frame
    void FixedUpdate(){
		if(!isVisible){
			respawnTimer-=Time.deltaTime;
			if(respawnTimer<=0.0f){
				respawnTimer = respawnTimerFINAL;
				transform.gameObject.GetComponent<Renderer>().enabled = true;
				transform.gameObject.GetComponent<BoxCollider>().enabled = true;
				isVisible = true;
			}
		}
		
		transform.Rotate(0, 0, (transform.rotation.z + rotationSpeed), Space.Self);
    }
	
	private void OnTriggerEnter(Collider col){
		if(col.gameObject.transform.name.Contains("Player")){
			if((col.gameObject.GetComponent<Character_Controller>().listOfAmmo[currentWeapon] + ammoToGive) < col.gameObject.GetComponent<Character_Controller>().listOfAmmoMax[currentWeapon]){
				col.gameObject.GetComponent<Character_Controller>().listOfAmmo[currentWeapon] += ammoToGive;
				transform.gameObject.GetComponent<Renderer>().enabled = false;
				transform.gameObject.GetComponent<BoxCollider>().enabled = false;
				isVisible = false;
				
				source.PlayOneShot(ammoPickupSound, 1.0f);
			}
			else if(col.gameObject.GetComponent<Character_Controller>().listOfAmmo[currentWeapon] == col.gameObject.GetComponent<Character_Controller>().listOfAmmoMax[currentWeapon]){
				//Do nothing.
			}
			else{
				col.gameObject.GetComponent<Character_Controller>().listOfAmmo[currentWeapon] = col.gameObject.GetComponent<Character_Controller>().listOfAmmoMax[currentWeapon];
				transform.gameObject.GetComponent<Renderer>().enabled = false;
				transform.gameObject.GetComponent<BoxCollider>().enabled = false;
				isVisible = false;
				
				source.PlayOneShot(ammoPickupSound, 1.0f);
			}
		}
		
		
		
	}
}
