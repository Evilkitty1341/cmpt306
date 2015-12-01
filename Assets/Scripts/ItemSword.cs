using UnityEngine;
using System.Collections;

public class ItemSword : MonoBehaviour {
	
    StatCollectionClass stat;

	GameObject player;

	ItemUpgrade Up;
	
	//give sowrd a damage
	public float SwordDamage = 100f;

	void Start(){



	}
	
	void OnTriggerEnter2D(Collider2D other) {  
		
		
		
		// If player collided with item Sword
		if (other.tag == "Player") {

			stat=other.GetComponent<StatCollectionClass>();

			Up = other.GetComponent<ItemUpgrade>();

			Destroy(this.gameObject);


			// set item sword unlocked
			stat.itemSword = true;

			Up.SwordLevel++;

			Up.SwordDamage+=100f;
			
			//if no equipment now just equip sword to player
			if(stat.ArmorEquip==false &&stat.BowEquip==false)
			{
				
				//add player attack damage with SwordDamage
				stat.damage+=Up.SwordDamage;
				
				//set sword equip to true
				stat.SwordEquip= true;
			}


			}
			
			// Destroy the item

			
			
			

	}
}
