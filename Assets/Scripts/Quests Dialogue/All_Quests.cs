using UnityEngine;
using System.Collections;

public class All_Quests : MonoBehaviour {

	/* Side Note:
	 * -Make sure Questgivers are tagged as QuestGiver1 or QuestGiver2
	 * -Also make sure Items are tagged as Items1 or Items2
	 * -Make sure Quest_Code, Quest1, Quest2 and Dialogue are on Player
	 * 
	 * TODO:
	 * -Consider adding an item count to quests in array so player knows how many items they have collected
	 * 
	 * Side Note: 
	 * -Didn't bother implementing items for Quest2 because depending how items are actually instantiated that will
	 * need to change anyways. Just made a dummyItems for Quest1 to make sure everything was working correctly.
	 * */

	//shows quests
	public bool showing = false;
	
	//creating GUI window size / position
	Rect winPos = new Rect (((Screen.width / 2) - 260), ((Screen.height / 2) - 150), 512, 256);
	
	//struct that has all the fields for a quest
	// also has all the getters and settesr for fields
	public struct quest
	{
		private string q_name;
		private string info;
		private bool has_quest;
		private bool completed;
		
		//so we're able to set values of structs
		public quest(string n, string i, bool has, bool complete)
		{
			q_name = n;
			info = i;
			has_quest = has;
			completed = complete;
		}
		
		//quest field getters and setters
		public string Name
		{
			get{return q_name;}
			set{ q_name = value;}
			
		}
		
		public string Info
		{
			get{return info;}
			set{ info = value;}
			
		}
		
		public bool Has
		{
			get{return has_quest;}
			set{ has_quest = value;}
			
		}
		
		public bool Complete
		{
			get{return completed;}
			set{ completed = value;}
			
		}
	}
	
	//Array for quests
	public quest[] QL = new quest[4]; 

	//creating quests
	quest Q1 = new quest ("Q1: ", "Go get Mathius' bag and return it to him!", false, false); 
	quest Q2 = new quest ("Q2: ", "Return Elizabeths items to her", false, false);
	quest Q3 = new quest ("Q3: ", "laugh at a bad joke", false, false); 
	quest Q4 = new quest ("Q4: ", "sing in the shower", false, false);
	
	//putting quests into an array list
	void CreateQuests()
	{
		
		QL [0] = Q1;
		QL [1] = Q2;
		QL [2] = Q3;
		QL [3] = Q4;
		
	}
	
	// Use this for initialization
	void Start () {
		
		CreateQuests ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate ()
	{  	
		if (Input.GetButtonDown ("Q")) {
			//if showing hide, else show GUI
			if (showing) {
				showing = false;
			}  else {
				showing = true;
			}
			
		}
	}
	
	void OnGUI ()
	{
		//if GUI is showing, setting size, title, etc.
		if (showing)
		{
			winPos = GUI.Window(2, winPos, QuestWindow, "Quest Journal");
		}
	}
	
	void QuestWindow(int ID)
	{

		//show all the quests that the player has that aren't completed yet
		for (int i =0; i < QL.Length; i++) {
			if((QL[i].Has) && !QL[i].Complete)
			{
				GUILayout.Box((QL[i]).Name + (QL[i]).Info);
			}
		}
		
	}
	
	
	/*
	****************************************************
	Side Note:
	When the character hits the QuestGiver after they've received the quest the mesage still appears. 
	Use GUIStyle later to change the look of them
	Add Scroll bar for Alpha version
	*/

}
