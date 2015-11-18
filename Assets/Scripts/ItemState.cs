using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemState : MonoBehaviour {
	// this class use to describe item's state
	public StatCollectionClass stat;
	
	public GameObject txt;
	
	void Start () {

	}
	
	
	void Update () {
		
		
		if (stat.ArmorEquip == true) {
			txt.GetComponent<TextMesh>().text="Armor: defend +50\n"+"(O to change items)";
		}
		
		if (stat.SwordEquip == true) {
			txt.GetComponent<TextMesh>().text = "Sword: damage +100\n"+"(O to change items)";
		}
		
		if (stat.BowEquip == true) {
			txt.GetComponent<TextMesh>().text = "Bow: damage +50\n"+"(O to change items)";
		}
	}
}
