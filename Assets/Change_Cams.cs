using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Cams : MonoBehaviour{
	public Camera mainCamera;
	public Camera sideCamera;
	
	private string textFromObject;
	
    // Start is called before the first frame update
    void Start(){
        mainCamera.enabled = true;
		sideCamera.enabled = false;
    }
	
	void OnMouseEnter(){
		gameObject.GetComponent<TextMesh>().color = Color.red;
	}﻿
	
	void OnMouseExit(){
		gameObject.GetComponent<TextMesh>().color = Color.black;
	}
	
	void OnMouseUp(){
		textFromObject = gameObject.GetComponent<TextMesh>().text;
		
		if(textFromObject == "Start Game" || textFromObject == "Back"){
			mainCamera.enabled = !mainCamera.enabled;
			sideCamera.enabled = !sideCamera.enabled;
		}
		else if(textFromObject == "Quit"){
			Application.Quit();
		}
	}
}
