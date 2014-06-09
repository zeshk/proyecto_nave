using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public GameObject ultrashot;
	public Transform shotSpawn;
	public float fireRate;
	public float ultraRate;
	public bool robot;
	private Vector3 ultra1 = new Vector3(5f,0f,-10f);
	private Vector3 ultra2 = new Vector3(2.5f,0f,-10f);
	private Vector3 ultra3 = new Vector3(0f,0f,-10f);
	private Vector3 ultra4 = new Vector3(-2.5f,0f,-10f);
	private Vector3 ultra5 = new Vector3(-5f,0f,-10f);
	 
	private float nextFire;
	private float coolDownUltra;
	
	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audio.Play ();
		}
		//Transformacion a robot
		if (Input.GetKeyDown (KeyCode.R))
		{
			if(robot == true){
				robot = false;
			} else {
				robot = true;
			}

		}
		//Disparo automatico para el robot
		if (robot && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audio.Play ();
		}
		//Disparo especial
		if (Input.GetKeyDown (KeyCode.Q) && Time.time > coolDownUltra)
		{
			coolDownUltra = Time.time + ultraRate;
			Instantiate(ultrashot, ultra1, shotSpawn.rotation);
			audio.Play ();
			Instantiate(ultrashot, ultra2, shotSpawn.rotation);
			audio.Play ();
			Instantiate(ultrashot, ultra3, shotSpawn.rotation);
			audio.Play ();
			Instantiate(ultrashot, ultra4, shotSpawn.rotation);
			audio.Play ();
			Instantiate(ultrashot, ultra5, shotSpawn.rotation);
			audio.Play ();
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;
		
		rigidbody.position = new Vector3
		(
			Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
		);
		
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}
}

