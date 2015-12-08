using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {
	
	
	/***************************/
	// Side Note:
	// -Make sure the mouse button is off in Input for the Quests Q or everytime mouse is clicked Quests appear
	// -Make sure speechbubble is dragged over to Dialogue script in Unity
	//
	//TODO:
	// - Can't get mid conversation dialogue to work
	//		-- added Q1.Repeat = false and Q2.Repeat == false in INTRO's so won't repeat
	//		   for now, but will need to move that to end of mid-dialogue
	//			once we get it working
	
	
	/// LOOK AT OUTRO QUESTS FOR QUESTGIVER 1
	/*****************************/
	//Quest Script Information
	All_Quests AllQuests;
	Quest4 Q4;
	Quest3 Q3;
	Quest2 Q2;
	Quest1 Q1;
	
	//boolean for dialogue GUI
	bool dialogue = false;
	
	//an array of dialogue strings
	string[] Dl = new string[25];
	
	//Fonts / Strings
	public Font chosenFont;
	string message;
	string Name;
	
	//creating GUI window size / position
	static Rect winPos; 
	Rect boxPos; 
	
	//Speech Bubble Information
	public GameObject speechBubble;
	Vector3 speechPos;
	Vector3 MSpeechPos;
	Vector3 ESpeechPos;
	Vector3 RSpeechPos;
	Vector3 SSpeechPos;
	Vector3 G1SpeechPos;
	Vector3 G2SpeechPos;
	Vector3 PlayerSpeechPos;
	
	//Switch for conversations
	string CurrentConvo;
	
	public GameObject FrontGate1;
	public GameObject FrontGate2;
	public GameObject FrontGate3;
	
	public GameObject BackGate1;
	public GameObject BackGate2;
	public GameObject BackGate3;
	
	public bool townDone;
	
	public bool freezePos;
	
	private bool Q3P1;
	private bool Q3P2;
	
	StatCollectionClass playerStat;
	
	// Use this for initialization
	void Start () {
		//Q2.setGoToTown(false);
		
		winPos = new Rect ((Screen.width / 2) - Screen.width / 4, (Screen.height / 2) - Screen.height / 4, Screen.width / 2, Screen.height / 2);
		
		//winPos = new Rect (this.transform.position.x + 355, this.transform.position.y + 412, 500, 200);
		//boxPos = new Rect ((Screen.width / 2) - Screen.width / 4, (Screen.height / 2) - Screen.height / 4, Screen.width / 2, Screen.height / 2);
		
		//Quests Components:
		AllQuests = gameObject.GetComponent<All_Quests>();
		Q4 = gameObject.GetComponent<Quest4>();
		Q3 = gameObject.GetComponent<Quest3>();
		Q2 = gameObject.GetComponent<Quest2>();
		Q1 = gameObject.GetComponent<Quest1>();
		
		playerStat = GetComponent<StatCollectionClass>();
		
		//SpeechBubble:
		speechBubble.SetActive(false);
		speechPos = speechBubble.transform.position;
		
		FrontGate1.SetActive (true);
		FrontGate2.SetActive (false);
		FrontGate3.SetActive (false);
		
		BackGate1.SetActive (true);
		BackGate2.SetActive (false);
		BackGate3.SetActive (false);
		
		townDone = false;
		
		freezePos = false;
		
		Q3P1 = false;
		Q3P2 = false;
		
		//Dialogue Array:
		
		//Mathius/Player Dialogue:
		Dl [0] = "\nHello!\n my name is Mathius, in all the chaos I seem to have lost my bag!\n Could you find it for me?";
		Dl [1] = "\nSure, I can get it!"; 		//player dialogue
		Dl [2] = "\nReturn it to me and I'll give you a reward for your efforts!...\n and If you see my friend Elizabeth tell her I'm okay, \n we got separated after the skyfall";
		Dl [3] = "\nThanks\n here's some gold coins,\n may they aid you on your journey...";
		
		//Elizabeth/Player Dialogue:
		Dl[4] = "\nHello\n My name is Elizabeth\n have you seen my friend Mathius?!";
		Dl[5] = "\nYes I have, he wanted me to tell you he's okay!"; //player dialogue
		Dl[6] = "\nThanks! I was very worried! \n you should head over to Birdtown,\n it's been steeped in turmoil since the skyfall\n here's a town pass, since the skyfall you'll need it to pass through the gate";
		Dl[7] = "\nThanks, I'll check it out!";
		
		//Guard1 Dialogue:
		Dl[8] = "\nWho Goes There!";
		Dl[9] = "\nI'm just a traveller, looking for General E. Speaking..."; // player dialogue
		Dl[10] = "\nYou don't have a town pass AND the entry fee, \nYOU \nSHALL NOT \nPASS! \n come back when you have both!";
		Dl[11] = "\nI have a town pass and enough coins to pay the entry fee"; //player dialogue
		Dl[12] = "\nhmm.. looks like you have the required items, \nyou may enter, \n OPEN THE GATE!";
		Dl[13] = "\nIt's too dangerous to leave the gate open at this time\n CLOSE THE GATE!!";
		
		
		
		/*************************
		 * will need to change where new lines are depending on resolution
		 * for the below dialogue
		 * ************************/
		
		// townRobot Dialogue (QuestGiver3)
		Dl[14] = "\n*Beep* *Boop*\n I was right, General E. Speaking is just\n past the back gate. He's working from his \ncrashed space shuttle. Go talk to Selina, she can \ngive you a key for the back gate";
		Dl[15] = "\nOkay I'l go and speak with her!"; // player
		Dl[16] = "\n*Beep* *Boop*\nOh! I almost forgot! here's something to\n make you feel a bit better!";
		
		// Selina (town)
		Dl[17] = "\nHello Stranger, how can I help you?";
		Dl[18] = "\nGigabyte sent me, he said you could give me the key to the back gate?"; //player
		Dl[19] = "\nah yes! here, take it, I just hope you can stop \nGeneral E. Speaking before it's too late..."; 
		
		
		//Guard 2 Dialogue
		//Negative
		Dl [20] = "\n If you don't have a key, I can't open the gate for you,\n try talking so Selina, she might \n be able to give you one";
		//Postitive
		Dl[21] = "\n I have the key, please open the gate for me"; //player
		Dl[22] = "\n Goodluck, The gate will close behind you,\n we can't risk letting General E. Speaking's soldiers into the town";
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//QUEST #1 Intro / Outro
		if(AllQuests.QL [0].Has == false && Q1.Has == true && Q1.Repeat == true && CurrentConvo == "QuestGiver1")
		{
			//Intro for QUEST NUMBER 1
			message = Dl[0];
			AllQuests.QL [0].Has = true;
			CurrentConvo = "QuestGiver1.0";
		}
		else if (Input.GetKeyDown("space") && AllQuests.QL [0].Has == true && Q1.Completed == true && CurrentConvo == "QuestGiver1.4"){
			
			//Outro 2 for QUEST NUMBER 1
			//makes sure dialogue closes and speech bubble disapears
			dialogue = false;
			speechBubble.SetActive (false);
			//Q1.Finished = true;
			freezePos = false;
		}
		
		
		//QUEST #2 Intro / Outro
		if (AllQuests.QL [1].Has == false && Q2.Has == true && Q2.Repeat == true)
		{
			//Intro for QUEST NUMBER 2
			AllQuests.QL [1].Has = true;
			message = Dl[4];
			speechPos = ESpeechPos;
			speechPos.y += 1f;
			CurrentConvo = "QuestGiver2.0";
			
		}
		else if ( Input.GetKeyDown("space") && AllQuests.QL [1].Has == true && CurrentConvo == "QuestGiver2.3" && Q2.Repeat == true)
		{
			//Outro for QUEST NUMBER 2
			speechPos = PlayerSpeechPos;
			speechPos.y += 1f;
			message = Dl[7];
			//Q2.setGoToTown(true);
			Q2.Repeat = false;
			CurrentConvo = "QuestGiver2.4";
		}
		else if (Input.GetKeyDown("space") && AllQuests.QL [1].Has == true && Q2.Repeat == false && CurrentConvo == "QuestGiver2.4"){
			dialogue = false;
			speechBubble.SetActive (false);
			Q2.Finished1 = true;
			CurrentConvo = " ";
			freezePos = false;
		}
		
		//QUEST #3 Outro Part1
		if (Input.GetKeyDown("space") && AllQuests.QL [2].Has == true && CurrentConvo == "Robo1.1")
		{
			//Intro for QUEST NUMBER 2
			message = Dl[16];
			speechPos = RSpeechPos;
			speechPos.y += 1f;
			playerStat.health = 100;
			playerStat.mana = 100;
			CurrentConvo = "Robo1.2";
		}
		else if (Input.GetKeyDown("space") && AllQuests.QL [2].Has == true && Q3.Repeat == true && CurrentConvo == "Robo1.2")
		{
			//Intro for QUEST NUMBER 2
			dialogue = false;
			speechBubble.SetActive(false);
			CurrentConvo = " ";
			freezePos = false;
			Q3P1 = true;
		}
		
		//QUEST #3 OUTRO PART 2
		if (Input.GetKeyDown("space") && AllQuests.QL [2].Has == true && Q3P1 == true && Q3P2 == false && CurrentConvo == "Selina1.1")
		{
			message = Dl[19];
			speechPos = SSpeechPos;
			speechPos.y += 1f;
			CurrentConvo = "Selina1.2";
		}
		else if (Input.GetKeyDown("space") && AllQuests.QL [2].Has == true && Q3P1 == true && Q3P2 == false && CurrentConvo == "Selina1.2")
		{
			AllQuests.QL[2].Complete = true;
			AllQuests.QL [3].Has = true;
			dialogue = false;
			speechBubble.SetActive(false);
			CurrentConvo = " ";
			Q3P2 = true;
			freezePos = false;
			Q3.Finished = true;
		}
		
		//GUARD 2 OUTRO
		if (Input.GetKeyDown("space") && AllQuests.QL [3].Has == true && CurrentConvo == "Guard2.1")
		{
			dialogue = false;
			speechBubble.SetActive(false);
			CurrentConvo = " ";
			freezePos = false;
		}
		if (Input.GetKeyDown("space") && AllQuests.QL [3].Has == false && CurrentConvo == "Guard2.2")
		{
			dialogue = false;
			speechBubble.SetActive(false);
			CurrentConvo = " ";
			freezePos = false;
		}
		
		
		if(Q1.Finished && Q2.Finished1)
		{
			Q2.setGoToTown(true);
		}
		
		
		
		//PlayerSpeechPos follows player for speech bubble
		PlayerSpeechPos = this.transform.position;
		
		//placing speech bubble above proper character
		speechBubble.transform.position = speechPos;
		
		OnGUIHelper ();
		
	}
	
	void OnGUIHelper()
	{
		
		//Mid Dialogue Conversations
		
		//Quest # 1 Conversation
		if (Input.GetKeyDown("space") && AllQuests.QL [0].Has == true && CurrentConvo == "QuestGiver1.0" && AllQuests.QL [0].Complete == false)
		{ 
			message = Dl [1];
			speechPos = PlayerSpeechPos;
			speechPos.y += 1f;
			CurrentConvo = "QuestGiver1.1";
			
		} else if (Input.GetKeyDown("space") && AllQuests.QL [0].Has == true && CurrentConvo == "QuestGiver1.1" && AllQuests.QL [0].Complete == false) 
		{
			
			message = Dl [2];
			speechPos = MSpeechPos;
			speechPos.y += 1f;
			Q1.Repeat = false;
			CurrentConvo = "QuestGiver1.2";
			
		} else if (Input.GetKeyDown("space") && AllQuests.QL [0].Has == true && CurrentConvo == "QuestGiver1.2" && AllQuests.QL [0].Complete == false) 
		{
			dialogue = false;
			speechBubble.SetActive (false);
			CurrentConvo = "QuestGiver1.3";
			freezePos = false;
			
		}
		
		
		//Quest # 2 Conversation
		if (Input.GetKeyDown("space") && AllQuests.QL [1].Has == true && CurrentConvo == "QuestGiver2.0" && AllQuests.QL [1].Complete == false)
		{ 
			
			message = Dl [4];
			speechPos = ESpeechPos;
			speechPos.y += 1f;;
			CurrentConvo = "QuestGiver2.1";
			
		} else if (Input.GetKeyDown("space") && AllQuests.QL [1].Has == true && CurrentConvo == "QuestGiver2.1" && AllQuests.QL [1].Complete == false) 
		{
			
			message = Dl [5];
			speechPos = PlayerSpeechPos;
			speechPos.y += 1f;
			CurrentConvo = "QuestGiver2.2";
			
		} else if (Input.GetKeyDown("space") && AllQuests.QL [1].Has == true && CurrentConvo == "QuestGiver2.2" && AllQuests.QL [1].Complete == false) 
		{
			
			message = Dl [6];
			speechPos = ESpeechPos;
			speechPos.y += 1f;
			CurrentConvo = "QuestGiver2.3";
		}
		
		
		
		
		//Guard 1 Conversation:
		//NEGATIVE GUARD RESPONSE
		if(Input.GetKeyDown("space") && Q2.getGoToTown() == false && Q2.Completed == false && CurrentConvo == "Guard1")
		{
			message = Dl[9];
			dialogue = true;
			speechBubble.SetActive(true);
			speechPos = PlayerSpeechPos;
			speechPos.y += 1f;
			CurrentConvo = "Guard1.1";
		}
		else if(Input.GetKeyDown("space") && Q2.getGoToTown() == false && Q2.Completed == false && CurrentConvo == "Guard1.1")
		{
			message = Dl[10];
			dialogue = true;
			speechBubble.SetActive(true);
			speechPos = G1SpeechPos;
			speechPos.y += 1f;
			CurrentConvo = "Guard1.2";
		}
		else if(Input.GetKeyDown("space") && Q2.getGoToTown() == false && Q2.Completed == false && CurrentConvo == "Guard1.2")
		{
			//Exits out of negative guard response
			dialogue = false;
			speechBubble.SetActive (false);
			freezePos = false;
		}
		
		
		//POSITIVE GUARD RESPONSE
		if(Input.GetKeyDown("space") && Q2.getGoToTown() == true && Q2.Completed == true  && CurrentConvo == "Guard1")
		{
			message = Dl[11];
			speechBubble.SetActive(true);
			speechPos = PlayerSpeechPos;
			speechPos.y += 1f;
			CurrentConvo = "Guard1.1";
		}
		else if (Input.GetKeyDown("space") && Q2.getGoToTown() == true && Q2.Completed == true && CurrentConvo == "Guard1.1") 
		{
			message = Dl[12];
			CurrentConvo = "Guard1.2";
			speechBubble.SetActive(true);
			speechPos = G1SpeechPos;
			speechPos.y += 1f;
			FrontGate1.SetActive(false);
			FrontGate2.SetActive(true);
		}
		else if(Input.GetKeyDown("space") && Q2.getGoToTown() == true && Q2.Completed == true && CurrentConvo == "Guard1.2")
		{
			//exits out of positive guards response
			dialogue = false;
			speechBubble.SetActive (false);
			AllQuests.QL [1].Complete = true;
			Q2.Finished2 = true;
			freezePos = false;
			
		}
		else if(Input.GetKeyDown("space") && Q2.Finished2 == true && CurrentConvo == "GateClosed" && townDone == false)
		{
			dialogue = false;
			speechBubble.SetActive (false);
			CurrentConvo = " ";
			townDone = true;
			freezePos = false;
		}
		
		
		
		//QUEST 3 MID DIALOGUE
		if (Input.GetKeyDown("space") && AllQuests.QL [2].Has == true && CurrentConvo == "Robo1.0")
		{
			//Intro for QUEST NUMBER 2
			message = Dl[15];
			speechPos = PlayerSpeechPos;
			speechPos.y += 1f;
			CurrentConvo = "Robo1.1";
		}
		//QUEST #3 MID DIALOGUE CONT. SELINA
		if (Input.GetKeyDown("space") && AllQuests.QL [2].Has == true && Q3P1 == true && Q3P2 == false && CurrentConvo == "Selina1.0")
		{
			//Intro for QUEST NUMBER 2
			message = Dl[18];
			speechPos = PlayerSpeechPos;
			speechPos.y += 1f;
			CurrentConvo = "Selina1.1";
		}
		
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		//when collide with a questgiver set dialogue to true
		//and set speechbubble to active
		//set speechbubble position to above QuestGiver
		
		//QuestGvier1 Collision
		if(col.gameObject.tag == "QuestGiver1" && Q1.Repeat == true && Q1.Finished == false && Q1.Completed == false)
		{
			CurrentConvo = "QuestGiver1";
			dialogue = true;
			speechBubble.SetActive(true);
			MSpeechPos = col.gameObject.transform.position;
			speechPos = MSpeechPos;
			speechPos.y += 1f;
			freezePos = true;
			
		}
		
		//OUTRO for QuestGiver1
		if(col.gameObject.tag == "QuestGiver1" && Q1.Repeat == true && Q1.Finished == false && Q1.Completed == true)
		{
			//Outro 1 for QUEST NUMBER 1
			MSpeechPos = col.gameObject.transform.position;
			speechPos = MSpeechPos;
			speechPos.y += 1f;
			message = Dl[3];
			dialogue = true;
			speechBubble.SetActive(true);
			Q1.Finished = true;
			AllQuests.QL [0].Complete = true;
			Q1.Repeat = false;
			CurrentConvo = "QuestGiver1.4";
			freezePos = true;
			
		}
		
		//QuestGiver2 Collision
		if (col.gameObject.tag == "QuestGiver2" && Q2.Repeat == true && Q2.Finished1 == false)
		{
			dialogue = true;
			speechBubble.SetActive(true);
			ESpeechPos = col.gameObject.transform.position;
			speechPos = ESpeechPos;
			speechPos.y += 1f;
			CurrentConvo = "QuestGiver2";
			freezePos = true;
		}
		
		//Guard1 Collision
		if (col.gameObject.tag == "Guard1" && Q2.Finished2 == false)
		{
			freezePos = true;
			message = Dl[8];
			dialogue = true;
			speechBubble.SetActive(true);
			G1SpeechPos = col.gameObject.transform.position;
			speechPos = G1SpeechPos;
			speechPos.y += 1f;
			CurrentConvo = "Guard1";
		}
		
		
		//townRobot Collision
		if (col.gameObject.tag == "QuestGiver3" && Q3P1 == false)
		{
			AllQuests.QL [2].Has = true;
			dialogue = true;
			speechBubble.SetActive(true);
			RSpeechPos = col.gameObject.transform.position;
			speechPos = RSpeechPos;
			speechPos.y += 1f;
			message = Dl[14];
			CurrentConvo = "Robo1.0";
			freezePos = true;
		}
		
		
		//Selina Collision
		if (col.gameObject.tag == "Selina" && Q3P1 == true && Q3P2 == false)
		{
			dialogue = true;
			speechBubble.SetActive(true);
			SSpeechPos = col.gameObject.transform.position;
			speechPos = SSpeechPos;
			speechPos.y += 1f;
			message = Dl[17];
			CurrentConvo = "Selina1.0";
			freezePos = true;
		}
		
		//Guard2 Positive Response
		if (col.gameObject.tag == "Guard2" && Q4.backGate == true)
		{
			dialogue = true;
			speechBubble.SetActive(true);
			speechPos = PlayerSpeechPos;
			speechPos.y += 1f;
			message = Dl[21];
			CurrentConvo = "Guard2.1";
			BackGate1.SetActive(false);
			BackGate2.SetActive(true);
			freezePos = true;
		}
		
		//Guard2 Negative Response
		if (col.gameObject.tag == "Guard2" && Q4.backGate == false)
		{
			dialogue = true;
			speechBubble.SetActive(true);
			G2SpeechPos = col.gameObject.transform.position;
			speechPos = G2SpeechPos;
			speechPos.y += 1f;
			message = Dl[20];
			CurrentConvo = "Guard2.2";
			freezePos = true;
		}
		
		
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Gate" && townDone == false && Q2.Finished2 == true) 
		{
			FrontGate2.SetActive(false);
			FrontGate3.SetActive(true);
			
			dialogue = true;
			speechBubble.SetActive(true);
			speechPos = G1SpeechPos;
			speechPos.y += 1f;
			
			message = Dl[13];
			CurrentConvo = "GateClosed";
			freezePos = true;
		}
		
		
		if (other.gameObject.tag == "backGate") 
		{
			BackGate2.SetActive(false);
			BackGate3.SetActive(true);
			message = Dl[22];
			dialogue = true;
			speechBubble.SetActive(true);
			speechPos = G2SpeechPos;
			speechPos.y += 1f;
			CurrentConvo = "Guard2.1";
			freezePos = true;
			other.gameObject.SetActive(false);
			
		}
	}
	
	
	void OnGUI ()
	{
		//GUI box for dialogue
		if (dialogue) {
			GUI.skin.font = chosenFont;
			GUIStyle centeredStyle = new GUIStyle ("Label");
			centeredStyle.alignment = TextAnchor.MiddleCenter;
			
			GUI.Box (winPos, message);
		}
	}
	
	
}
