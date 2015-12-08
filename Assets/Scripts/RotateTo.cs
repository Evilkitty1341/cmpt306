using UnityEngine;
using System.Collections;

public class RotateTo : MonoBehaviour {

	public Vector3 target;

	// Use this for initialization
	void Start () {

		target = Vector3.zero;
	
	}
	
	// Update is called once per frame
	void Update () {

		float rotZ = Mathf.Atan2 (target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (rotZ + 90f, Vector3.forward);
	}
}
