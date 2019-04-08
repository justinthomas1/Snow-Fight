using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Level : MonoBehaviour{
	private string textFromObject;
	
	void OnMouseEnter(){
		gameObject.GetComponent<TextMesh>().color = Color.red;
	}﻿
	
	void OnMouseExit(){
		gameObject.GetComponent<TextMesh>().color = Color.black;
	}
	
	void OnMouseUp(){
		textFromObject = gameObject.GetComponent<TextMesh>().text;
		
		if(textFromObject == "Snowy Hills"){
			Application.LoadLevel(1);
		}
		else if(textFromObject == "The Cabin"){
			Application.LoadLevel(2);
		}
		else if(textFromObject == "Icy Cavern"){
			Application.LoadLevel(3);
		}
	}
}