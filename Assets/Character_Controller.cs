using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour{
	public Rigidbody body;
	public Transform cam;
	
	//Borrowed from the ThrowObject example from the "Sound Effects & Scripting" tutorial
	private AudioSource source;
	public AudioClip[] gunshotSounds = new AudioClip[3];
	private float volLowRange = .5f;
    private float volHighRange = 1.0f;

	public GameObject snowball;
	public int forceOfSnowballs = 4500;
	
	public string[] listOfWeaponNames = {"Pistol","Shotgun","MachineGun"};
	public int[] listOfAmmo = {5,5,50};
	public int[] listOfAmmoMax = {50,20,200};
	public int playerCurrentWeapon = 0;
	public int playerHealth = 100;
	public int playerScore = 0;
	public bool playerDead = false;
	
	public bool canFireMGAgain = true;
	private float machineGunTimerFINAL = 0.1f;
	public float machineGunTimer = 0.1f;
	
	public bool canFireShotgunAgain = true;
	private float shotgunTimerFINAL = 1.0f;
	public float shotgunTimer = 1.0f;
	
	//public bool isVulnerable = true;
	//private float vulnerabilityTimerFINAL = 0.5f;
	//public float vulnerabilityTimer = 0.5f;

	Vector2 input;

	void Start(){
		makeAllWeaponsDisappearExceptForCurrentlyEquippedOne();
		source = GetComponent<AudioSource>();
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


			float vol = Random.Range(volLowRange, volHighRange);
			if(Input.GetMouseButtonDown(0)){
				if(listOfWeaponNames[playerCurrentWeapon] == "Pistol"){
					if(listOfAmmo[0]>0){
						Transform pistolBulletSpawnpointTransform = GameObject.Find("PistolBulletSpawnpoint").transform;
						
						GameObject bullet = Instantiate(snowball, pistolBulletSpawnpointTransform.position, Quaternion.identity) as GameObject;
						bullet.GetComponent<CreateAndDestroy>().damage = 3;
						bullet.GetComponent<Rigidbody>().AddForce(pistolBulletSpawnpointTransform.forward * forceOfSnowballs);
						listOfAmmo[0]--;
						source.time = 1.6f;
						source.PlayOneShot(gunshotSounds[0], vol);
					}
				}
				if(listOfWeaponNames[playerCurrentWeapon] == "Shotgun"){
					if(canFireShotgunAgain){
						if(listOfAmmo[1]>0){
							GameObject[,] bullets = new GameObject[3,3];
							float spaceBetweenBullets = 0.5f;
						
							Vector3 frontOfGun = cam.forward;
							Vector3 upOfGun = cam.up;
							Vector3 sideOfGun = cam.right;
							
							Transform shotgunBulletSpawnpointTransform = GameObject.Find("ShotgunBulletSpawnpoint").transform;
							
							int forceOfSeparationOnShotgunBullets = 500;
							
							//So here I'm instantiating every single bullet. Each of them shoots off in a different direction, so doing manually seemed best.
							//there's probably a better way to do this, but I'm leaving it as is unless I decide to rewrite the math.
							bullets[0,0] = Instantiate(snowball, shotgunBulletSpawnpointTransform.position + -(sideOfGun)*spaceBetweenBullets + upOfGun*spaceBetweenBullets, Quaternion.identity) as GameObject;
							bullets[0,0].GetComponent<Rigidbody>().AddForce(forceOfSeparationOnShotgunBullets*(-(sideOfGun) + upOfGun) + (shotgunBulletSpawnpointTransform.forward * forceOfSnowballs));
							bullets[0,1] = Instantiate(snowball, shotgunBulletSpawnpointTransform.position + Vector3.zero*spaceBetweenBullets + upOfGun*spaceBetweenBullets, Quaternion.identity) as GameObject;
							bullets[0,1].GetComponent<Rigidbody>().AddForce(forceOfSeparationOnShotgunBullets*(upOfGun) + (shotgunBulletSpawnpointTransform.forward * forceOfSnowballs));
							bullets[0,2] = Instantiate(snowball, shotgunBulletSpawnpointTransform.position + (sideOfGun)*spaceBetweenBullets + upOfGun*spaceBetweenBullets, Quaternion.identity) as GameObject;
							bullets[0,2].GetComponent<Rigidbody>().AddForce(forceOfSeparationOnShotgunBullets*((sideOfGun) + upOfGun) + (shotgunBulletSpawnpointTransform.forward * forceOfSnowballs));
							
							bullets[1,0] = Instantiate(snowball, shotgunBulletSpawnpointTransform.position + -(sideOfGun)*spaceBetweenBullets, Quaternion.identity) as GameObject;
							bullets[1,0].GetComponent<Rigidbody>().AddForce(forceOfSeparationOnShotgunBullets*(-(sideOfGun)) + (shotgunBulletSpawnpointTransform.forward * forceOfSnowballs));
							bullets[1,1] = Instantiate(snowball, shotgunBulletSpawnpointTransform.position + Vector3.zero*spaceBetweenBullets, Quaternion.identity) as GameObject;
							bullets[1,1].GetComponent<Rigidbody>().AddForce((shotgunBulletSpawnpointTransform.forward * forceOfSnowballs));
							bullets[1,2] = Instantiate(snowball, shotgunBulletSpawnpointTransform.position + (sideOfGun)*spaceBetweenBullets, Quaternion.identity) as GameObject;
							bullets[1,2].GetComponent<Rigidbody>().AddForce(forceOfSeparationOnShotgunBullets*((sideOfGun)) + (shotgunBulletSpawnpointTransform.forward * forceOfSnowballs));
							
							bullets[2,0] = Instantiate(snowball, shotgunBulletSpawnpointTransform.position + -(sideOfGun)*spaceBetweenBullets + -(upOfGun)*spaceBetweenBullets, Quaternion.identity) as GameObject;
							bullets[2,0].GetComponent<Rigidbody>().AddForce(forceOfSeparationOnShotgunBullets*(-(sideOfGun) + -(upOfGun)) + (shotgunBulletSpawnpointTransform.forward * forceOfSnowballs));
							bullets[2,1] = Instantiate(snowball, shotgunBulletSpawnpointTransform.position + Vector3.zero*spaceBetweenBullets + -(upOfGun)*spaceBetweenBullets, Quaternion.identity) as GameObject;
							bullets[2,1].GetComponent<Rigidbody>().AddForce(forceOfSeparationOnShotgunBullets*(-upOfGun) + (shotgunBulletSpawnpointTransform.forward * forceOfSnowballs));
							bullets[2,2] = Instantiate(snowball, shotgunBulletSpawnpointTransform.position + (sideOfGun)*spaceBetweenBullets + -(upOfGun)*spaceBetweenBullets, Quaternion.identity) as GameObject;
							bullets[2,2].GetComponent<Rigidbody>().AddForce(forceOfSeparationOnShotgunBullets*((sideOfGun) + -upOfGun) + (shotgunBulletSpawnpointTransform.forward * forceOfSnowballs));
							
							for(int i=0; i<3; i++){
								for(int j=0; j<3; j++){
									bullets[i,j].GetComponent<CreateAndDestroy>().damage = 2;
								}
							}
							
							//bullet.GetComponent<Rigidbody>().AddForce(cam.forward * forceOfSnowballs);
							listOfAmmo[1]--;
							source.PlayOneShot(gunshotSounds[1], vol);
							canFireShotgunAgain = false;
						}
					}
				}
			}
			if(Input.GetMouseButton(0)){
				if(canFireMGAgain){
					if(listOfWeaponNames[playerCurrentWeapon] == "MachineGun"){
						if(listOfAmmo[2]>0){
							Transform machineGunBulletSpawnpointTransform = GameObject.Find("MachineGunBulletSpawnpoint").transform;
							
							GameObject bullet = Instantiate(snowball, machineGunBulletSpawnpointTransform.position, Quaternion.identity) as GameObject;
							bullet.GetComponent<CreateAndDestroy>().damage = 1;
							bullet.GetComponent<Rigidbody>().AddForce(machineGunBulletSpawnpointTransform.forward * forceOfSnowballs);
							listOfAmmo[2]--;
							source.PlayOneShot(gunshotSounds[2], vol);
							canFireMGAgain = false;
						}
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