using UnityEngine;
using System.Collections;

public class ColliderCheck : MonoBehaviour {

	public string direction;

	//Check for collisions with the wall, if you do collide, try and walk away from it.
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Wall") {
			//GetComponentInParent<PathManager>().SendMessage("DirectionSet", true, direction);
		}
	}
}
