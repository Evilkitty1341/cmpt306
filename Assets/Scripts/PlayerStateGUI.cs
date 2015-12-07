using UnityEngine;
using System.Collections;

public class PlayerStateGUI : MonoBehaviour {
	
	// connect stateGUI to stat class
	StatCollectionClass stat;

	//now we have 3 GUI used to handle open and close
	GameObject player;

	SkillTree skill;

	All_Quests quest;

	ItemGUI item;

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

		item = player.GetComponent<ItemGUI>();
		
	}

	
	//create gui for each state
	void StateGui (int ID) {
		
		GUI.TextArea (new Rect (Screen.width/8, 50, Screen.width/7, 30), "Level: " + stat.playerLevel);
		
		GUI.TextArea (new Rect (Screen.width/8, 90, Screen.width/7, 30), "Xp: " + stat.xp+"/"+stat.xpNextLevel);
		
//		GUI.TextArea (new Rect (Screen.width/8, 130, Screen.width/7, 30), "Health: " + stat.health+"/"+stat.initialHealth);
//		
//		GUI.TextArea (new Rect (Screen.width/8, 170, Screen.width/7, 30), "Mana: " + stat.mana+"/"+stat.initialMana);

		GUI.TextArea (new Rect (Screen.width/8, 130, Screen.width/7, 30), "Damage: " + stat.damage);
		
		GUI.TextArea (new Rect (Screen.width/8, 170, Screen.width/7, 30), "defend: " + stat.defend);
		
		GUI.TextArea (new Rect (Screen.width/8, 210, Screen.width/7, 30), "Strength: " + stat.strength);
		
		GUI.TextArea (new Rect (Screen.width/8, 250, Screen.width/7, 30), "Intellect: " + stat.intellect);

		GUI.TextArea (new Rect (Screen.width/8, 290, Screen.width/7, 30), "agility: " + stat.agility);
		
	}


	void OnGUI () {

		//show GUI with the showing's value
		if (showing)
		{
			winPos = GUI.Window(2, winPos, StateGui, "Player State");
		}
		
	}

	void Update ()
	{     
		//if the key is pressed and the GUI is showing, hide it
		// else show the GUI
		if (Input.GetButtonDown ("C")) {
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

			if(item.showing==true)
			{
				item.showing=false;
			}
			
			
		}
	}

	
}
