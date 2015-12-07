using UnityEngine;
using System.Collections;

public class ProjectileCheck : MonoBehaviour {
	
	public float damage;

	Rigidbody2D spawned_Bullet;
	
	
	// Use this for initialization
	void Start () {
		
		spawned_Bullet = GetComponent<Rigidbody2D> ();
		
		
	}
	
	// Update is called once per frame
	void Update () {

			//Destroy (spawned_Bullet, 20);
			//Destroy (this.gameObject, 20);
		
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player")
		{
			if(collision.gameObject.tag == "Player"){
				collision.gameObject.GetComponent<StatCollectionClass>().doDamage(damage);
			}
			Destroy (spawned_Bullet);
			Destroy(this.gameObject);
		}
		Destroy (spawned_Bullet, 5);
		Destroy(this.gameObject, 5);
	}

}
