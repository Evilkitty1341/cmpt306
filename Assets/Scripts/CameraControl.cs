using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	/*
	 * currently constantly walks towards the player, nothing else is implemented
	 * */
	public GameObject followFemale;
	public GameObject followMale;

	//Temp Character Sprites for walk in sprite scene
	public GameObject tempFemale;
	public GameObject tempMale;

	// Use this for initialization
	void Start () {


		followFemale = GameObject.Find ("FemalePlayerPrefab");
		followMale = GameObject.Find ("MalePlayerPrefab");

		//getting sprites for walk in sprite scene
		tempFemale = GameObject.Find ("TempFemale");
		tempMale = GameObject.Find ("TempMale");

	}
	
	// Update is called once per frame
	void Update () {

		// Make the camera follow the player
		if (PlayerSelect.getFemale ()) {
			Destroy (followMale);
			Destroy (tempMale);
			this.transform.position = followFemale.transform.position + new Vector3 (0, 0, -10);

		} else {
			Destroy (followFemale);
			Destroy (tempFemale);
			this.transform.position = followMale.transform.position + new Vector3 (0, 0, -10);

		}

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Application.LoadLevel (0);
		}
	}
	
}
