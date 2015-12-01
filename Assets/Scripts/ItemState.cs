using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemState : MonoBehaviour {

	// this class use to find out is the item equiped
	public StatCollectionClass stat;

	public ItemUpgrade up;
	
	public GameObject txt;
	
	void Start () {

	}
	
	
	void Update () {
		
		//when armor equiped 
		if (stat.ArmorEquip == true) {
			txt.GetComponent<TextMesh>().text="Armor: defend + "+up.ArmorDamage+"\n"+"(O to change items)";
		}

		//when sword equiped
		if (stat.SwordEquip == true) {
			txt.GetComponent<TextMesh>().text = "Sword: damage +"+up.SwordDamage+"\n"+"(O to change items)";
		}

		//when bow equiped
		if (stat.BowEquip == true) {
			txt.GetComponent<TextMesh>().text = "Bow: damage + " +up.BowDamage+"\n"+"(O to change items)";
		}
	}
}
