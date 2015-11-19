using UnityEngine;
using System.Collections;

public class itemsControl : MonoBehaviour {

	public StatCollectionClass stat;
	public GameObject items; 
	public GameObject text;
	
	void OnMouseEnter() {
		text.SetActive (true);
	}
	
	void OnMouseExit() {
		text.SetActive(false);
	}
	
	void OnMouseUp(){
		//when sword is already equiped
		if(stat.SwordEquip==true)
		{
			//and we have item armor unlocked
			if(stat.itemArmor == true)
			{
				//take off sword
				stat.SwordEquip =false;
				
				//equip armor
				stat.ArmorEquip =true;
				
				//adjust player state
				stat.damage-=100f;
				
				stat.defend+=50f;
				
				//adjust item show on the scene

			}
			
			//player dont unlocked armor but unlocked bow
			else if(stat.itemBow==true)
			{
				//adjust player state
				stat.SwordEquip =false;
				
				stat.BowEquip =true;
				
				stat.damage-=50f;
				

			}
		}
		
		//player equiped armor
		else if(stat.ArmorEquip==true)
		{
			//player have item bow 
			if(stat.itemBow==true)
			{
				//adjust player state
				stat.ArmorEquip =false;
				
				stat.BowEquip =true;
				
				stat.damage+=50f;
				
				stat.defend-=50f;
				

			}
			//player dont have bow but have sword
			else if(stat.itemSword ==true)
			{
				//adjust player state
				stat.ArmorEquip =false;
				
				stat.SwordEquip =true;
				
				stat.damage+=100f;
				
				stat.defend-=50;
				

			}
		}
		
		// player equiped bow
		else if(stat.BowEquip==true)
		{
			//player have item sword 
			if(stat.itemSword==true)
			{
				//adjust player state
				stat.BowEquip =false;
				
				stat.SwordEquip =true;
				
				stat.damage+=50f;
				

			}
			//player dont have item sword but have armor
			else if(stat.itemArmor==true)
			{
				//adjust state
				stat.BowEquip =false;
				
				stat.ArmorEquip =true;
				
				stat.damage-=50f;
				
				stat.defend+=50f;
				

			}
		}


	
}
}

