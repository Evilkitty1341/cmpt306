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
	public int maxEnergyBallLv=5;
	//xp cost
	public int EnergyBallPrice=30;
	//mana cost
	public float EnergyBallMpCost = 5f;
	//use for skill grow
	int i =0;
	
	//sample of second skill
	public int maxFireBreathLv=5;
	public int FireBreathPrice=60;
	public float FireBreathMpCost = 10f;
	public float FireBreathDamage = 120f;
	int j =0;
	
	//sample of third skill
	public int maxSunStrikeLv=5;
	public int SunStrikePrice=120;
	public float SunStrikeMpCost = 20f;
	public float SunStrikeDamage = 300f;
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
		if (GUI.Button (new Rect (Screen.width/2-Screen.width/7, 50, Screen.width/7, 30), "Energy Ball Lv" + i)) {
			//skill level must lower than max level
			if (i < maxEnergyBallLv) {
				// player's xp value max higher than skill xp cost
				if (stat.xp >= EnergyBallPrice) {
					//set skill actived
					stat.EnergyBallUnlocked = true;
					//cost the player's xp by skill xp cost value
					stat.xp -= EnergyBallPrice;
					//level up
					i++;
					//each skill value increase with the level
					EnergyBallPrice *=i+1;
					EnergyBallMpCost *=i;
					stat.EnergyBalldamage+=10f;
					
					
					
					
				} else if (stat.xp < EnergyBallPrice) {
					Debug.Log ("not enouph xp");
				}
			} else {
				Debug.Log ("max skill level");
			}
			
		}
		//same as 1st skill
		if (GUI.Button (new Rect (Screen.width/2-Screen.width/7, 150, Screen.width/7, 30), "Fire Breath Lv" + j)) {
			if (i == maxEnergyBallLv) {
				if (j < maxFireBreathLv) {
					
					if (stat.xp >= FireBreathPrice) {
						stat.FireBreathUnlocked = true;
						stat.xp -= FireBreathPrice;
						j++;
						FireBreathPrice *= j+1;
						FireBreathMpCost *=j;
						FireBreathDamage *=j;
						
						
						
					} else if (stat.xp < FireBreathPrice) {
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
					
					if (stat.xp >= SunStrikePrice) {
						stat.SunStrikeUnlocked = true;
						stat.xp -= SunStrikePrice;
						k++;
						SunStrikePrice *= k+1;
						SunStrikeMpCost *=k;
						SunStrikeDamage *=k;
						
						
						
					} else if (stat.xp < SunStrikePrice) {
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
