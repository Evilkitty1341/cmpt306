using UnityEngine;
using System.Collections;

public class SkillTree: MonoBehaviour {
	
	// connect skilltree to stat class
	StatCollectionClass stat;

	GameObject player;

	// now have 3 GUI need other 2 GUI to handle open and close
    
	
	PlayerStateGUI psg;

	All_Quests quest;

	ItemGUI item;
			

	

	// use to check if the GUI is actived


	public bool showing = false;
	
	//creating GUI window size / position
	Rect winPos = new Rect (Screen.width/13, Screen.height/5, Screen.width-Screen.width/6, Screen.height-Screen.height/2);
	
	//skill level
	public int maxFireBallLv=5;
	//mana cost
	public float FireBallMpCost = 10f;
	
	public float FireBallDamage = 3f;
	//use for skill grow
	int i =1;
	
	//sample of second skill
	public int maxFireBreathLv=5;
	public float FireBreathMpCost = 20f;

	public float FireBreathDamage = 5f;

	int j =0;
	
	//sample of third skill
	public int maxSunStrikeLv=5;
	public float SunStrikeMpCost = 40f;

	public float SunStrikeDamage = 10f;
	int k =0;
	
	//connect to each script
	void Start()
	{
		player = this.gameObject;
		
		stat = player.GetComponent<StatCollectionClass >();
		
		psg = player.GetComponent<PlayerStateGUI> ();
		
		quest = player.GetComponent<All_Quests> ();
		
		item = player.GetComponent<ItemGUI>();
		
		
		
	}
	
	// create GUI Button on panel
	void something (int ID) {
		//set the location of button
		if (GUI.Button (new Rect (Screen.width/2-Screen.width/7, 50, Screen.width/7, 30), "Fire Ball Lv" + i)) {
			//skill level must lower than max level
			if (i < maxFireBallLv) {
				// player's xp value max higher than skill xp cost
				if (stat.skillPoint >0) {


					//cost the player's skill point by 1
					stat.skillPoint--;
					//level up
					i++;
					//each skill value increase with the level

					FireBallMpCost +=i;
					FireBallDamage+=1f;
					
					
					
					
				} else if (stat.skillPoint < 1) {
					Debug.Log ("not enouph xp");
				}
			} else {
				Debug.Log ("max skill level");
			}
			
		}

		//same as 1st skill
		if (GUI.Button (new Rect (Screen.width/2-Screen.width/7, 150, Screen.width/7, 30), "Fire Breath Lv" + j)) {
			if (i == maxFireBallLv) {
				if (j < maxFireBreathLv) {
					
					if (stat.skillPoint >0) {
						stat.FireBreathUnlocked = true;
						stat.skillPoint--;
						j++;

						FireBreathMpCost +=j;
						FireBreathDamage +=2f;;
						
						
						
					} else if (stat.skillPoint < 1) {
						Debug.Log ("not enouph xp");
					}
				} else {
					Debug.Log ("max skill level");
				}
				
			}
			else 
			{
				Debug.Log (" need previous skill");
			}
		}
		
		//same as 1st skill
		if (GUI.Button (new Rect (Screen.width/2-Screen.width/7, 250, Screen.width/7, 30), "Sun Strike Lv" + k)) {
			
			if (j == maxFireBreathLv) {
				
				if (k < maxSunStrikeLv) {
					
					if (stat.skillPoint >0) {
						stat.SunStrikeUnlocked = true;
						stat.skillPoint--;
						k++;

						SunStrikeMpCost +=k;
						SunStrikeDamage +=5f;
						
						
						
					} else if (stat.skillPoint < 1) {
						Debug.Log ("not enouph xp");
					}
				} else {
					Debug.Log ("max skill level");
				}
				
			}
			else 
			{
				Debug.Log (" need previous skill");
			}
		}

		GUI.TextArea (new Rect (Screen.width/8, 50, Screen.width/7, 30), "Skill Point: " + stat.skillPoint);
	}
	

		void OnGUI () {
		
		//check value of showing determing show GUI or not
			if (showing)
			{
			winPos = GUI.Window(2, winPos, something, "Skill Tree");
			}
		
		}


		void Update ()
		{     
			
			//if the key is pressed and the GUI is showing, hide it
			// else show the GUI
			if (Input.GetButtonDown ("K")) {
				//set showing to true if false, if false turn it to true
				showing = !showing;
				
				
				//if other GUI actived turn it off
				if(psg.showing==true)
				{
				psg.showing=false;
					
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
