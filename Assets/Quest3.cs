using UnityEngine;
using System.Collections;

public class Quest3 : MonoBehaviour {

	All_Quests AllQuests;
	
	//Variables made so the Dialogue script has something
	// to check conditions with
	public bool Has;		//when player gets quest set to true
	public bool Completed;	// when quest is completed set to true
	public bool Repeat; 	//so Dialogue doesn't repeat for a quest
	int identify = 3;
	
	//number of items the player currently has
	public int CurNumItems;
	
	//number of items needed to complete this quest
	public int ItemsTotal;
	
	public bool Finished;
	
	// Use this for initialization
	void Start () {
		
		CurNumItems = 0;
		ItemsTotal = 1;
		AllQuests = gameObject.GetComponent<All_Quests>();
		Has = false;
		Completed = false;
		Repeat = true;
		
		Finished = false;
		//item.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
		//Set Repeat to true so dialogue will work
		if (CurNumItems == ItemsTotal && Completed == false) {
			Repeat = true;
		}
		
		//if you've completed the quest they gave you and go collide with them again 
		//they give you your reward
		if (AllQuests.QL[2].Has == true && CurNumItems == ItemsTotal) 
		{
			Completed = true;
		}
		
	}
	
	//Quest Giver Collision
	// based on the fact that Quest Code is attached to the player NOT the camera   **IMPORTANT**
	void OnCollisionEnter2D(Collision2D col)
	{
		//If you run into quest giver and they haven't given you the quest yet, 
		//they give you the quest
		if (col.gameObject.tag == "QuestGiver3" && Finished == false && AllQuests.QL[2].Has == false) 
		{
			Has = true;	
			
		}
		
		//if you collide with an Item before you have the quest it won't do anything
		//but if you collide once you have the quest it destroys the item and adds 1 to the CurNumItems you have
		if (col.gameObject.tag == "Selina" && Finished == false)
		{
			CurNumItems += 1;
		}
	}
}
