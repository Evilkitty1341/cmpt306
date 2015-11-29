using UnityEngine;
using System.Collections;

public class ColliderCheck : MonoBehaviour {

	public string direction;

	//Check for collisions with the wall, if you do collide, try and walk away from it.
	void OnCollisionStay2D(Collision2D collision)
	{
		GetComponentInParent<PathManager> ().SendMessage ("DirectionSet", "clear");
		if (collision.gameObject.tag == "Player") {
			//Stuff
		} else if (collision.gameObject.tag == "Wall") {
			GetComponentInParent<PathManager> ().DirectionSet(direction);
			GetComponentInParent<PathManager> ().obstacle = true;
		}
	}
}
