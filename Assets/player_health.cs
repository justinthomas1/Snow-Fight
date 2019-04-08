using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_health : MonoBehaviour{
    public Text playerHealth;
	public Text playerAmmo;
	public Text playerScore;
	
	public Text gameOverText;
	
	public Image fadeOutImage;
	
	public Character_Controller controller;
	
	public float playerDeadTimeout = 5.0f;

	void Start(){
		controller = GetComponent<Character_Controller>();
	}

    // Update is called once per frame
    void Update(){
		if(!controller.playerDead){
			playerHealth.text = "Health: " + controller.playerHealth;
			playerAmmo.text = "Ammo: " + controller.listOfAmmo[controller.playerCurrentWeapon];
			playerScore.text = "Score: " + controller.playerScore;
			gameOverText.text = "";
		}
		else{
			playerHealth.text = "";
			playerAmmo.text = "";
			playerScore.text = "";
			gameOverText.text = "Game Over!";
			
			Color newColor = fadeOutImage.color;
			
			if(playerDeadTimeout>0.0f){
				playerDeadTimeout-= Time.deltaTime;
			}
			else if(newColor.a<255.0f){
				newColor.a +=  Time.deltaTime;
				fadeOutImage.color = newColor;
			}
			
			if(newColor.a>=255.0f){
				Debug.Log("Got here.");
				Application.LoadLevel(0);
			}
		}
    }
}