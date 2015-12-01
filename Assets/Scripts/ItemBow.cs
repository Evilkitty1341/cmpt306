using UnityEngine;
using System.Collections;

public class ItemBow : MonoBehaviour {
	
	
	StatCollectionClass stat;
	
	GameObject player;

	ItemUpgrade Up;
	
	//give a damage value to bow
	public float BowDamage = 50f;

	void Start(){
		

		
	}
	void OnTriggerEnter2D(Collider2D other) {  
		
		
		
		// If player collided with bow
		if (other.tag == "Player") {
			stat=other.GetComponent<StatCollectionClass>();
			Up = other.GetComponent<ItemUpgrade>();
				// set item bow unlocked
				stat .itemBow = true;

			Up.BowLevel++;

			Up.BowDamage+=50;
			
				if (stat .ArmorEquip == false && stat .SwordEquip == false) {
				
					//add player attack damage with bow damage
					stat .damage += Up.BowDamage;
				
					stat .BowEquip = true;
				}
			}
		
			// Destroy the item
			Destroy(this.gameObject);
			
			
			
		}
	}

