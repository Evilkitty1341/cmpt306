using UnityEngine;
using System.Collections;

public class StoryLine : MonoBehaviour {

	//Story scenes in order
	public GameObject S1;
	public GameObject S2;

	//Text scenes in order
	public GameObject T1;
	public GameObject TSpace;
	public GameObject T2;
	public GameObject T3;


	// Use this for initialization
	void Start () {

		S1.SetActive (true);
		S2.SetActive (false);

		TSpace.SetActive (true);
		T1.SetActive (false);
		T2.SetActive (false);
		T3.SetActive (false);
	
	}
	
	// Update is called once per frame
	void Update () {

		//switches between scenes
		SwitchScene ();
	
	}

	void SwitchScene()
	{
		if (Input.anyKeyDown && S1.activeSelf == true) {

			TSpace.SetActive (false);
			S1.SetActive(false);
			T1.SetActive(true);
			S2.SetActive(true);
		}
		else if (Input.anyKeyDown && T1.activeSelf == true) {
			T1.SetActive(false);
			T2.SetActive(true);
		}
		else if (Input.anyKeyDown && T2.activeSelf == true) {
			T2.SetActive(false);
			T3.SetActive(true);
		}
		else if (Input.anyKeyDown && T3.activeSelf == true) {
			Application.LoadLevel (1);
		}


	}


}
