using UnityEngine;
using System.Collections;

public class FireBreathExp : MonoBehaviour {

	public StatCollectionClass enemyStat;
	
	GameObject player;
	
	StatCollectionClass playerStat;

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

		skill = player.GetComponent<SkillTree> ();
		
		playerStat = player.GetComponent<StatCollectionClass >();
		
		Destroy(gameObject, 2f);
		
		
		
	}
	
	
	void OnTriggerEnter2D(Collider2D col)
	{
		
		
		if (col.gameObject.tag == "Enemy") {
			
			enemyStat = col.GetComponent<StatCollectionClass>();
			
			enemyStat.doDamage(skill.FireBreathDamage);
			
			this.onExplosion();
			
			Destroy (gameObject);
			
			
			
			
		} 
		
		if (col.gameObject.tag == "wallTop"||col.gameObject.tag == "wallBottom"
		    ||col.gameObject.tag == "wallLeft"|| col.gameObject.tag == "wallRight"
		    ||col.gameObject.tag == "Obstacle") {
			
			this.onExplosion();
			
			Destroy (gameObject);
			
			
		}
		
	}

}
