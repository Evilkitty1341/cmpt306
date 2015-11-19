using UnityEngine;
using System.Collections;

public class IconSkills : MonoBehaviour {
	public GameObject icons; 
	public GameObject player;
	
	void OnMouseEnter() {
		icons.GetComponent<SpriteRenderer> ().sprite = icons.gameObject.GetComponent<IconControl>().icons [2];
	}
	
	void OnMouseExit() {
		icons.GetComponent<SpriteRenderer> ().sprite = icons.gameObject.GetComponent<IconControl>().icons [0];
	}

	void OnMouseUp(){
		player.GetComponent<SkillTree> ().showing = !player.GetComponent<SkillTree> ().showing;
		
		if (player.GetComponent<PlayerStateGUI> ().showing = true) {
			player.GetComponent<PlayerStateGUI> ().showing = false;
		}
	}
}
