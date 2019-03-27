using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAndDestroy : MonoBehaviour
{
	public float timer = 1.0f;
	public int damage = 0;

	void FixedUpdate(){
		timer -= Time.deltaTime;
		if(timer<=0.0f){
			Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter(Collision col){
		Destroy(gameObject);
	}
}
