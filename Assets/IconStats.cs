using UnityEngine;
using System.Collections;

public class IconStats : MonoBehaviour {
	public GameObject icons; 
	public GameObject player;
	
	void OnMouseEnter() {
		icons.GetComponent<SpriteRenderer> ().sprite = icons.gameObject.GetComponent<IconControl>().icons [1];
	}
	
	void OnMouseExit() {
		icons.GetComponent<SpriteRenderer> ().sprite = icons.gameObject.GetComponent<IconControl>().icons [0];
	}

	void OnMouseUp() {

		player.GetComponent<StatGUI>().showing = !player.GetComponent<StatGUI>().showing;

		//make other GUI's false so if they're currently open they close
		player.GetComponent<SkillTree>().showing = false;
		player.GetComponent<All_Quests>().showing = false;
	}
}
