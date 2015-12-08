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

	//use to control all GUI when open 1 of them
	GameObject player;

	PlayerStateGUI psg;

	SkillTree skill;

	ItemGUI item;

	//change font
	public Font chosenFont;

	//counter for quests
	Quest1 Q1Script;
	Quest2 Q2Script;
	Quest3 Q3Script;
	Quest4 Q4Script;

	RotateTo R;

	//Array for quest items
	public int[] QI = new int[5]; 


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
		private int id;
		
		//so we're able to set values of structs
		public quest(string n, string i, bool has, bool complete, int identify)
		{
			q_name = n;
			info = i;
			has_quest = has;
			completed = complete;
			id = identify;
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

		public int Identify
		{
			get{return id;}
			set{id = value;}
			
		}

	}
	
	//Array for quests
	public quest[] QL = new quest[5]; 

	//creating quests
	quest Q1 = new quest ("Q1: ", "Return Mathius' bag!", false, false, 1); 
	quest Q2 = new quest ("Q2: ", "Gain entry into the town!", false, false, 2);
	quest Q3 = new quest ("Q3: ", "Talk to Selina", false, false, 3); 
	quest Q4 = new quest ("Q4: ", "Kill General E. Speaking", false, false, 4);


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

		//find out all GUI we need to handle
		player = GameObject.FindWithTag ("Player");
		
		psg = player.GetComponent<PlayerStateGUI> ();
		
		skill = player.GetComponent<SkillTree> ();

		item = player.GetComponent<ItemGUI> ();

		Q1Script = this.gameObject.GetComponent<Quest1>();
		Q2Script = this.gameObject.GetComponent<Quest2>();
		Q3Script = this.gameObject.GetComponent<Quest3>();
		Q4Script = this.gameObject.GetComponent<Quest4>();

		R = GameObject.Find ("Arrow").GetComponent<RotateTo>();

	}
	
	// Update is called once per frame
	void Update () {

		//for minimap arrow
		setRotationString ();

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

			//if other GUI actived when open this GUI turn it off
			if(psg.showing==true)
			{
				psg.showing=false;
				
			}
			
			if(skill.showing==true)
			{
				skill.showing=false;
			}

			if(item.showing==true)
			{
				item.showing=false;
			}
			
		}
	}
	
	void OnGUI ()
	{
		//if GUI is showing, setting size, title, etc.
		if (showing)
		{
			GUI.skin.font = chosenFont;
			winPos = GUI.Window(2, winPos, QuestWindow, "Quest Journal");
		}
	}
	
	void QuestWindow(int ID)
	{
		//show all the quests that the player has that aren't completed yet
		int j = 0;
		for (int i =0; i < QL.Length; i++) {
			if((QL[i].Has) && !QL[i].Complete)
			{
				//Make sure it displays proper cur num items / total nums 
				//is right for the quests
				if(QL[i].Identify == 1)
				{
					QI [0] = Q1Script.CurNumItems;
					QI [1] = Q1Script.ItemsTotal;
				}
				else if(QL[i].Identify == 2)
				{
					QI [0] = Q2Script.CurNumItems;
					QI [1] = Q2Script.ItemsTotal;
				}
				else if(QL[i].Identify == 3)
				{
					QI [0] = Q3Script.CurNumItems;
					QI [1] = Q3Script.ItemsTotal;
				}
				else if(QL[i].Identify == 4)
				{
					QI [0] = Q4Script.CurNumItems;
					QI [1] = Q4Script.ItemsTotal;
				}
				GUILayout.Box((QL[i]).Name + (QL[i]).Info + " " + QI[0] + "/" + QI[1]);
			}
		}
		
	}


	/************
	* To Be Continued...
	****************/
	//Set the rotation for the Arrow towards the next thing in here!
	void setRotationString()
	{
		if (QL [0].Has && !Q1Script.Completed && Q1Script.CurNumItems == 0) 
		{
			R.setRotateTo ("Items1");
		} else if (QL [0].Has && !Q1Script.Completed && Q1Script.CurNumItems == 1) 
		{
			R.setRotateTo ("QuestGiver1");
		} else if (QL [0].Complete && !Q2Script.Completed && Q2Script.CurNumItems == 0) 
		{
			R.setRotateTo ("QuestGiver2");
		}
		else if(QL [0].Complete && Q2Script.Finished1 && !Q2Script.Finished2 )
		{
			R.setRotateTo("Guard1");
		}
		else if(Q2Script.Finished2 && Q1Script.Finished && !Q3Script.Has)
		{
			R.setRotateTo("Gigabyte");
		}
		else if(Q3Script.Has && !Q3Script.Completed)
		{
			R.setRotateTo("Selina");
		}
		else if(Q1Script.Completed && Q2Script.Completed && Q3Script.Completed)
		{
			R.setRotateTo("Guard2");
		}

	}

}
