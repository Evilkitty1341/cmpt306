using UnityEngine;
using System.Collections;

public class IconQuests : MonoBehaviour {
	public GameObject icons;
	public GameObject player;
	
	void OnMouseEnter() {
		icons.GetComponent<SpriteRenderer> ().sprite = icons.gameObject.GetComponent<IconControl>().icons [4];
	}
	
	void OnMouseExit() {
		icons.GetComponent<SpriteRenderer> ().sprite = icons.gameObject.GetComponent<IconControl>().icons [0];
	}

	void OnMouseUp() {
		player.GetComponent<All_Quests>().showing = !player.GetComponent<All_Quests>().showing;

		if (player.GetComponent<PlayerStateGUI> ().showing = true) {
			player.GetComponent<PlayerStateGUI> ().showing = false;
		}
		
		if (player.GetComponent<SkillTree> ().showing = true) {
			player.GetComponent<SkillTree> ().showing = false;
		}

		if (player.GetComponent<ItemGUI> ().showing == true) {
			player.GetComponent<ItemGUI> ().showing =false;
		}
	}
}
