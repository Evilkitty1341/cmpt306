using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {

	public StatCollectionClass stat;
	
	public SkillTree skill;
	
	private float fireDelay = 2.0F;

	float cooldownTimer = 0;
	//AudioSource audio;
	
	//prefab to spawn
	public GameObject FireBallPrefab;
	
	//the Ball that has been spawned
	public GameObject spawnedFireBall;

	StoryLineComponents SLC;
	
	
	
	// Use this for initialization
	void Start () {
		//audio = GetComponent<AudioSource>();
		SLC = GameObject.Find("Main Camera").GetComponent<StoryLineComponents>();
	}
	
	// Update is called once per frame
	void Update () {
		
		cooldownTimer -= Time.deltaTime;
		
		
		if(Input.GetKey(KeyCode.Alpha1)&& stat.EnergyBallUnlocked == true && cooldownTimer <=0 && stat.mana>= skill.EnergyBallMpCost && SLC.playerEnabled == true){
			
			stat.mana -= skill.EnergyBallMpCost;
			
			//audio.Play ();
			
			spawnedFireBall = GameObject.Instantiate(FireBallPrefab, transform.position, transform.rotation) as GameObject;

			if (stat.playerDirection == 1)
			{
				spawnedFireBall.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 500));
			}
			else if (stat.playerDirection == 2)
			{
				spawnedFireBall.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(500, 0));
			}
			else if (stat.playerDirection == 3)
			{
				spawnedFireBall.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, -500));
			}
			else if (stat.playerDirection == 4)
			{
				spawnedFireBall.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-500, -0));
			}

			
			cooldownTimer = fireDelay;
			
		}
	}

}
