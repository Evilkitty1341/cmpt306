using UnityEngine;
using System.Collections;

public class ItemGUIManager : MonoBehaviour {
	
	//access to class State
	public StatCollectionClass stat;
	
	//3 item we need to manager now
	public GameObject Sword;
	
	public GameObject Armor;
	
	public GameObject Bow;
	
	void Update ()
	{     

		// show the picture on the scene if player got the item

		if (stat.itemSword) {
			Sword.GetComponent<SpriteRenderer> ().color = Color.white;
		}

		if (stat.itemArmor) {
			Armor.GetComponent<SpriteRenderer> ().color = Color.white;
		}

		if (stat.itemBow) {

			Bow.GetComponent<SpriteRenderer> ().color = Color.white;
		}
		
	}
}
