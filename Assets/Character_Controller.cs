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
	public bool playerDead = false;
	public int playerPistolAmmo = 5;
	public int playerShotgunAmmo = 5;
	public int playerMachineGunAmmo = 5;
	
	public bool canFireMGAgain = true;
	private float machineGunTimerFINAL = 0.1f;
	public float machineGunTimer = 0.1f;
	
	public bool canFireShotgunAgain = true;
	private float shotgunTimerFINAL = 1.0f;
	public float shotgunTimer = 1.0f;

	Vector2 input;

	void Start(){
		makeAllWeaponsDisappearExceptForCurrentlyEquippedOne();
	}
	
	void Update(){
		if(playerHealth<=0){
			Destroy(GetComponent<BoxCollider>());
			Destroy(GetComponent<Rigidbody>());
			//Destroy(GetComponent<MeshRenderer>());
			playerDead = true;
			transform.gameObject.GetComponent<CameraStuff>().playerDead = true;
		}
	}

    // Update is called once per frame
    void FixedUpdate(){
		if(!playerDead){
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
			
			if(!canFireMGAgain){
				machineGunTimer -= Time.deltaTime;
				if(machineGunTimer<=0.0f){
					canFireMGAgain = true;
					machineGunTimer = machineGunTimerFINAL;
				}
			}
			if(!canFireShotgunAgain){
				shotgunTimer -= Time.deltaTime;
				if(shotgunTimer<=0.0f){
					shotgunTimer = shotgunTimerFINAL;
					canFireShotgunAgain = true;
				}
			}

			if(Input.GetMouseButtonDown(0)){
				if(listOfWeaponNames[playerCurrentWeapon] == "Pistol"){
					GameObject bullet = Instantiate(snowball, GameObject.Find("PistolBulletSpawnpoint").transform.position, Quaternion.identity) as GameObject;
					bullet.GetComponent<CreateAndDestroy>().damage = 2;
					bullet.GetComponent<Rigidbody>().AddForce(cam.forward * forceOfSnowballs);	
				}
				if(listOfWeaponNames[playerCurrentWeapon] == "Shotgun"){
					GameObject[] bullets = new GameObject[9];
					
					//TODO: Maybe a 2D array of bullets?
					/* for(int i=0; i<3; i++){
						for(int j=0; j<3; j++)
							bullets[] = Instantiate(snowball, GameObject.Find("ShotgunBulletSpawnpoint").transform.position, Quaternion.identity) as GameObject;
						}
					}
					
					bullet.GetComponent<Rigidbody>().AddForce(cam.forward * forceOfSnowballs); */
				}
			}
			if(Input.GetMouseButton(0)){
				if(canFireMGAgain){
					if(listOfWeaponNames[playerCurrentWeapon] == "MachineGun"){
						GameObject bullet = Instantiate(snowball, GameObject.Find("MachineGunBulletSpawnpoint").transform.position, Quaternion.identity) as GameObject;
						bullet.GetComponent<CreateAndDestroy>().damage = 1;
						bullet.GetComponent<Rigidbody>().AddForce(cam.forward * forceOfSnowballs);
						canFireMGAgain = false;
					}
				}
			}
			
			if(Input.GetMouseButtonDown(1)){
				playerCurrentWeapon++;
				
				//Ensures the array index never goes out of bounds.
				playerCurrentWeapon = playerCurrentWeapon%3;
				
				makeAllWeaponsDisappearExceptForCurrentlyEquippedOne();
			}
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
		GameObject.Find(listOfWeaponNames[playerCurrentWeapon]).GetComponent<BoxCollider>().enabled = true;
	}
}