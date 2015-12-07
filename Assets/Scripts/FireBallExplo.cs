using UnityEngine;
using System.Collections;

public class FireBallExplo : MonoBehaviour {

	StatCollectionClass enemyStat;
	
	GameObject player;

	SkillTree skill;

	public GameObject explosion;
	
	

	void onExplosion()
	{
		Instantiate (explosion, transform.position,transform.rotation);
	}
	//AudioSource audio;
	
	void Start () 
	{
		player = GameObject.FindWithTag ("Player");
		
		skill = player.GetComponent<SkillTree >();
		
		Destroy(gameObject, 2f);


		
	}
	
	
	void OnTriggerEnter2D(Collider2D col)
	{
		
		
		if (col.gameObject.tag == "Enemy") {
			
			enemyStat = col.GetComponent<StatCollectionClass>();
			
			enemyStat.doDamage(skill.FireBallDamage);
			/*
			if(enemyStat.health <= 0)
			{
				Destroy(col.gameObject);
			}
			*/
			this.onExplosion();

			Destroy (gameObject);
			
			
			
			
		} 
		
		if (col.gameObject.tag == "Obstacle") {

			this.onExplosion();

			Destroy (gameObject);
			
			
		}
		
	}

}
