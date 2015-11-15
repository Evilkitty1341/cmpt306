using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {

	
	/***************************/
	// Side Noe:
	// -Make sure the mouse button is off in Input for the Quests Q or everytime mouse is clicked Quests appear
	// -Make sure speechbubble is dragged over to Dialogue script in Unity
	//
	//TODO:
	// - Can't get mid conversation dialogue to work
	//		-- added Q1.Repeat = false and Q2.Repeat == false in INTRO's so won't repeat
	//		   for now, but will need to move that to end of mid-dialogue
	//			once we get it working
	/*****************************/
	
	//Quest Script Information
	All_Quests AllQuests;
	Quest2 Q2;
	Quest1 Q1;

	//boolean for dialogue GUI
	bool dialogue = false;

	//an array of dialogue strings
	string[] Dl = new string[6];

	//Fonts / Strings
	public Font chosenFont;
	string message;
	string Name;
	
	//creating GUI window size / position
	Rect winPos = new Rect (((Screen.width / 2) - 260), ((Screen.height / 2) + 50), 512, 140);
	Rect boxPos = new Rect (((Screen.width / 2) - 425), ((Screen.height / 2) - 176), 485, 110);

	//Speech Bubble Information
	public GameObject speechBubble;
	Vector3 speechPos;
	Vector3 MSpeechPos;
	Vector3 ESpeechPos;
	Vector3 PlayerSpeechPos;

	// Use this for initialization
	void Start () {
		
		
		//Quests Components:
		AllQuests = gameObject.GetComponent<All_Quests>();
		Q2 = gameObject.GetComponent<Quest2>();
		Q1 = gameObject.GetComponent<Quest1>();

		//SpeechBubble:
		speechBubble.SetActive(false);
		speechPos = speechBubble.transform.position;
		PlayerSpeechPos = this.transform.position;


		//Dialogue Array:

		//Mathius/Player Dialogue:
		Dl [0] = "\nHello!\n my name is Mathius, in all the chaos I seem to have lost my bag!\n Would you mind finding it for me?";
		Dl [1] = "Sure I can get it!"; 		//player dialogue
		Dl [2] = "Return it to me and I'll give you a reward for your efforts!";
		Dl [3] = "Thanks\n here's some armour\n I have no use for it";

		//Elizabeth/Player Dialogue:
		Dl [4] = "Hello\n My name is Elizabeth\n a band of creatures took my weapons \nif you get them back for me I'll reward you";
		Dl[5] = "Thank you so much!\n to show my appreciation have a magenta potion!";
	}
	
	// Update is called once per frame
	void Update () {

		//QUEST #1 Intro / Outro
		if(AllQuests.QL [0].Has == false && Q1.Has == true && Q1.Repeat == true)
		{
			//Intro for QUEST NUMBER 1
			Name = "Mathius:";
			message = Dl[0];
			AllQuests.QL [0].Has = true;
			Q1.Repeat = false;
		}
		else if(AllQuests.QL [0].Has == true && Q1.Completed == true && Q1.Repeat == true)
		{
			//Outro for QUEST NUMBER 1
			Name = "Mathius:";
			message = Dl[3];
			AllQuests.QL [0].Complete = true;
			Q1.Repeat = false;
		}


		//QUEST #2 Intro / Outro
		if (AllQuests.QL [1].Has == false && Q2.Has == true && Q2.Repeat == true)
		{
			//Intro for QUEST NUMBER 2
			AllQuests.QL [1].Has = true;
			Name = "Elizabeth:";
			message = Dl[4];
			Q2.Repeat = false;
			
		}
		else if (AllQuests.QL [1].Has == true && Q2.Completed == true && Q2.Repeat == true)
		{
			//Outro for QUEST NUMBER 2
			Name = "Elizabeth:";
			AllQuests.QL [1].Complete = true;
			this.gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
			message = Dl[5];
			Q2.Repeat = false;
		}

		//placing speech bubble above proper character
		speechBubble.transform.position = speechPos;
		
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		//when collide with a questgiver set dialogue to true
		//and set speechbubble to active
		//set speechbubble position to above QuestGiver

		//QuestGvier1 Collision
		if(col.gameObject.tag == "QuestGiver1" && Q1.Repeat == true)
		{
			dialogue = true;

			speechBubble.SetActive(true);
			MSpeechPos = col.gameObject.transform.position;
			speechPos = MSpeechPos;
			speechPos.y += 1f;

		}

		//QuestGiver2 Collision
		if (col.gameObject.tag == "QuestGiver2" && Q2.Repeat == true)
		{
			dialogue = true;

			speechBubble.SetActive(true);
			ESpeechPos = col.gameObject.transform.position;
			speechPos = ESpeechPos;
			speechPos.y += 1f;
		}
	}
	
	void OnGUI ()
	{
		if (dialogue)
		{
			winPos = GUI.Window(3, winPos, DialogueWindow, Name);

			//Not Complete:
			//if any key is pressed advance the dialogue
			if(Input.anyKeyDown)
			{
				DecideDialogue();
				//dialogue = false;
				//speechBubble.SetActive(false);
			}
		}
	}
	
	void DialogueWindow(int ID)
	{
		//Font style and GUI box
		GUI.skin.font = chosenFont;
		GUI.Box (boxPos, message);
		
	}

	void DecideDialogue()
	{
		//Not Complete:
		//this is Mid Conversation Dialogue
		//it skips the conditions and just does the last else statement
		if (Input.anyKeyDown) {
			if (AllQuests.QL [0].Has == true && Name == "Mathius" && Q1.Repeat == true) {
				Name = "...";
				speechPos = PlayerSpeechPos;
				speechPos.y += 1f;
				message = Dl [1];
				OnGUI();
			
			} else if (AllQuests.QL [0].Has == true && Name == "..." && Q1.Repeat == true) {
				Name = "Mathius:";
				speechPos = MSpeechPos;
				speechPos.y += 1f;
				message = Dl [2];
				Q1.Repeat = false;
			} else {
				dialogue = false;
				speechBubble.SetActive (false);
			}
		}
	}

}
