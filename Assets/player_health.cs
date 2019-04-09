using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_health : MonoBehaviour{
    public Text playerHealth;
	public Text playerAmmo;
	public Text playerScore;
	public Text playerTimer;
	
	public int minutes = 2;
	public int seconds = 0;
	public string secondsString = "00";
	public float secondTimer = 1.0f;
	
	public Text gameOverText;
	
	public Image fadeOutImage;
	
	public Character_Controller controller;
	public CameraStuff camStuff;
	
	public float playerDeadTimeout = 5.0f;

	void Start(){
		controller = GetComponent<Character_Controller>();
		camStuff = GetComponent<CameraStuff>();
	}

    // Update is called once per frame
    void Update(){
		if(!controller.playerDead){
			playerHealth.text = "Health: " + controller.playerHealth;
			playerAmmo.text = "Ammo: " + controller.listOfAmmo[controller.playerCurrentWeapon];
			playerScore.text = "Score: " + controller.playerScore;
			gameOverText.text = "";
			if(map_settings.TimeAttack){
				playerTimer.text = minutes + ":" + secondsString;
				
				secondTimer-= Time.deltaTime;
				if(secondTimer<=0.0f){
					if((seconds-1 < 10) && seconds>0){
						seconds--;
						secondsString = "0" + seconds.ToString();
						secondTimer = 1.0f;
					}
					else if(seconds==0){
						if(minutes>0){
							minutes--;
							seconds = 59;
							secondsString = seconds.ToString();
							secondTimer = 1.0f;
						}
						else{
							controller.playerDead = true;
							camStuff.playerDead = true;
							Destroy(controller.GetComponent<BoxCollider>());
							Destroy(controller.GetComponent<Rigidbody>());
						}
					}
					else{
						seconds--;
						secondsString = seconds.ToString();
						secondTimer = 1.0f;
					}
				}
			}
		}
		else{
			playerHealth.text = "";
			playerAmmo.text = "";
			playerScore.text = "";
			playerTimer.text = "";
			gameOverText.text = "Game Over!\n\nFinal Score: " + controller.playerScore;
			
			Color newColor = fadeOutImage.color;
			//For the record, the alpha channel at 1.0f will be completely visible and therefore blacked out.
			//Counting it towards 2.0f is just to give a bit more time on fadeout so it's not as jarring a transition.
			if(playerDeadTimeout>0.0f){
				playerDeadTimeout-= Time.deltaTime;
			}
			else if(newColor.a<2.0f){
				newColor.a +=  Time.deltaTime;
				fadeOutImage.color = newColor;
			}
			else if(newColor.a>=2.0f){
				Application.LoadLevel(0);
			}
		}
    }
}