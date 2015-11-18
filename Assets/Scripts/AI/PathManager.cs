using UnityEngine;
using System.Collections;

public class PathManager : MonoBehaviour {

	public bool obstacle = false;		//Flag if an obstacle has been hit.
	bool pause = false; 				//Flag if entity is pathing a random distance. Pauses other pathing logic temporarily.
	Vector3 player;						//The player position vector.
	public float mobSp;					//Keep a record of the spawn location, so we can return to it if necessary.
	public Rigidbody2D rbody;
	public Vector3 homePos;

	public bool forward;
	public bool backward;
	public bool left;
	public bool right;

	void Start () {
		homePos = this.transform.position;
		rbody = this.GetComponent<Rigidbody2D>();
		Debug.Log (homePos.ToString ());

		ConfigColliders ();
	}
	
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/* 
		Point towards and move towards target. If theres a collision with a map object, walk a random direction and 
		try again.
		Tags in the string of the tag of the object to path towards
		TODO Target picking logic if theres more then one entity with that tag.
	*/
	///////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void LocalPathToTarget(string tag){
		player = GameObject.FindWithTag (tag).transform.localPosition;
		if (!obstacle) {
			MoveToOnAxis(player);
			//MoveTo (player);
			//float rotZ = Mathf.Atan2 (player.y - transform.position.y, player.x - transform.position.x) * Mathf.Rad2Deg;
			//transform.rotation = Quaternion.AngleAxis (rotZ, Vector3.forward);
		} else if (obstacle || left || right || forward || backward) {
			Debug.Log ("Obstacle pathing time!");
			localObstacle();
		}
	}


	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/* 
		TODO The function to handle global pathing. Room to room, area to area pathing.
		This will be implemented with a static map (static after random generation).
		Each component will have premade nodes and we will construct a joining algorith that adds edges or combines
		nodes on our prefab map pieces/regions depending how they end up connected.
		This function will call localPathToTarget for the closest node.
		Then the entity will path along the graph to its exit node.
		If our graph is of the correct form, we can use A* to make path decisions.
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void GlobalPathToTarget(){



	}


	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/* 
		TODO The function to head back to the spawn location, using both global and local pathing.
		This function is to prevent kiting. (Dragging enemies into obstacles, keeping them at range, etc.)
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void GoToSpawn(){

		MoveTo (homePos);

	}

	public void localObstacle(){
		if(forward && (!right || !left)){
			Debug.Log("Time to do local Obstacle forward stuff.");
			MoveToOnAxis (new Vector3(transform.position.x + 1f, transform.position.y, 0));
		}
		else if(backward){
			//Nothing
		}
		else if(left){
			MoveToOnAxis (new Vector3(transform.position.x, transform.position.y + Time.deltaTime, 0));
		}
		else if(right){
			MoveToOnAxis (new Vector3(transform.position.x, transform.position.y - Time.deltaTime, 0));
		}
		else if(left && forward){
			MoveToOnAxis (new Vector3(transform.position.x, transform.position.y + Time.deltaTime, 0));
		}
		else if(right && forward){
			MoveToOnAxis (new Vector3(transform.position.x, transform.position.y - Time.deltaTime, 0));
		}
		else{
		}
		StartCoroutine(waitForMove ());
	}


	public void killForce(){
		rbody.angularVelocity = 0.0f;
		rbody.velocity = Vector3.zero;
	}


	public void MoveToOnAxis(Vector3 pos){
		float diffX;
		float diffY;

		killForce ();

		diffX = pos.x - transform.position.x;
		diffY = pos.y - transform.position.y;
		if (Mathf.Abs (diffX) >= Mathf.Abs (diffY)) {
			if (diffX <= 0f) {
				rbody.AddRelativeForce (Vector2.left * mobSp);
			} else {
				rbody.AddRelativeForce (Vector2.right * mobSp);
			}
		}	
		else{
			if (diffY <= 0f) {
				rbody.AddRelativeForce (Vector2.down * mobSp);
			} else {
				rbody.AddRelativeForce (Vector2.up * mobSp);
			}
		}
		waitForMove ();
	}

	public void MoveToOnAxis(GameObject name){
		Vector3 pos = name.transform.position;
		float diffX;
		float diffY;
		
		killForce ();
		
		diffX = pos.x - transform.position.x;
		diffY = pos.y - transform.position.y;
		if (Mathf.Abs (diffX) >= Mathf.Abs (diffY)) {
			if (diffX < 0f) {
				rbody.AddRelativeForce (Vector2.left * mobSp);
			} else {
				rbody.AddRelativeForce (Vector2.right * mobSp);
			}
		}	
		else{
			if (diffY < 0f) {
				rbody.AddRelativeForce (Vector2.down * mobSp);
			} else {
				rbody.AddRelativeForce (Vector2.up * mobSp);
			}
		}
		waitForMove ();
	}

	//Moves the caller to the position vector.
	public void MoveTo(Vector3 pos){
			waitForMove ();
			float rotZ = Mathf.Atan2 (pos.y - transform.position.y, pos.x - transform.position.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (rotZ, Vector3.forward);
			killForce ();
			rbody.AddRelativeForce (Vector2.right * mobSp);
			//transform.Translate (Vector3.right * mobSp * Time.deltaTime);
			StartCoroutine(waitForMove ());
	}

	public void MoveTo(GameObject target){
			killForce ();
			waitForMove ();	
			Vector3 pos = target.transform.position;
			float rotZ = Mathf.Atan2 (pos.y - transform.position.y, pos.x - transform.position.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (rotZ, Vector3.forward);
			killForce ();
			rbody.AddRelativeForce (Vector2.right * mobSp);
			//transform.Translate (Vector3.right * mobSp * Time.deltaTime);
			StartCoroutine(waitForMove());
	}

	//Moves the caller random direction for a second or two.
	public void MoveRandom(){
		//print ("Going random angle");
		float randomAng = Random.Range (-90.0f, 90.0f);
		//print ("I picked: " + randomAng + "!");
		transform.rotation = Quaternion.AngleAxis (Random.Range (-90.0f, 90.0f), Vector3.forward);
		transform.Translate (Vector3.right * (mobSp / 4) * Time.deltaTime);
	}
	
	//Given a game object, find the direction vector to the object, flip the sign and move that direction.
	public void RunFrom(GameObject target){
		Vector3 t = target.transform.position;
		Vector3 o = this.transform.position;	
		Vector3 r = new Vector3 (t.x - o.x, t.y - o.y, 0f);
		float rotZ = Mathf.Atan2 (target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (-rotZ, Vector3.forward);
		transform.Translate (Vector3.right * mobSp * 0.8f * Time.deltaTime);
		//StartCoroutine (waitForAction (4));
	}


	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/* 
		The collision handler for pathing.
		If a collision occurs and we aren't global pathing (TODO) we walk a random direction and try and transit to
		node/target again.
		If we are global pathing, our global pathing graph has an issue.
		Throw an error.
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/*
	void OnCollisionEnter2D(Collision2D collision)
	{
		//TODO Tags need to be adjusted for all collidables.
		if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy" ) {
		//StartCoroutine(waitForMove ());

		}
	}
	*/	
	void DirectionSet(string direction){

		switch (direction) {
		case "forward":
			forward = true;
			break;
		case "backward":
			backward = true;
			break;
		case "left":
			left = true;
			break;
		case "right":
			right = true;
			break;
		case "clear":
			forward = false;
			backward = false;
			left = false;
			right = false;
			break;
		default:
			break;
		}

	}

	void ConfigColliders(){

		ColliderCheck[] colliders = GetComponentsInChildren<ColliderCheck> ();

		foreach (ColliderCheck n in colliders) {
			switch (n.name) {
			case "ColliderLeft":
				n.direction = "left";
				break;
			case "ColliderRight":
				n.direction = "right";
				break;
			case "ColliderForward":
				n.direction = "forward";
				break;
			case "ColliderBackward":
				n.direction = "backward";
				break;
			default:
				break;
			}
		}
	
	}
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/* 
		Wait to allow object to move away from obstacle (hopefully). Then try and resume following player.
		This is a helper function for localPathToTarget()
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	IEnumerator waitForMove()
	{
		//print ("waiting to change direction");
		pause = true;
		yield return new WaitForSeconds(0.5f);
		DirectionSet ("clear");
		pause = false;
		obstacle = false;
		//print ("finished changing direction");
	}

}
