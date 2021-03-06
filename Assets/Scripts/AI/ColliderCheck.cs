﻿using UnityEngine;
using System.Collections;

public class ColliderCheck : MonoBehaviour {

	public string direction;
	PathManager parentPM;

	//Check for collisions with the wall, if you do collide, try and walk away from it.
	void OnCollisionStay2D(Collision2D collision)
	{
		parentPM = GetComponentInParent<PathManager> ();
		GetComponentInParent<PathManager> ().SendMessage ("DirectionSet", "clear");
		if (collision.gameObject.tag == "Player") {
			//Stuff
		} else if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Lava") {
			parentPM.DirectionSet(direction);
			parentPM.obstacle = true;
			if(parentPM.delP == Vector3.zero || parentPM.delP == null){
				//parentPM.delP = parentPM.transform.localPosition;
			}
		}
	}
}
