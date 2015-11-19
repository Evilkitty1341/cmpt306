using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
	public GameObject followFemale;
	public GameObject followMale;
	// Use this for initialization
	void Start () {
		followFemale = GameObject.Find ("FemalePlayerPrefab");
		followMale = GameObject.Find ("MalePlayerPrefab");
	}
	
	// Update is called once per frame
	void Update () {

		// Make the camera follow the player
		if (PlayerSelect.getFemale ()) {
			Destroy (followMale);
			this.transform.position = followFemale.transform.position + new Vector3 (0, 0, -10);
		} else {
			Destroy (followFemale);
			this.transform.position = followMale.transform.position + new Vector3 (0, 0, -10);
		}
	}
}
