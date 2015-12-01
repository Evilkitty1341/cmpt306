using UnityEngine;
using System.Collections;

public class IconItems : MonoBehaviour {
	public GameObject icons; 
	public GameObject player;
	
	void OnMouseEnter() {
		icons.GetComponent<SpriteRenderer> ().sprite = icons.gameObject.GetComponent<IconControl>().icons [3];
	}
	
	void OnMouseExit() {
		icons.GetComponent<SpriteRenderer> ().sprite = icons.gameObject.GetComponent<IconControl>().icons [0];
	}

	void OnMouseUp(){
		player.GetComponent<ItemGUI> ().showing = !player.GetComponent<ItemGUI> ().showing;
		
		if (player.GetComponent<SkillTree> ().showing = true) {
			player.GetComponent<SkillTree> ().showing = false;
		}
		
		if (player.GetComponent<All_Quests> ().showing = true) {
			player.GetComponent<All_Quests> ().showing = false;
		}

		if (player.GetComponent<PlayerStateGUI> ().showing = true) {
			player.GetComponent<PlayerStateGUI> ().showing = false;
		}

	}
}
