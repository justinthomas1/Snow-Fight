using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_AI : MonoBehaviour{
    //public Transform player;
	public GameObject player;
	public int health = 10;
	public int damage = 10;
	public int speed = 3;
	public int scoreToAdd = 10;
	public int forceToPlayerOnImpact = 700;
	
	// Use for initialization
	void Start(){
		player = GameObject.Find("Player");
		if(transform.name.Contains("Enemy One")){
			damage = 10;
			health = 10;
			speed = 3;
			scoreToAdd = 10;
		}
		else if(transform.name.Contains("Enemy Two")){
			damage = 30;
			health = 20;
			speed = 2;
			scoreToAdd = 30;
		}
		else if(transform.name.Contains("Enemy Three")){
			damage = 50;
			health = 30;
			speed = 1;
			scoreToAdd = 50;
		}
	}
	
	// Update is called once per frame
    void Update(){
        if(health<=0){
			Destroy(gameObject);
			player.GetComponent<Character_Controller>().playerScore += scoreToAdd;
		}
		
		Vector3 whereToAim = new Vector3(player.transform.position.x,
										transform.position.y,
										player.transform.position.z); //Fixes the Y position so the entire shape doesn't rotate that way.
		
		transform.LookAt(whereToAim);
		
		//Just move forward relative to where you're facing (which should be at the player!)
		transform.position += (transform.forward)*Time.deltaTime*speed;
		
		//transform.LookAt(player);
    }
	
 	void OnCollisionEnter(Collision col){
		//If the enemy collides with the player, push them back a bit.
		if(col.gameObject.transform.name.Contains("Player")){
			col.gameObject.GetComponent<Character_Controller>().playerHealth -= damage;
			col.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * forceToPlayerOnImpact);	
		}
		//Otherwise, remove a certain amount of health from the enemy.
		else if(col.gameObject.transform.name.Contains("Snowball")){
			health -= col.gameObject.GetComponent<CreateAndDestroy>().damage;
		}
	}
}
