using UnityEngine;
using System.Collections;

public class ItemGUIManager : MonoBehaviour {
	
	//access to class State
	public StatCollectionClass stat;
	
	public ItemUpgrade item;
	
	//3 item we need to manager now
	public GameObject Sword;
	
	public GameObject Armor;
	
	public GameObject Bow;
	
	void Update ()
	{     
		
		// show the picture on the scene if player got the item
		//change color with each level
		if (stat.itemSword) {

			if(item.SwordLevel==1)
			{
				Sword.GetComponent<SpriteRenderer> ().color = Color.white;
			}
			if(item.SwordLevel==2)
			{
				Sword.GetComponent<SpriteRenderer> ().color = Color.green;
			}
			if(item.SwordLevel==3)
			{
				Sword.GetComponent<SpriteRenderer> ().color = Color.blue;
			}
			if(item.SwordLevel==4)
			{
				Sword.GetComponent<SpriteRenderer> ().color = Color.yellow;
			}
			if(item.SwordLevel==5)
			{
				Sword.GetComponent<SpriteRenderer> ().color = Color.red;
			}

		}
		
		if (stat.itemArmor) {
			if(item.ArmorLevel==1)
			{
				Armor.GetComponent<SpriteRenderer> ().color = Color.white;
			}
			if(item.ArmorLevel==2)
			{
				Armor.GetComponent<SpriteRenderer> ().color = Color.green;
			}
			if(item.ArmorLevel==3)
			{
				Armor.GetComponent<SpriteRenderer> ().color = Color.blue;
			}
			if(item.ArmorLevel==4)
			{
				Armor.GetComponent<SpriteRenderer> ().color = Color.yellow;
			}
			if(item.ArmorLevel==5)
			{
				Armor.GetComponent<SpriteRenderer> ().color = Color.red;
			}
		}
		
		
		if (stat.itemBow) {
			
			if(item.BowLevel==1)
			{
				Bow.GetComponent<SpriteRenderer> ().color = Color.white;
			}
			if(item.BowLevel==2)
			{
				Bow.GetComponent<SpriteRenderer> ().color = Color.green;
			}
			if(item.BowLevel==3)
			{
				Bow.GetComponent<SpriteRenderer> ().color = Color.blue;
			}
			if(item.BowLevel==4)
			{
				Bow.GetComponent<SpriteRenderer> ().color = Color.yellow;
			}
			if(item.BowLevel==5)
			{
				Bow.GetComponent<SpriteRenderer> ().color = Color.red;
			}
		}
		
	}
}

