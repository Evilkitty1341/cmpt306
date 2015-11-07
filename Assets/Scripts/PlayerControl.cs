using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	public float speed = 5f; // Character's movement speed
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Controlls for character movement
		if (Input.GetKey(KeyCode.UpArrow))
		{
			transform.Translate (Vector3.up * speed * Time.deltaTime);
			anim.SetInteger ("Direction", 0); // Up
			anim.SetBool ("Moving", true);
		} else if (Input.GetKey(KeyCode.DownArrow))
		{
			transform.Translate (Vector3.down * speed * Time.deltaTime);
			anim.SetInteger ("Direction", 1); // Down
			anim.SetBool ("Moving", true);
		} else if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate (Vector3.left * speed * Time.deltaTime);
			anim.SetInteger ("Direction", 2); // Left
			anim.SetBool ("Moving", true);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate (Vector3.right * speed * Time.deltaTime);
			anim.SetInteger ("Direction", 3); // Right
			anim.SetBool ("Moving", true);
		} else {
			anim.SetBool ("Moving", false);
		}
	}
}
