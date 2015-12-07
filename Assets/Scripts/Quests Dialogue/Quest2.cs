using UnityEngine;
using System.Collections;

public class Quest2 : MonoBehaviour {

	All_Quests AllQuests;
	Quest1 Q1;



	//Variables needed for the Dialogue script to have
	//something to check conditions with
	public bool Has;		//if player has this quest, set to true
	public bool Completed;	//if player has completed a quest
	public bool Repeat;		//so the Dialogue won't repeat
	int identify = 2;
	//number of items the player currently has
	public int CurNumItems;
	
	//totaly number of items player needs to complete this quest
	public int ItemsTotal;
	public bool Finished1;
	public bool Finished2;

	//Gate to open
	public GameObject gate;

	//talked to Elizabeth bool
	private bool goToTown;

	//bool to make gate animation stop for good
	bool DoneAnim;

	//Hacky fix for numItems
	bool items1;
	bool items2;

	//Gate animation
//	Animator anim;
	
	// Use this for initialization
	void Start () {

		DoneAnim = false;
		CurNumItems = 0;
		ItemsTotal = 2;

		items1 = false;
		items2 = false;

		//End dialogue for characters
		//that speak for Quest2
		Finished1 = false;
		Finished2 = false;

		//set to true in Dialogue after
		//you talk to Elizabeth
		goToTown = false;

		AllQuests = gameObject.GetComponent<All_Quests>();
		Has = false;
		Completed = false;
		Repeat = true;

		Q1 = this.gameObject.GetComponent<Quest1>();

		//anim = gate.GetComponent<Animator>();

		
	}
	
	// Update is called once per frame
	void Update () {

		//Set Repeat to true so dialogue will work
		if (CurNumItems == ItemsTotal && Completed == false) {
			Repeat = true;
		}

		//if you've completed the quest they gave you and go collide with them again 
		//they give you your reward
		if (CurNumItems == ItemsTotal && goToTown == true && Q1.Finished == true ) 
		{
			Completed = true;
			
		}

		if (this.Finished2 == true) {
			//anim.SetBool("Open", true);
			gate.SetActive(false);
		}

		numItems ();
		
	}
	
	//Quest Giver Collision
	// based on the fact that Quest Code is attached to the player NOT the camera   **IMPORTANT**
	void OnCollisionEnter2D(Collision2D col)
	{
		//If you run into quest giver and they haven't given you the quest yet, 
		//they give you the quest
		if (col.gameObject.tag == "QuestGiver2" && Finished1 == false && AllQuests.QL[1].Has == false) 
		{
			
			Has = true;
			
		}

		//if you collide with an Item before you have the quest it won't do anything
		//but if you collide once you have the quest it destroys the item and adds 1 to the CurNumItems you have
		if (col.gameObject.tag == "Guard1" && Finished2 == false)
		{

		}
	}

	public void setGoToTown(bool talkedToE)
	{
		this.goToTown = talkedToE;
	}

	public bool getGoToTown()
	{
		return goToTown;
	}

	void numItems()
	{
		if(Q1.Finished == true && Finished2 == false && items1 == false)
		{
			CurNumItems ++;
			items1 = true;
		}
		else if(Finished1 == true && Finished2 == false && items2 == false)
		{
			CurNumItems ++;
			items2 = true;
		}
	}


}
