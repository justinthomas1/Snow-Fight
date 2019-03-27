using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStuff : MonoBehaviour
{
	public bool playerDead = false;
	
	public float speedH = 2.0f;
	
	private float yaw = 0.0f;
	private float pitch = -90.0f;

    // Update is called once per frame
    void Update(){
		if(!playerDead){
			yaw += speedH * Input.GetAxis("Mouse X");
			transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
		}
    }
}
