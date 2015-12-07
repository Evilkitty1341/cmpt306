using UnityEngine;
using System.Collections;

public class StoryLineComponents : MonoBehaviour {

	public GameObject thoughtBubble;
	public GameObject RthoughtBubble;

	public GameObject smoke;

	public GameObject Num1;
	public GameObject FemNum2;
	public GameObject MaleNum2;
	public GameObject Num3;
	public GameObject Num4;
	public GameObject Num5;
	public GameObject FemNum6;
	public GameObject MaleNum6;
	public GameObject Num7;
	public GameObject Num8;

	public GameObject robot;

	public GameObject tutorial;

	bool done;

	public bool playerEnabled;

	string dialogue;

	CameraControl controller;

	Vector3 destination;

	Vector3 dialoguePos;

	Vector3 thoughtDestination;

	bool check;

	// Use this for initialization
	void Start () {

		controller = this.GetComponent<CameraControl>();

		destination = new Vector3 (-120, 0, 0);

		thoughtDestination = new Vector3 (-125, 0, 0);

		dialoguePos = new Vector3 (-122f, -3.7f, 0f);

		thoughtBubble.SetActive (false);
		RthoughtBubble.SetActive (false);
		smoke.SetActive (false);

		controller.followMale.GetComponent<SpriteRenderer>().enabled = false;
		controller.followFemale.GetComponent<SpriteRenderer>().enabled = false;

		Num1.SetActive (false);
		FemNum2.SetActive (false);
		MaleNum2.SetActive (false);
		Num3.SetActive (false);
		Num4.SetActive (false);
		Num5.SetActive (false);
		FemNum6.SetActive (false);
		MaleNum6.SetActive (false);
		Num7.SetActive (false);
		Num8.SetActive (false);

		check = true;
		done = false;
		tutorial.SetActive(false);
		dialogue = "Gigabyte1.0";

		playerEnabled = false;
}

// Update is called once per frame
	void Update () {
		if (!done) 
		{
			//MALE DIALOGUE
			if(controller.tempMale != null && controller.tempMale.transform.position == destination )
			{

				if(check == true)
				{
					thoughtBubble = GameObject.Instantiate(thoughtBubble, transform.position, transform.rotation) as GameObject;
					RthoughtBubble = GameObject.Instantiate(RthoughtBubble, transform.position, transform.rotation) as GameObject;
					thoughtBubble.transform.position = new Vector3(-123f, 2.62f, 0f);
					RthoughtBubble.transform.position = new Vector3(-115f, 2.27f, 0f);
					thoughtBubble.SetActive(true);
					RthoughtBubble.SetActive(true);
					check = false;
				}

				if(Input.GetKeyDown("space") && dialogue == "Gigabyte1.0")
				{
					check = false;
					thoughtBubble.SetActive(false);
					RthoughtBubble.SetActive(false);
					Num1 = GameObject.Instantiate(Num1, transform.position, transform.rotation) as GameObject;
					Num1.transform.position = dialoguePos;
					Num1.SetActive(true);
					dialogue = "Player1.0";
				}
				else if(Input.GetKeyDown("space") && dialogue == "Player1.0")
				{
					Num1.SetActive(false);
					MaleNum2 = GameObject.Instantiate(MaleNum2, transform.position, transform.rotation) as GameObject;
					MaleNum2.SetActive(true);
					MaleNum2.transform.position = dialoguePos;
					dialogue = "Gigabyte1.1";
				}

				else if(Input.GetKeyDown("space") && dialogue == "Gigabyte1.1")
				{
					MaleNum2.SetActive(false);
					Num3 = GameObject.Instantiate(Num3, transform.position, transform.rotation) as GameObject;
					Num3.SetActive(true);
					Num3.transform.position = dialoguePos;
					dialogue = "Gigabyte1.2";
				}
				else if(Input.GetKeyDown("space") && dialogue == "Gigabyte1.2")
				{
					Num3.SetActive(false);
					Num4 = GameObject.Instantiate(Num4, transform.position, transform.rotation) as GameObject;
					Num4.SetActive(true);
					Num4.transform.position = dialoguePos;
					dialogue = "Gigabyte1.3";
				}
				else if(Input.GetKeyDown("space") && dialogue == "Gigabyte1.3")
				{
					Num4.SetActive(false);
					Num5 = GameObject.Instantiate(Num5, transform.position, transform.rotation) as GameObject;
					Num5.SetActive(true);
					Num5.transform.position = dialoguePos;
					dialogue = "Player1.1";
				}
				else if(Input.GetKeyDown("space") && dialogue == "Player1.1")
				{
					Num5.SetActive(false);
					MaleNum6 = GameObject.Instantiate(MaleNum6, transform.position, transform.rotation) as GameObject;
					MaleNum6.SetActive(true);
					MaleNum6.transform.position = dialoguePos;
					dialogue = "Gigabyte1.4";
				}
				else if(Input.GetKeyDown("space") && dialogue == "Gigabyte1.4")
				{
					smoke.SetActive(false);
					MaleNum6.SetActive(false);
					Num7 = GameObject.Instantiate(Num7, transform.position, transform.rotation) as GameObject;
					Num7.SetActive(true);
					Num7.transform.position = dialoguePos;
					dialogue = "smoke";
				}
				else if(Input.GetKeyDown("space") && dialogue == "smoke")
				{
					smoke.SetActive(true);
					//smoke = GameObject.Instantiate(smoke, transform.position, transform.rotation) as GameObject;
					controller.tempMale.SetActive(false);
					dialogue = "Gigabyte1.5";
				}
				else if(Input.GetKeyDown("space") && dialogue == "Gigabyte1.5")
				{
					Num7.SetActive(false);
					Num8 = GameObject.Instantiate(Num8, transform.position, transform.rotation) as GameObject;
					Num8.SetActive(true);
					Num8.transform.position = dialoguePos;
					dialogue = "tutorial";
				}
				else if(Input.GetKeyDown("space") && dialogue == "tutorial")
				{
					smoke.SetActive(false);
					tutorial.SetActive(true);
					controller.followMale.GetComponent<SpriteRenderer>().enabled = true;
					
					tutorial = GameObject.Instantiate(tutorial, transform.position, transform.rotation) as GameObject;
					tutorial.transform.position = new Vector3(-124f, -1.97f, 0f);
					
					playerEnabled = true;
					
					Num8.SetActive(false);
					robot.SetActive(false);
					dialogue = "done";
				}
				else if(Input.GetKeyDown("space") && dialogue == "done")
				{
					tutorial.SetActive(false);
					done = true;
				}



			//FEMALE DIALOGUE
			}
			else if(controller.tempFemale != null && controller.tempFemale.transform.position == destination )
			{
				if(check == true)
				{
					thoughtBubble = GameObject.Instantiate(thoughtBubble, transform.position, transform.rotation) as GameObject;
					RthoughtBubble = GameObject.Instantiate(RthoughtBubble, transform.position, transform.rotation) as GameObject;
					thoughtBubble.transform.position = new Vector3(-123f, 2.62f, 0f);
					RthoughtBubble.transform.position = new Vector3(-115f, 2.27f, 0f);
					thoughtBubble.SetActive(true);
					RthoughtBubble.SetActive(true);
					check = false;
				}
				
				if(Input.GetKeyDown("space") && dialogue == "Gigabyte1.0")
				{
					check = false;
					thoughtBubble.SetActive(false);
					RthoughtBubble.SetActive(false);
					Num1 = GameObject.Instantiate(Num1, transform.position, transform.rotation) as GameObject;
					Num1.transform.position = dialoguePos;
					Num1.SetActive(true);
					dialogue = "Player1.0";
				}
				else if(Input.GetKeyDown("space") && dialogue == "Player1.0")
				{
					Num1.SetActive(false);
					FemNum2 = GameObject.Instantiate(FemNum2, transform.position, transform.rotation) as GameObject;
					FemNum2.SetActive(true);
					FemNum2.transform.position = dialoguePos;
					dialogue = "Gigabyte1.1";
				}
				else if(Input.GetKeyDown("space") && dialogue == "Gigabyte1.1")
				{
					FemNum2.SetActive(false);
					Num3 = GameObject.Instantiate(Num3, transform.position, transform.rotation) as GameObject;
					Num3.SetActive(true);
					Num3.transform.position = dialoguePos;
					dialogue = "Gigabyte1.2";
				}
				else if(Input.GetKeyDown("space") && dialogue == "Gigabyte1.2")
				{
					Num3.SetActive(false);
					Num4 = GameObject.Instantiate(Num4, transform.position, transform.rotation) as GameObject;
					Num4.SetActive(true);
					Num4.transform.position = dialoguePos;
					dialogue = "Gigabyte1.3";
				}
				else if(Input.GetKeyDown("space") && dialogue == "Gigabyte1.3")
				{
					Num4.SetActive(false);
					Num5 = GameObject.Instantiate(Num5, transform.position, transform.rotation) as GameObject;
					Num5.SetActive(true);
					Num5.transform.position = dialoguePos;
					dialogue = "Player1.1";
				}
				else if(Input.GetKeyDown("space") && dialogue == "Player1.1")
				{
					Num5.SetActive(false);
					FemNum6 = GameObject.Instantiate(FemNum6, transform.position, transform.rotation) as GameObject;
					FemNum6.SetActive(true);
					FemNum6.transform.position = dialoguePos;
					dialogue = "smoke";
				}
				else if(Input.GetKeyDown("space") && dialogue == "smoke")
				{
					smoke.SetActive(true);
					//smoke = GameObject.Instantiate(smoke, transform.position, transform.rotation) as GameObject;
					controller.tempFemale.SetActive(false);
					dialogue = "Gigabyte1.4";
				}
				else if(Input.GetKeyDown("space") && dialogue == "Gigabyte1.4")
				{
					FemNum6.SetActive(false);
					Num7 = GameObject.Instantiate(Num7, transform.position, transform.rotation) as GameObject;
					Num7.SetActive(true);
					Num7.transform.position = dialoguePos;
					dialogue = "Gigabyte1.5";
				}
				else if(Input.GetKeyDown("space") && dialogue == "smoke")
				{
					smoke.SetActive(true);
					smoke = GameObject.Instantiate(smoke, transform.position, transform.rotation) as GameObject;
					controller.tempFemale.SetActive(false);
					dialogue = "Gigabyte1.5";
				}
				else if(Input.GetKeyDown("space") && dialogue == "Gigabyte1.5")
				{
					Num7.SetActive(false);
					Num8 = GameObject.Instantiate(Num8, transform.position, transform.rotation) as GameObject;
					Num8.SetActive(true);
					Num8.transform.position = dialoguePos;
					dialogue = "tutorial";
				}
				else if(Input.GetKeyDown("space") && dialogue == "tutorial")
				{
					smoke.SetActive(false);
					tutorial.SetActive(true);
					controller.followFemale.GetComponent<SpriteRenderer>().enabled = true;

					tutorial = GameObject.Instantiate(tutorial, transform.position, transform.rotation) as GameObject;
					tutorial.transform.position = new Vector3(-124f, -1.97f, 0f);
					playerEnabled = true;

					Num8.SetActive(false);
					robot.SetActive(false);
					dialogue = "done";
				}
				else if(Input.GetKeyDown("space") && dialogue == "done")
				{
					tutorial.SetActive(false);
					done = true;
				}
			}
		}
		
	}
	
}
