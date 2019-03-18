using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour{
	public Rigidbody body;
	public Transform cam;

	public GameObject snowball;
	public int forceOfSnowballs = 3500;
	
	public string[] listOfWeaponNames = {"Pistol","Shotgun","MachineGun"};
	public int playerCurrentWeapon = 0;
	public int playerHealth = 100;
	public int playerPistolAmmo = 5;
	public int playerShotgunAmmo = 5;
	public int playerMachineGunAmmo = 5;

	Vector2 input;

	void Start(){
		makeAllWeaponsDisappearExceptForCurrentlyEquippedOne();
	}

    // Update is called once per frame
    void FixedUpdate(){
		input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		input = Vector2.ClampMagnitude(input, 1);

		Vector3 camF = cam.forward;
		Vector3 camR = cam.right;

		//Zeroing out the y values so movement is only affected with X axis of camera
		camF.y = 0;
		camR.y = 0;
		camF = camF.normalized;
		camR = camR.normalized;

		transform.position += (camF*input.y + camR*input.x)*Time.deltaTime*10;

		if(Input.GetMouseButtonDown(0)){
			if(listOfWeaponNames[playerCurrentWeapon] == "Pistol"){
				GameObject bullet = Instantiate(snowball, GameObject.Find("PistolBulletSpawnpoint").transform.position, Quaternion.identity) as GameObject;
				bullet.GetComponent<Rigidbody>().AddForce(cam.forward * forceOfSnowballs);	
			}
			if(listOfWeaponNames[playerCurrentWeapon] == "Shotgun"){
				GameObject bullet = Instantiate(snowball, GameObject.Find("ShotgunBulletSpawnpoint").transform.position, Quaternion.identity) as GameObject;
				bullet.GetComponent<Rigidbody>().AddForce(cam.forward * forceOfSnowballs);
			}
			if(listOfWeaponNames[playerCurrentWeapon] == "MachineGun"){
				GameObject bullet = Instantiate(snowball, GameObject.Find("MachineGunBulletSpawnpoint").transform.position, Quaternion.identity) as GameObject;
				bullet.GetComponent<Rigidbody>().AddForce(cam.forward * forceOfSnowballs);
			}
			
		}
		if(Input.GetMouseButtonDown(1)){
			playerCurrentWeapon++;
			
			//Ensures the array index never goes out of bounds.
			playerCurrentWeapon = playerCurrentWeapon%3;
			
			makeAllWeaponsDisappearExceptForCurrentlyEquippedOne();
		}
    }
	
	void makeAllWeaponsDisappearExceptForCurrentlyEquippedOne(){
		GameObject.Find("Pistol").GetComponent<Renderer>().enabled = false;
		GameObject.Find("Pistol").GetComponent<BoxCollider>().enabled = false;
		GameObject.Find("Shotgun").GetComponent<Renderer>().enabled = false;
		GameObject.Find("Shotgun").GetComponent<BoxCollider>().enabled = false;
		GameObject.Find("MachineGun").GetComponent<Renderer>().enabled = false;
		GameObject.Find("MachineGun").GetComponent<BoxCollider>().enabled = false;
		
		GameObject.Find(listOfWeaponNames[playerCurrentWeapon]).GetComponent<MeshRenderer>().enabled = true;
	}
}