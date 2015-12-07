using UnityEngine;
using System.Collections;

public class ItemArmor : MonoBehaviour {
	
	StatCollectionClass stat;
	
	GameObject player;
	
	ItemUpgrade Up;
	
	//give armor a value of defend
	public float ArmorDef = 1f;
	
	void Start(){
		
	}
	
	void OnTriggerEnter2D(Collider2D other) {  
		
		
		
		// If player collided with item armor
		if (other.tag == "Player") {
			
			stat=other.GetComponent<StatCollectionClass>();
			
			// set item armor unlocked
			stat.itemArmor = true;
			
			Up = other.GetComponent<ItemUpgrade>();
			
			Up.ArmorLevel++;
			
			Up.ArmorDamage+=1f;
			
			//if not equipment now just equip it
			if(stat.SwordEquip ==false && stat.BowEquip==false)
			{
				//add player defend with armor defend
				stat.defend+=Up.ArmorDamage;
				//set armor equip true
				stat.ArmorEquip = true; 	
			}
			
			// Destroy the item
			Destroy(this.gameObject);
			
		}
		
	}
}
