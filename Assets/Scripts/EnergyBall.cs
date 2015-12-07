using UnityEngine;
using System.Collections;

public class EnergyBall : MonoBehaviour {
	
	public StatCollectionClass stat;
	
	public SkillTree skill;
	
	private float fireDelay = 1f;
	
	float cooldownTimer = 0;
	//AudioSource audio;
	
	//prefab to spawn
	public GameObject EnergyBallPrefab;
	
	//the Ball that has been spawned
	public GameObject spawnedEnergyBall;

	//to make sure the intro scene is done
	StoryLineComponents SLC;
	
	
	
	// Use this for initialization
	void Start () {
		//audio = GetComponent<AudioSource>();
		SLC = GameObject.Find("Main Camera").GetComponent<StoryLineComponents>();
	}
	
	// Update is called once per frame
	void Update () {
		
		cooldownTimer -= Time.deltaTime;
		
		
		if(Input.GetKey(KeyCode.Alpha1)&& stat.EnergyBallUnlocked == true && cooldownTimer <=0&& stat.mana>= skill.EnergyBallMpCost && SLC.playerEnabled == true ){
			
			stat.mana -= skill.EnergyBallMpCost;
			
			//audio.Play ();
			
			spawnedEnergyBall = GameObject.Instantiate(EnergyBallPrefab, transform.position, transform.rotation) as GameObject;
			
			spawnedEnergyBall.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(500,0));
			
			cooldownTimer = fireDelay;
			
		}
	}
}
