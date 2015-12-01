using UnityEngine;
using System.Collections;

public class RockDestroy : MonoBehaviour {


	public GameObject Sword;

	public GameObject Bow;

	public GameObject Armor;

	public ItemUpgrade Up;

	 //Use this for initialization
	void PutItems () {

		int number;

		number = Random.Range (0, 10);

		//sword<5
//		if (Up.SwordTotal < 5) {
//
//			//sowrd and bow <5
//			if (Up.BowTotal < 5) {
//
//				//all<5
//				if (Up.ArmorTotal < 5) {
//
//					number = number;
//
//				}
//				//sowrd and bow <5 armoer >5
//				else {
//					if (number == 2) {
//						number = 3;
//					}
//				}
//			} 
//			//sword<5 bow>5 armore<5
//			else if (Up.ArmorTotal < 5) {
//				
//				if (number == 1) {
//					number = 3;
//				}
//			} 
//			//sowrd<5 bow>5 armor>5
//			else {
//				if (number == 1 || number == 2) {
//					number = 3;
//				}
//			}
//		}
//		//sowrd>5 bow<5
//		else if (Up.BowTotal < 5) {
//
//			//sowrd>5 b<5 a<5
//			if (Up.ArmorTotal < 5) {
//			
//				if (number == 0) {
//					number = 3;
//				}
//			} 
//			//sowrd>5 b<5 a>5
//			else {
//
//				if (number == 0 || number == 2) {
//					number = 3;
//				}
//			}
//		
//		} 
//		//s>5 b>5
//		else {
//			//s>5 b>5 a<5
//			if (Up.ArmorTotal < 5) {
//				if (number == 0 || number == 1) {
//					number = 3;
//				}
//			} else {
//				number = 3;
//			}
//		}

	



		if (number == 0) {
		
			Instantiate (Sword, transform.position,transform.rotation);

			Up.SwordTotal++;
		}

		else if (number == 1) {
			
			Instantiate (Bow, transform.position,transform.rotation);

			Up.BowTotal++;
		}

		else if (number == 2) {
			
			Instantiate (Armor, transform.position,transform.rotation);

			Up.ArmorTotal++;
		}

		else  {

			return;

		}

	
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other) {

//		Up=other.GetComponentInParent<ItemUpgrade> ();

		if (other.tag == "Skill") {



			Destroy(this.gameObject);

			PutItems();


		
		}
	

	}
}
