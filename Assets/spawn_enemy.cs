using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_enemy : MonoBehaviour
{
    public float timeTillNextSpawn = 20.0f;
	public float curTime = 20.0f;
	
	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	
	public int enemyToSpawn = 1;
	
    void Start(){
		if(transform.name.Contains("Enemy 1")){
			timeTillNextSpawn = 20.0f;
			curTime = 20.0f;
			enemyToSpawn = 1;
		}
		if(transform.name.Contains("Enemy 2")){
			timeTillNextSpawn = 30.0f;
			curTime = 30.0f;
			enemyToSpawn = 2;
		}
		if(transform.name.Contains("Enemy 3")){
			timeTillNextSpawn = 50.0f;
			curTime = 50.0f;
			enemyToSpawn = 3;
		}
	}
	
	void FixedUpdate(){
		curTime-=Time.deltaTime;
		if(curTime<=0.0f){
			if(enemyToSpawn==1){
				Instantiate(enemy1, transform.position, Quaternion.identity);
			}
			if(enemyToSpawn==2){
				Instantiate(enemy2, transform.position, Quaternion.identity);
			}
			if(enemyToSpawn==3){
				Instantiate(enemy3, transform.position, Quaternion.identity);
			}
			
			timeTillNextSpawn -= 1.0f;
			curTime = timeTillNextSpawn;
		}
    }
	
}
