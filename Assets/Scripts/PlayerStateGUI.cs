using UnityEngine;
using System.Collections;

public class PlayerStateGUI : MonoBehaviour {
	
	// connect stateGUI to stat class
	public StatCollectionClass stat;

	bool showing = false;
	
	//creating GUI window size / position
	Rect winPos = new Rect (0, 0, Screen.width, Screen.height-Screen.height/8);

	
	//create gui for each state
	void OnGUI () {
		
		GUI.TextArea (new Rect (Screen.width/2-Screen.width/24, 50, 120, 30), "Level" + stat.playerLevel);
		
		GUI.TextArea (new Rect (Screen.width/2-Screen.width/24, 90, 120, 30), "Xp" + stat.xp);
		
		GUI.TextArea (new Rect (Screen.width/2-Screen.width/24, 130, 120, 30), "Health: " + stat.health);
		
		GUI.TextArea (new Rect (Screen.width/2-Screen.width/24, 170, 120, 30), "Mana: " + stat.mana);
		
		GUI.TextArea (new Rect (Screen.width/2-Screen.width/24, 210, 120, 30), "Damage: " + stat.damage);
		
		GUI.TextArea (new Rect (Screen.width/2-Screen.width/24, 250, 120, 30), "defend: " + stat.defend);
		
		GUI.TextArea (new Rect (Screen.width/2-Screen.width/24, 290, 120, 30), "Strength: " + stat.strength);
		
		GUI.TextArea (new Rect (Screen.width/2-Screen.width/24, 330, 120, 30), "Intellect: " + stat.intellect);
		
	}
	
}
