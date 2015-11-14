using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {
	
	//Check for collisions with the wall, if you do collide, try and walk away from it.
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Wall") {

		}
	}
	

}
