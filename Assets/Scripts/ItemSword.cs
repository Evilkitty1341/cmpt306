using UnityEngine;
using System.Collections;

public class ItemSword : MonoBehaviour {
	
	StatCollectionClass stat;

	GameObject player;
	
	//give sowrd a damage
	public float SwordDamage = 100f;

	void Start(){

		player = GameObject.FindWithTag ("Player");
		
		stat = player.GetComponent<StatCollectionClass >();

	}
	
	void OnTriggerEnter2D(Collider2D other) {  
		
		
		
		// If player collided with item Sword
		if (other.tag == "Player") {
			
			// set item sword unlocked
			stat.itemSword = true;
			
			//if no equipment now just equip sword to player
			if(stat.ArmorEquip==false &&stat.BowEquip==false)
			{
				
				//add player attack damage with SwordDamage
				stat.damage+=SwordDamage;
				
				//set sword equip to true
				stat.SwordEquip= true;
			}
			
			// Destroy the item
			Destroy(this.gameObject);
			
			
			
		}
	}
}
