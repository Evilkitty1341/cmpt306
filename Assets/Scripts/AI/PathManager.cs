using sys = System;
using UnityEngine;
using System.Collections;

public class PathManager : MonoBehaviour {
	
	public bool obstacle = false;		//Flag if an obstacle has been hit.
	bool pause = false; 				//Flag if entity is pathing a random distance. Pauses other pathing logic temporarily.
	public Vector3 player;				//The player position vector.
	public float mobSp;					//Keep a record of the spawn location, so we can return to it if necessary.
	public Rigidbody2D rbody;
	public Vector3 homePos;
	Animator anim;
	
	float randomSign;

	public bool los;
	public bool forward;
	public bool backward;
	public bool left;
	public bool right;
	public Vector2 lastDir;
	public Vector2 lastPos;
	int failedMoveCount = 0;
	public Vector3 delP;
	
	
	void Start () {
		los = false;
		homePos = this.transform.position;
		delP = Vector3.zero;
		rbody = this.GetComponent<Rigidbody2D>();
		//Debug.Log (homePos.ToString ());
		int check = Random.Range (1, 3);

		if (check == 1) {
			randomSign = 1.0f;
		}
		else
		{
			randomSign = 1.0f;
		}
		ConfigColliders ();

		anim = gameObject.GetComponent<Animator>();
		anim.SetInteger("Direction", 2);

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
		if (obstacle) {
			//Debug.Log ("Obstacle pathing time!");
			localObstacle ();
		} else if (delP.magnitude >= 0.05) {
			MoveToOnAxis (player);
		}
		else {
			MoveToOnAxis(player);
		}
	}
	
	public void LocalPathToTarget(Vector3 tar){
		if (obstacle) {
			//Debug.Log ("Obstacle pathing time!");
			localObstacle ();
		} else if (delP.magnitude >= 0.05) {
			MoveToOnAxis (tar);;
		}
		else {
			MoveToOnAxis(tar);
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
		/*
		Given the search space and the way procedural is handed A* did not become viable for every AI.
		AI implementation removed as it was not cost effective.
		Time pending, look at a fast A* alternative or redefine the search space to an array with lower cost access.
		*/
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
		//Collision on axis
		if(forward){
			//Debug.Log("Time to do local Obstacle forward stuff.");
			delP.x += mobSp * Time.deltaTime * randomSign;
			MoveToOnAxis (new Vector3(transform.position.x + Time.deltaTime * randomSign, transform.position.y, 0));
		}
		else if (backward){
			//Debug.Log("Time to do local Obstacle backward stuff.");
			delP.x -= mobSp * Time.deltaTime * randomSign;
			MoveToOnAxis (new Vector3(transform.position.x - Time.deltaTime * randomSign, transform.position.y, 0));
		}
		else if (left){
			delP.y += mobSp * Time.deltaTime * randomSign;
			MoveToOnAxis (new Vector3(transform.position.x, transform.position.y + Time.deltaTime * randomSign, 0));
		}
		else if (right){
			delP.y -= mobSp * Time.deltaTime * randomSign;
			MoveToOnAxis (new Vector3(transform.position.x, transform.position.y - Time.deltaTime * randomSign, 0));
		}
		else{
			delP = Vector3.zero;
		}
		StartCoroutine(waitForMove ());
	}
	
	
	public void killForce(){
		rbody.angularVelocity = 0.0f;
		rbody.velocity = Vector3.zero;
		anim.SetBool("Moving", false);
	}
	
	
	public void MoveToOnAxis(Vector3 pos){
		killForce ();
		anim.SetBool("Moving", true);
		float diffX;
		float diffY;
		
		diffX = pos.x - transform.position.x;
		diffY = pos.y - transform.position.y;

		//Debug.Log(diffPos().ToString());
		if (diffPos() < 0.05f){
			if(failedMoveCount < 3){
				failedMoveCount++;
			}
			else{
				randomSign = randomSign * -1.0f;
			}
			if(lastDir == Vector2.left){
				rbody.AddRelativeForce(Vector2.right * Time.deltaTime * randomSign);
				anim.SetInteger("Direction", 3);
			}
			else if(lastDir == Vector2.right){
				rbody.AddRelativeForce(Vector2.left * Time.deltaTime * randomSign);
				anim.SetInteger("Direction", 1);
			}
			else if(lastDir == Vector2.up){
				rbody.AddRelativeForce(Vector2.down * Time.deltaTime * randomSign);
				anim.SetInteger("Direction", 0);
			}
			else if(lastDir == Vector2.down){
				rbody.AddRelativeForce(Vector2.up * Time.deltaTime * randomSign);
				anim.SetInteger("Direction", 2);

			}
		}
		if(delP.magnitude == 0){
			failedMoveCount = 0;
			if (Mathf.Abs (diffX) >= Mathf.Abs (diffY)) {
				if (diffX < 0f) {
					rbody.AddRelativeForce (Vector2.left * mobSp);
					anim.SetInteger("Direction", 3);
				} else {
					rbody.AddRelativeForce (Vector2.right * mobSp);
					anim.SetInteger("Direction", 1);
				}
			} else {
				if (diffY < 0f) {
					rbody.AddRelativeForce (Vector2.down * mobSp);
					anim.SetInteger("Direction", 2);
				} else {
					rbody.AddRelativeForce (Vector2.up * mobSp);
					anim.SetInteger("Direction", 0);
				}
			}
		}
		else{
			failedMoveCount = 0;
			if(obstacle){
				if (Mathf.Abs (diffX) >= Mathf.Abs (diffY)) {
					if (diffX < 0f) {
						rbody.AddRelativeForce (Vector2.left * mobSp * randomSign);
						//rbody.AddRelativeForce (Vector2.up * 0.001f * randomSign);
						anim.SetInteger("Direction", 3);

						lastDir = Vector2.left;
					} else {
						rbody.AddRelativeForce (Vector2.right * mobSp * randomSign);
						//rbody.AddRelativeForce (Vector2.down * 0.001f * randomSign);
						anim.SetInteger("Direction", 1);

						lastDir = Vector2.right;
					}
				} else {
					if (diffY < 0f) {
						rbody.AddRelativeForce (Vector2.down * mobSp * randomSign);
						//rbody.AddRelativeForce (Vector2.left * 0.001f * randomSign);
						anim.SetInteger("Direction", 2);

						lastDir = Vector2.down;
					} else {
						rbody.AddRelativeForce (Vector2.up * mobSp * randomSign);
						//rbody.AddRelativeForce (Vector2.right * 0.001f * randomSign);
						anim.SetInteger("Direction", 0);

						lastDir = Vector2.up;
					}
				}
			}
			else if(!obstacle){
				failedMoveCount = 0;
				float checkX = 0.0f;
				float checkY = 0.0f;
				int decay = 0;
				if(lastDir == Vector2.zero){
					if ((Mathf.Abs (delP.y) + Mathf.Abs (diffX)) >= (Mathf.Abs (delP.x) + Mathf.Abs (diffY))) {
						decay = 2;
						if (diffX < 0f) {
							rbody.AddRelativeForce (Vector2.left * mobSp * randomSign);
							anim.SetInteger("Direction", 3);
						} else {
							rbody.AddRelativeForce (Vector2.right * mobSp * randomSign);
							anim.SetInteger("Direction", 1);
						}
					} else {
						decay = 1;
						if (diffY < 0f) {
							rbody.AddRelativeForce (Vector2.down * mobSp * randomSign);
							anim.SetInteger("Direction", 2);

						} else {
							rbody.AddRelativeForce (Vector2.up * mobSp * randomSign);
							anim.SetInteger("Direction", 0);
						}
					}
					if(decay == 1){
						Debug.Log ("Decay X...");
						if(delP.x < 0f){
							checkX = Mathf.Exp(Mathf.Abs (Time.deltaTime * mobSp));
							if(checkX >= Mathf.Abs (delP.x))
								delP.x = 0.0f;
							else 
								delP.x = delP.x + checkX;
						}
						else{
							checkX = Mathf.Exp(Mathf.Abs (Time.deltaTime * mobSp));
							if(checkX >= Mathf.Abs (delP.x))
								delP.x = 0.0f;
							else 
								delP.x = delP.x - checkX;
						}
					}
					else if(decay == 2){
						if(delP.y < 0f){
							checkY = Mathf.Exp(Mathf.Abs (Time.deltaTime * mobSp));
							if(checkY >= Mathf.Abs (delP.y))
								delP.y = 0.0f;
							else 
								delP.y = delP.y + checkY;
						}
						else{
							checkY= Mathf.Exp(Mathf.Abs (Time.deltaTime * mobSp));
							if(checkY >= Mathf.Abs (delP.y))
								delP.y = 0.0f;
							else 
								delP.y = delP.y - checkY;
						}
					}else{
						delP = Vector3.zero;
					}
				}
				else if(lastDir == Vector2.left){
					rbody.AddRelativeForce(Vector2.down * mobSp * randomSign * (delP.y/Mathf.Abs (delP.y)));
					anim.SetInteger("Direction", 2);
					lastDir = Vector2.zero;
				}
				else if(lastDir == Vector2.right){
					rbody.AddRelativeForce(Vector2.up * mobSp * randomSign * (delP.x/Mathf.Abs(delP.x)));
					anim.SetInteger("Direction", 0);
					lastDir = Vector2.zero;
				}
				else if(lastDir == Vector2.up){
					rbody.AddRelativeForce(Vector2.left * mobSp * randomSign * (delP.y/Mathf.Abs (delP.y)));
					anim.SetInteger("Direction", 3);
					lastDir = Vector2.zero;
				}
				else if(lastDir == Vector2.down){
					rbody.AddRelativeForce(Vector2.right * mobSp * randomSign * (delP.x/Mathf.Abs(delP.x)));
					anim.SetInteger("Direction", 1);
					lastDir = Vector2.zero;
				}
			}
		}
		StartCoroutine(waitForMove ());
	}

	//Moves the caller to the position vector.
	public void MoveTo(Vector3 pos){
		killForce ();
		float rotZ = Mathf.Atan2 (pos.y - transform.position.y, pos.x - transform.position.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (rotZ, Vector3.forward);
		rbody.AddRelativeForce (Vector2.right * mobSp);
		StartCoroutine(waitForMove ());
	}
	
	public void MoveTo(GameObject target){
		killForce ();
		Vector3 pos = target.transform.position;
		float rotZ = Mathf.Atan2 (pos.y - transform.position.y, pos.x - transform.position.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (rotZ, Vector3.forward);
		rbody.AddRelativeForce (Vector2.right * mobSp);
		StartCoroutine(waitForMove());
	}
	
	//Moves the caller random direction for a second or two.
	public void MoveRandom(){
		float randomAng = Random.Range (-90.0f, 90.0f);
		transform.rotation = Quaternion.AngleAxis (Random.Range (-90.0f, 90.0f), Vector3.forward);
		transform.Translate (Vector3.right * (mobSp / 4) * Time.deltaTime);
	}
	
	//Given a game object, find the direction vector to the object, flip the sign and move that direction.
	public void RunFrom(GameObject target){
		Vector3 t = target.transform.position;
		Vector3 o = this.transform.position;	
		Vector3 r = new Vector3 (t.x - o.x, t.y - o.y, 0f);
		float rotZ = Mathf.Atan2 (target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;

		Vector3 runTo = Vector3.RotateTowards (o, t, rotZ, rotZ);
		runTo = Vector3.Scale (runTo, new Vector3 (5, 5, 5));
		MoveToOnAxis (runTo);
	}

	public void LineOfSight(string name){
		GameObject tar = GameObject.FindGameObjectWithTag (name);
		LayerMask mask = 1 << tar.layer;
		//Debug.Log ("Layer found: " + LayerMask.LayerToName (mask.value));

		RaycastHit2D hit = Physics2D.Linecast(transform.position, tar.transform.position, mask);
		if (hit.collider == null){
			return;
		}

		//Debug.Log ("Hit: " + hit.collider.gameObject.tag.ToString() + " Meant to hit: " + name);

		if(hit.collider.gameObject.tag == name){
			Debug.Log ("I can see the player!");
			los = true;
		}
		else{
			Debug.Log ("Can't see player...");
			los = false;
		}
	}

	public void DirectionSet(string direction){
		
		switch (direction) {
		case "forward":
			forward = true;
			break;
		case "left":
			left = true;
			break;
		case "right":
			right = true;
			break;
		case "backward":
			backward = true;
			break;
		case "clear":
			left = false;
			right = false;
			backward = false;
			forward = false;
			break;
		default:
			break;
		}
		
	}

	float diffPos(){
		float check = Mathf.Abs(transform.position.magnitude - lastPos.magnitude);
		if(check < 0.1){
			anim.SetBool("Moving", false); 
		}
		return check;

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
		lastPos = transform.position;
		yield return new WaitForSeconds(0.1f + Random.Range (0.2f, 0.3f));
		DirectionSet ("clear");
		pause = false;
		obstacle = false;

		//print ("finished changing direction");
	}
	
}