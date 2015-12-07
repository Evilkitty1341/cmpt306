using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {

	public StatCollectionClass stat;
	
	public SkillTree skill;
	
	private float fireDelay = 2.0F;

	float cooldownTimer = 0;

	
	//prefab to spawn
	public GameObject FireBallPrefab;
	
	public GameObject FireBreath;
	
	public GameObject SunStrike;
	
	
	
	//the Ball that has been spawned
	public GameObject spawnedFireBall;
	
	public GameObject spawnedFireBreath;
	
	public GameObject spawnedSunStrike;

	StoryLineComponents SLC;
	
	
	
	// Use this for initialization
	void Start () {
		//audio = GetComponent<AudioSource>();
		SLC = GameObject.Find("Main Camera").GetComponent<StoryLineComponents>();
	}
	
	// Update is called once per frame
	void Update () {
		
		cooldownTimer -= Time.deltaTime;
		
		
		if(Input.GetKey(KeyCode.Alpha1)&& stat.FireBallUnlocked == true && cooldownTimer <=0 && stat.mana>= skill.FireBallMpCost && SLC.playerEnabled == true){
			
			stat.mana -= skill.FireBallMpCost;
			
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

		if(Input.GetKey(KeyCode.Alpha2)&& stat.FireBreathUnlocked == true && cooldownTimer <=0 && stat.mana>= skill.FireBreathMpCost){
			
			stat.mana -= skill.FireBreathMpCost;
			
			//audio.Play ();
			
			spawnedFireBreath = GameObject.Instantiate(FireBreath, transform.position, transform.rotation) as GameObject;
			
			if (stat.playerDirection == 1)
			{
				spawnedFireBreath.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 500));
			}
			else if (stat.playerDirection == 2)
			{
				spawnedFireBreath.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(500, 0));
			}
			else if (stat.playerDirection == 3)
			{
				spawnedFireBreath.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, -500));
			}
			else if (stat.playerDirection == 4)
			{
				spawnedFireBreath.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-500, -0));
			}
			
			
			cooldownTimer = fireDelay;
			
		}
		
		
		if(Input.GetKey(KeyCode.Alpha3)&& stat.SunStrikeUnlocked == true && cooldownTimer <=0 && stat.mana>= skill.SunStrikeMpCost){
			
			stat.mana -= skill.SunStrikeMpCost;
			
			//audio.Play ();
			
			spawnedSunStrike = GameObject.Instantiate(SunStrike, transform.position, transform.rotation) as GameObject;
			
			if (stat.playerDirection == 1)
			{
				spawnedSunStrike.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 500));
			}
			else if (stat.playerDirection == 2)
			{
				spawnedSunStrike.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(500, 0));
			}
			else if (stat.playerDirection == 3)
			{
				spawnedSunStrike.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, -500));
			}
			else if (stat.playerDirection == 4)
			{
				spawnedSunStrike.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-500, -0));
			}
			
			
			cooldownTimer = fireDelay;
			
		}

	}

}
