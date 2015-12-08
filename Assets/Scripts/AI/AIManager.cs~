using UnityEngine;
using System.Collections;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/* 	
 	At startup this struct is populated with AI parameters. 
  	These are simple booleans and values to describe the AI classificaiton.
	ie. Melee = true, Aggressive = true, Stalker = true
	These values will describe the behavior and help construct the decision tree Psuedo-dyanamically.
  	If nothing else(dynamic explodes on us), it will describe the piece-wise template they adhere to.
	This is a struct as a opposed to a class because these never change once created.
	Instead they are USED dynamically in the decision logic as opposed to calculated dynamically.
	This means once created and shared its done, all scripts are on the same page.
	Then if something like idealRange needs to change we use the static value like idealRange*2 etc.
*/
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
public struct AIConfig {
	
	public string AIType;

	public bool isMelee;		//Will favour melee attacks over ranged. Should default to ranged if melee is not possible.
								//If false the enemy will favour ranged attacks, and use melee or run if the player does.
	public bool isHybrid;		//Can perform both melee and hybrid. WIP TODO

	public bool isAggressive;	//The enemy will attack first and from a greater distance.

	public bool isStalker;		//No jokes. The enemy follows the player further then other AI.

	public bool hasMagic;		//Has magic abilities, will reference a list in attack and use them on conditions.

	public bool isCautious;	//If the AI has low health, the AI will run away if possible. Use defensive abilities/heal.

	public bool isMade;		//This AI Configuration was successfully setup.	


	//Used to determine the radius of aggresion in entitys.
	//Chosen from play testing and estimating the size of the map.
	//This variable is connected to how far the mob should be from the spawn point.
	//But ignores this while the player is around, so a threat zone too big means the entitys will never
	//stop attacking. Too small and they are never a threat.
	public float threatZone;
	
	//The range that entitys want to be at. Which in this case is right next to the player.
	//This would change if there was a ranged component to the enemies.
	public float idealRange;
	
	//The Min distance the entity considers a friend a benefit in the decision tree.
	//This is based on map size. Generally big enough to include fellow spawnees but not so large as to
	//overlap another spawn point (generally).
	public float friendlyMinDis;
	
	//How far the entity will like to be from its spawn location. 
	//If and only if the player is not within the threatZone.
	public float kiteDistance;
	
	//When do I choose to run? At 1 hp. With more max hp this would likely change.
	public float minHealth;

	//How to scale normalized stats. Used for balancing.
	public float healthMultiplier;

	public float manaMultiplier;

	public float attackMultiplier;
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	//A k-lookgood variable, offsets behavioral elemnts by a multiple of this percentage.
	public float delta;

	public float movementSpeed;

	public StatCollectionClass statExchange;


}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/* 
	This class pulls together all the scripts and methods that will comprise our AI.The basics are laid out here
	since this is a fairly hefty code set.
	
	This script is attached to a prefab and startup will assign stats and behavior, randomly(DONE), or 
	specified(DONE).

	Pathing is handled in PathManager.cs (DONE TODO Keep refining.)
	Attacking is handled in AIBehavior.cs (DONE)
	Decision making will be handled in DecisionTree.cs (DONE)
	Decision behavior is defined in AIBehavior.cs(DONE), that the DecisionTree will use at each step(DONE).
	DTree.cs contains our tree structure, and decision logic class.(DONE)
	Each node will contain an instance of BranchLogic, which will execute a delegate, and store the result.(DONE)
	That result is then used to move the tree iterator to the appropriate child.(DONE)

	weightedLevel is representative as the SUM of all the normalized combat stats
	healthMul, manaMul, ect. are the mulipliers to denormalize those stats for gameplay.
	These stats are moved to the AICONFIG, these are set at creation only but can be set individually for each 
	Behavior Type defined in a simple method within BehaviorTypes.cs (INPROGRESS).

	Once stats and behavior are choosen, the AI starts, drawing on the other scripts to carry out actions and
	choices. 
	This will be encapsulated in a decision tree using function pointers.(DONE)
	A decision tree template will be constructed and the pointers will be adjusted depending on the AI type.
	(DONE)
	Function pointers (delegates) are added to an instance of BranchLogic, where they are carried out when 
	prompted and the decision path stored. This can be drawn on to adjust the tree pointer in DecisionTree.
	If a result fails all conditionals or we hit a leaf node, a return value of zero prompts a return to root.
	**(May need to confine this behavior if we run across exploits)
	
	Stat based behavior will be decided from the random stats, other stats will be choosen at random with 
	guidelines. 
	That is stats must fall between minStatValue and maxStatValue.
	weightedLevel to generate by can never be greater than maxStatValue*numOfStats (an impossible assignment).
	This will throw a debug break and error message. (DONE)

	IMPORTANT:
	Functionality has been split between AIManager.cs and Procedural AI so that AI stored state can be seperated
	from AI generation.	
*/
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
public class AIManager : MonoBehaviour {

	public string decisionType;

	DecisionTree behavior;

	public AIConfig config;


	void Start () {

		StatCollectionClass tempStat;
		PathManager tempPath;
		AIBehavior tempBe;
		DecisionTree tempTre;
		ColliderCheck tempCol;



		tempStat = gameObject.AddComponent<StatCollectionClass>();
		tempStat.enabled = false;
		tempStat.copyStats (config.statExchange);

		tempPath = gameObject.AddComponent<PathManager> ();
		tempPath.enabled = false;
		tempPath.mobSp = config.movementSpeed;

		tempBe = gameObject.AddComponent<AIBehavior> ();
		tempBe.enabled = false;
		tempBe.anim = gameObject.GetComponent<Animator>();
		tempBe.rb = config;
		tempBe.pathing = tempPath;
		tempBe.reference = tempStat;
		tempBe.ps = gameObject.GetComponentInChildren<ProjectileSpawner>();

		tempTre = gameObject.AddComponent<DecisionTree>();
		tempTre.enabled = false;
		tempTre.behaviorType = config.AIType;

		tempCol = gameObject.AddComponent<ColliderCheck> ();
		tempCol.enabled = false;

		behavior = gameObject.GetComponent<DecisionTree> ();
		behavior.enabled = false;
		behavior.behaviorType = decisionType;

		//AI START//
		tempStat.enabled = true;
		tempPath.enabled = true;
		tempBe.enabled = true;
		tempTre.enabled = true;
		tempCol.enabled = true;
		behavior.enabled = true;

		behavior.startDeciding ();
	}


	void Update () {
		//print ("Health: " + config.statExchange.health.ToString());
		if (gameObject.GetComponentInParent<StatCollectionClass>().health <= 0.09f) {
			print ("Health: " + config.statExchange.health.ToString());
			behavior.stopDeciding();
			DestroyObject(gameObject);
		}
		else{
			//print ("Health: " + config.statExchange.health.ToString());
			//behavior.stopDeciding();
			//DestroyObject (gameObject);
		}

	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/* 
	
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}