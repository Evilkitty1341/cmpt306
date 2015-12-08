using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {

	public string spawnGroup;

	public float timer = 1f;
	public float dTimer = 5f;
	public float dTimerReduce = 0.1f;

	private GameObject[] spawnLocations;
	public float spawnRadius;
	private int[] spawnFilled;
	public GameObject spawnObjectMelee;
	public GameObject spawnObjectRanged;
	public GameObject spawnObjectHybrid;
	public GameObject spawnObjectBoss;

	public int maxNumGroups;	// Maximum number of group of enemies to spawn in the area
	public int groupWeight;	// Overall weighting for a group of enemies
	public int minInGroup;		// Minimum number of enemies that can spawn in a group
	public int maxInGroup;		// Maximum number of enemies that can spawn in a group
	public int minStatPerNPC;
	public int maxStatPerNpc;
	private float countdown;
	private int numEnemies;			// This will be determined per group spawned
	private int statPerEnemy;		// This will be determined after the number of enemies for a group is determined

	ProceduralAI aiMaker;
	
	// Use this for initialization
	void Start () {
		if((groupWeight/minInGroup) > maxStatPerNpc * 3){
			Debug.LogError("Improper spawn configuration");
		}
		else{
			SpawnWave();

			GameObject spawnBoss = GameObject.FindGameObjectWithTag ("SpawnBoss");
			if(spawnBoss != null){
				SpawnSingle(spawnBoss.transform.position);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		countdown -= Time.deltaTime;

		if (countdown <= 0f) {
			// Determine number of enemies to spawn and assign each a number of stat points
			/*numEnemies = Random.Range (minInGroup, maxInGroup);
			print (numEnemies);
			statPerEnemy = groupWeight / numEnemies;

			for(int k = 0; k <= maxNumGroups; k++)
			{
				// Pick random spawn point
				int j = Random.Range(0, spawnLocations.Length);

				for (int i = 0; i <= numEnemies; i++)
				{
					// Spawn enemy
					GameObject enemy = Instantiate(spawnObject) as GameObject;

					// Set position to selected spawn point
					enemy.transform.position = new Vector3(spawnLocations[j].transform.position.x, spawnLocations[j].transform.position.y, 0f);
				}
			}*/

			// Will logarithmically decrease the time between enemy spawns to a minimum of timer seconds apart
			countdown = timer - Mathf.Min (0f, 0.5f * dTimer * Mathf.Log10(Mathf.Max (1f, Time.timeSinceLevelLoad) * dTimerReduce) - dTimer);
		}
	}
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/* 
		Spawns a single preconfigured AI. Used for boss spawning atm.
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void SpawnSingle(Vector3 pos){

		AIConfig ai = new AIConfig();
		ManualAI mAI = new ManualAI();

		ai = mAI.LevelOneBoss(ai);
		GameObject enemy;

		enemy = Instantiate(spawnObjectBoss) as GameObject;

		enemy.GetComponent<AIManager>().config = ai;
		enemy.GetComponent<AIManager>().decisionType = "boss";
		enemy.GetComponent<AIManager>().enabled = true;

		// Set position to selected spawn point
		enemy.transform.position = pos;

	}
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/* 
			Finds all target with tag Spawn and spawns a group of procedural enemies based on the configurations.
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void SpawnWave(){
		spawnRadius = 1.0f;
		spawnLocations = GameObject.FindGameObjectsWithTag ("Spawn");
		print (spawnLocations.Length);
		spawnFilled = new int[spawnLocations.Length];
		
		if (spawnLocations.Equals (null)) {
			Debug.LogError("Spawn locations not properly added or configured!!!");
		}
		
		for(int i = 0; i < spawnLocations.Length; i++) {
			spawnFilled [i] = 0;
		}
		
		aiMaker = gameObject.AddComponent<ProceduralAI> ();
		int failedAttempts = 0;

		for(int k = 0; k < maxNumGroups; k++)
		{
			// Pick random spawn point
			int j = Random.Range(0, spawnLocations.Length);
			//print (j);
			if(spawnFilled[j] == 1 && failedAttempts < 10){
				failedAttempts++;
				k--;
			}
			else if(failedAttempts >= 10){
				Debug.LogError("Spawn assignments failed before spawn locations were exhausted: Check variables and spawn points for consistency.");
				return;
			}
			
			else{
				// Determine number of enemies to spawn and assign each a number of stat points
				numEnemies = Random.Range (minInGroup, maxInGroup);
				statPerEnemy = groupWeight / numEnemies;
				spawnFilled[j] += 1;

				for (int i = 1; i < numEnemies; i++)
				{
					float spawnDelta = Random.Range(0f, 2f * Mathf.PI);
					AIConfig a = aiMaker.AIBuilder("mob", statPerEnemy, minStatPerNPC, maxStatPerNpc);
					// Spawn enemy
					
					GameObject enemy;
					
					if(a.isMelee)
						enemy = Instantiate(spawnObjectMelee) as GameObject;
					else if(a.isHybrid)
						enemy = Instantiate(spawnObjectHybrid) as GameObject;
					else
						enemy = Instantiate(spawnObjectRanged) as GameObject;
					
					enemy.GetComponent<AIManager>().config = a;
					enemy.GetComponent<AIManager>().enabled = true;
					// Set position to selected spawn point
					Vector3 locWDelta = spawnLocations[j].transform.position;
					locWDelta.x = locWDelta.x + Random.Range(0.25f, spawnRadius) * Mathf.Sin (spawnDelta);
					locWDelta.y = locWDelta.y + Random.Range(0.25f, spawnRadius) * Mathf.Cos (spawnDelta);
					locWDelta.z = 0.0f;
					enemy.transform.position = locWDelta;
				}

			}
		}

		// Initial timer
		countdown = timer + dTimer;
	}
}
