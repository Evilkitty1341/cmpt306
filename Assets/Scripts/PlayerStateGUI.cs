using UnityEngine;
using System.Collections;

public class PlayerStateGUI : MonoBehaviour {
	
	// connect stateGUI to stat class
	StatCollectionClass stat;
	
	GameObject player;

	SkillTree skill;

	public bool showing = false;
	
	//creating GUI window size / position
	Rect winPos = new Rect (Screen.width/13, Screen.height/5, Screen.width-Screen.width/6, Screen.height-Screen.height/2);


	void Start()
	{
		player = GameObject.FindWithTag ("Player");
		
		stat = player.GetComponent<StatCollectionClass >();

		skill = player.GetComponent<SkillTree >();
		
	}
	// create GUI Button on panel
	
	//create gui for each state
	void StateGui (int ID) {
		
		GUI.TextArea (new Rect (Screen.width/2-Screen.width/7, 50, Screen.width/7, 30), "Level: " + stat.playerLevel);
		
		GUI.TextArea (new Rect (Screen.width/2-Screen.width/7, 90, Screen.width/7, 30), "Xp: " + stat.xp);
		
		GUI.TextArea (new Rect (Screen.width/2-Screen.width/7, 130, Screen.width/7, 30), "Health: " + stat.health);
		
		GUI.TextArea (new Rect (Screen.width/2-Screen.width/7, 170, Screen.width/7, 30), "Mana: " + stat.mana);
		
		GUI.TextArea (new Rect (Screen.width/2-Screen.width/7, 210, Screen.width/7, 30), "Damage: " + stat.damage);
		
		GUI.TextArea (new Rect (Screen.width/2-Screen.width/7, 250, Screen.width/7, 30), "defend: " + stat.defend);
		
		GUI.TextArea (new Rect (Screen.width/2-Screen.width/7, 290, Screen.width/7, 30), "Strength: " + stat.strength);
		
		GUI.TextArea (new Rect (Screen.width/2-Screen.width/7, 330, Screen.width/7, 30), "Intellect: " + stat.intellect);
		
	}

	void OnGUI () {
		
		
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
			
			
		}
	}

	
}
