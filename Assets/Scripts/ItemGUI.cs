using UnityEngine;
using System.Collections;

public class ItemGUI : MonoBehaviour {

	// connect stateGUI to stat class
	StatCollectionClass stat;

	ItemUpgrade up;
	
	//now we have 3 GUI used to handle open and close
	GameObject player;
	
	SkillTree skill;
	
	All_Quests quest;

	PlayerStateGUI ps;
	
	//set GUI to inactive first
	public bool showing = false;
	
	//creating GUI window size / position
	Rect winPos = new Rect (Screen.width/2-Screen.width/6, Screen.height/5, Screen.width/2-Screen.width/7, Screen.height-Screen.height/2);
	
	
	void Start()
	{
		player = this.gameObject;
		
		stat = player.GetComponent<StatCollectionClass >();
		
		skill = player.GetComponent<SkillTree >();
		
		quest = player.GetComponent<All_Quests> ();

		ps = player.GetComponent<PlayerStateGUI>();

		up = player.GetComponent<ItemUpgrade> ();

		
	}
	
	
	//create gui for each state
	void StateGui (int ID) {
		
		GUI.TextArea (new Rect (Screen.width/8, 50, Screen.width/7, 30), "Sword Level: " + up.SwordLevel);
		
		GUI.TextArea (new Rect (Screen.width/8, 90, Screen.width/7, 30), "Sword Damage: " + up.SwordDamage);
		
		GUI.TextArea (new Rect (Screen.width/8, 130, Screen.width/7, 30), "Bow Level: " + up.BowLevel);
		
		GUI.TextArea (new Rect (Screen.width/8, 170, Screen.width/7, 30), "Bow Damage: " + up.BowDamage);
		
		GUI.TextArea (new Rect (Screen.width/8, 210, Screen.width/7, 30), "Armor Level: " + up.ArmorLevel);
		
		GUI.TextArea (new Rect (Screen.width/8, 250, Screen.width/7, 30), "Armor Defend: " + up.ArmorDamage);
		

		
	}
	
	
	void OnGUI () {
		
		//show GUI with the showing's value
		if (showing)
		{
			winPos = GUI.Window(2, winPos, StateGui, "Item");
		}
		
	}
	
	void Update ()
	{     
		//if the key is pressed and the GUI is showing, hide it
		// else show the GUI
		if (Input.GetButtonDown ("I")) {
			//set showing to true if false, if false turn it to true
			showing = !showing;
			
			//if other GUI actived turn it off
			
			if(skill.showing==true)
			{
				skill.showing=false;
				
			}
			
			if(quest.showing==true)
			{
				quest.showing=false;
			}

			if(ps.showing==true)
			{
				ps.showing=false;
			}
			
			
		}
	}
}
