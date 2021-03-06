﻿using UnityEngine;
using System.Collections;

public class ProjectileCheck : MonoBehaviour {
	
	public float damage;

	Rigidbody2D spawned_Bullet;
	
	
	// Use this for initialization
	void Start () {
		
		spawned_Bullet = GetComponent<Rigidbody2D> ();

		if(gameObject.tag == "Flamethrower"){
			Destroy (spawned_Bullet, 3.0f);
			Destroy (this.gameObject, 3.0f);
		}
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy")
		{
			if(collision.gameObject.tag == "Enemy"){
				collision.gameObject.GetComponent<StatCollectionClass>().doDamage(damage);
			}
		}
		if(collision.gameObject.tag == "Player"){
			collision.gameObject.GetComponent<StatCollectionClass>().doDamage(damage);
		}
		Destroy (spawned_Bullet);
		Destroy (this.gameObject);
	}

}
