using UnityEngine;
using System.Collections;

public class AIBehavior : MonoBehaviour {
	
	bool hpAndFriends = false;		//Used to distinguish hp check on two different sub branches.
	bool taunting = false;			//Check to flag if taunting has already occurred, to spare my ears.

	Animator anim;					//Animator to flip animation variables in the in unity conroller gui.
	AudioSource audio; 				//Grab the audio, to taunt with.

	StatCollectionClass reference;
	public PathManager pathing;
	public AIConfig rb;

	void start(){
	
		//pathing = this.GetComponentInParent<PathManager>();
		if (pathing.Equals (null))
			Debug.LogError ("PATHING NOT FOUND");

	}

	//Root, Check if player is a threat.
	public int InThreatZone(){
		
		//Calculate Distance
		float check = checkDistance (GameObject.FindWithTag ("Player"));
		
		if(check <= rb.threatZone){
			Debug.Log("I am Threatened!");
			return 1;
		}
		else{
			Debug.Log("I am NOT threatened! Where am I?");
			return 2;
		}
	}

	//If the player is a threat, are they in melee range?
	public int InRange(){
		
		float check = checkDistance (GameObject.FindWithTag ("Player"));
		if (check == -1) {
			//return 0;
		}
		if(check < rb.idealRange){
			Debug.Log("Target is within range.");
			return 1;
		}
		else{
			hpAndFriends = false;
			Debug.Log("Target is out of range.");
			return 2;
		}
	}
 
	//TODO No enemy prefabs.
	public int MaxWander(){

		if(checkDistance(this.GetComponentInParent<PathManager>().homePos) > rb.kiteDistance){
			//action = ReturnToSpawn;
			Debug.Log("I am too far from home!");
			return 1;
		}
		else{
			//action = RandomWalkOrIdle;
			Debug.Log("I am at home!");
			return 2;
		}
	}
	//What is the distance to every ally?
	//For every ally that is within friendlyMinDis increment a counter.
	//If the counter is greater than zero, it has allies!
	public int AllyDis(){
		bool friends = false;
		GameObject[] allies = GameObject.FindGameObjectsWithTag ("Enemy");
		int allyInRg = 0;
		
		if (allies.Length > 0) {
			for(int i = 0; i < allies.Length; i++){
				if(checkDistance(allies[i]) < rb.friendlyMinDis){
					allyInRg++;
				}
			}
		}
		//The count and bool may seem redundant.
		//In this context it is.
		//However if the # of allies around becomes important, then we need this information.
		if (allyInRg >= 1) {
			friends = true;
		}
		
		if(friends){
			//action = Attack;
			Debug.Log("I have friends! Grrr attack!");
			return 1;
		}
		
		else{
			//action = HPCheck;
			Debug.Log("No friends!? How am I?");
			return 2;
		}
	}
	//Check the hitpoints of the current entity.
	//Run away if its low, advance or attack if its high.
	//Slightly different outcomes depending which node calls it.
	//TODO No enemy prefabs.
	public int HPCheck(){

		float check =gameObject.GetComponent<StatCollectionClass> ().health;

			if(check > rb.minHealth){
				Debug.Log("HP for days!");
				return 1;
			}
			else{
				Debug.Log("My HP is low, this is unfortunate");
				return 2;
			}
	}
	//Randomly pick between a walk, idle, or taunt action.
	public int RandomWalkOrIdle(){
		bool i = randomBinary ();
		if (i) {
			Debug.Log ("I am feeling lazy.");
			return 1;
			//idle
			
		} else if (!i) {
			Debug.Log ("Its just a flesh wound, come here I'll bite your knee caps off!");
			return 2;
			//action = Taunt;
		} else {
			Debug.Log ("I am restless...");
			return 3;
			//action = RandomWalk;
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/* 
		Leaf Nodes
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	

	//TODO Attack stuff goes here.
	public int Attack(){
		return 0;
	}
	//Stop everything and run away until player is out of threatZone.
	public int Retreat(){
		return 0;

	}
	//Move towards the player
	public int AdvanceTowards(){
		pathing.MoveTo(GameObject.FindGameObjectWithTag("Player"));
		return 0;
	}
	//Pick a random direction and walk in it.
	//Keep walking it until finished.
	//This has to be a short duration otherwise there is a lag in response while it finishes.
	//The rest of the AI tree can't respond while it does its thing.
	public int RandomWalk(){
		return 0;
	}
	//Flip the animation variable idle to true so unity idles the entity.
	public int Idle(){
		return 0;
	}
	//Taunt by playing a sound.
	public int Taunt(){
		return 0;
	}

	public int RunAway(){
		//pathing.RunFrom(GameObject.FindGameObjectWithTag("Player"));
		return 0;
	}

	//Stop everything and go back to the spawn location.
	public int ReturnToSpawn(){
		return 0;
	}


	//Random binary value, returned as a boolean.
	private bool randomBinary(){

		int i = Random.Range (0, 2);
		
		if (i == 1) {
			return true;
		} else {
			return false;
		}

	}
	
	public int RandomTrinary(){
		int randy = Random.Range (1, 4);
		return randy;
	}

	//Finds the magnitude of the seperation vector between an object the caller.
	public float checkDistance(GameObject target){
		if (target == null) {
			Debug.LogError ("target not found");
			Debug.Break();
			return -1;
		}
		else {
			//Input Checking should go here.
			Vector3 t = target.transform.position;
			Vector3 o = gameObject.transform.position;
			Vector2 r = new Vector2 (t.x - o.x, t.y - o.y);
			return r.magnitude;
		}
	}
	
	//Finds the magnitude of the seperation vector between a position vector and the caller.
	public float checkDistance(Vector3 homePos){
		//Input Checking
		Vector3 o = gameObject.transform.position;
		Vector2 r = new Vector2(homePos.x - o.x, homePos.y - o.y);	
		return r.magnitude;	
	}

	//Finds the magnitude of the seperation vector between a position vector and the caller.
	public float checkDistance(Vector3 homePos, Vector3 o){
		//Input Checking
		//Vector3 o = gameObject.transform.position;
		Vector2 r = new Vector2(homePos.x - o.x, homePos.y - o.y);	
		return r.magnitude;	
	}
}
