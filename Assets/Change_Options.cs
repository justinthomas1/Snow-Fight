using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Options : MonoBehaviour{
	private GameObject survivalButton;
	private GameObject timeAttackButton;
	
	//This value actually needs to be static or else the boolean will be overwritten each time you mouse over to the other option.
	public static bool survivalToggled;
	
	private string textFromObject;
	
    // Start is called before the first frame update
    void Start(){
		survivalToggled = true;
		
		survivalButton = GameObject.Find("SurvivalButton");
		timeAttackButton = GameObject.Find("TimeAttackButton");
		
		survivalButton.GetComponent<TextMesh>().color = Color.yellow;
    }
	
	void OnMouseEnter(){
		gameObject.GetComponent<TextMesh>().color = Color.red;
	}﻿
	
	void OnMouseExit(){
		if(survivalToggled){
			survivalButton.GetComponent<TextMesh>().color = Color.yellow;
			timeAttackButton.GetComponent<TextMesh>().color = Color.black;
		}
		else if(!survivalToggled){
			survivalButton.GetComponent<TextMesh>().color = Color.black;
			timeAttackButton.GetComponent<TextMesh>().color = Color.yellow;
		}
	}
	
	void OnMouseUp(){
		textFromObject = gameObject.GetComponent<TextMesh>().text;
		
		if(textFromObject == "Survival"){
			survivalToggled = true;
		}
		else if(textFromObject == "Time Attack"){
			survivalToggled = false;
		}
	}
}