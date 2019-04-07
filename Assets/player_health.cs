using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_health : MonoBehaviour{
    public Text playerHealth;
	public Text playerAmmo;
	public Text playerScore;
	
	public Text gameOverText;
	
	public Character_Controller controller;

    // Update is called once per frame
    void Update(){
        controller = GetComponent<Character_Controller>();
		
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
		}
    }
}
