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

	void OnMouseUp(){
		player.GetComponent<PlayerStateGUI> ().showing = !player.GetComponent<PlayerStateGUI> ().showing;

		if (player.GetComponent<SkillTree> ().showing = true) {
			player.GetComponent<SkillTree> ().showing = false;
		}

		if (player.GetComponent<All_Quests> ().showing = true) {
			player.GetComponent<All_Quests> ().showing = false;
		}
	}
}
