using sys = System;
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
	public int minHealth;

	public float healthMultiplier;

	public float manaMultiplier;

	public float attackMultiplier;

	public float movementSpeed;


}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/* 
	This class pulls together all the scripts and methods that will comprise our AI.The basics are laid out here
	since this is a fairly hefty code set.
	
	This script is attached to a prefab and startup will assign stats and behavior, randomly(DONE), or 
	specified(TODO).

	Pathing is handled in PathManager.cs (INPROGRESS)
	Attacking is handled in AttackManager.cs (TODO?)
	Decision making will be handled in DecisionTree.cs (INPROGRESS)
	Decision behavior is defined in AIBehavior.cs(INPROGRESS), that the DecisionTree will use at each step(DONE).
	DTree.cs contains our tree structure, and decision logic class.(DONE)
	Each node will contain an instance of BranchLogic, which will execute a delegate, and store the result.(DONE)
	That result is then used to move the tree iterator to the appropriate child.(DONE)

	weightedLevel is representative as the SUM of all the normalized combat stats
	healthMul, manaMul, ect. are the mulipliers to denormalize those stats for gameplay.
	These stats are moved to the AICONFIG, these are set at creation only but can be set individually for each 
	Behavior Type defined in a simple method within BehaviorTypes.cs (INPROGRESS).

	Once stats and behavior are choosen, the AI starts, drawing on the other scripts to carry out actions and
	choices. 
	This will be encapsulated in a decision tree using function pointers.(INPROGRESS)
	A decision tree template will be constructed and the pointers will be adjusted depending on the AI type.
	(INPROGRESS)
	Function pointers are added to an instance of BranchLogic, where they are carried out when prompted and
	the decision path stored. This can be drawn on to adjust the tree pointer in DecisionTree.
	If a result fails all conditionals or we hit a leaf node, a return value of zero prompts a return to root.
	**(May need to confine this behavior if we run across exploits)
	
	Stat based behavior will be decided from the random stats, other stats will be choosen at random with 
	guidelines. 
	That is stats must fall between minStatValue and maxStatValue.
	weightedLevel to generate by can never be greater than maxStatValue*numOfStats (an impossible assignment).
	This will throw a debug break and error message. (DONE)
	
*/
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
public class AIManager : MonoBehaviour {

	private const int numOfStats = 3;
	private const int minStatValue = 2;
	private const int maxStatValue = 30;

	public int weightedLevel = 30;
	public int manaMul = 1;
	public string decisionType;

	SpriteRenderer mySpri;
	public string spriteSheetName;

	private DecisionTree behavior;
	

	void Start () {

		AIConfig mob = AIBuilder ("mob", weightedLevel);
		
		gameObject.AddComponent<PathManager> ().mobSp = mob.movementSpeed; 
		gameObject.AddComponent<AIBehavior> ().rb = mob;
		gameObject.GetComponent<AIBehavior> ().pathing = gameObject.GetComponent<PathManager> ();
		gameObject.AddComponent<DecisionTree>();
		mySpri = gameObject.GetComponent<SpriteRenderer> ();

		//AI START//
		behavior = gameObject.GetComponent<DecisionTree> ();

		behavior.startDeciding ();
	}

	void LateUpdate(){

		
		var subSprites = Resources.LoadAll<Sprite>("Enemies/" + spriteSheetName);
		
		foreach (var renderer in GetComponentsInChildren<SpriteRenderer>())
		{
			string spriteName = renderer.sprite.name;
			var newSprite = sys.Array.Find(subSprites, item => item.name == spriteName);
			
			if (newSprite)
				renderer.sprite = newSprite;
		}

	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/*
		The AIBuilder method creates a config for a AI enabled entity.
		Currently it psuedorandomly assigns stats, eventually options for chosen stats and weighted random stats 
		will be added.
		The AIBuilder will require switch logic to pick a set of nonconflicting behaviors.
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public AIConfig AIBuilder(string type, int rWeight){
		if(rWeight > (maxStatValue * numOfStats)){
			Debug.LogError("Weight is too high for the number of stats given their maximums.");
			Debug.Break();
		}
		StatCollectionClass member = gameObject.GetComponent<StatCollectionClass>(); 
		decisionType = type;
		bool AImade = false;
		bool isMelee = false;
		bool isHybrid = false;
		bool isAggressive = false;		
		bool isStalker = false;		
		bool hasMagic = false;		
		bool isCautious = false;

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/*
			We need to vary the stats based on weight.
			Look at the weight and see if the even split (average) is between the values is over statMax (our maximum 
			normalized stat value).
			If it is, assign randomly from a range between statmin to statMax.
			Otherwise assign randomly statmin to the dividend of weight total with # of stats and add the modulus 
			remainder. This will give stats that fairly equalized to the weight. Let AdjustStats fix any underages or
			overages. 
		*/
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		int[] split = new int[numOfStats];

		if ((rWeight / numOfStats) > maxStatValue) { //Upper bound on stats, might not be needed if things are weighted right.
			int remain = rWeight;
			for (int i = 0; i < numOfStats; i++) {
				split [i] = Random.Range (minStatValue, maxStatValue);
				//For Testing: print ("Random value for stat #" + i.ToString () + " : " + split [i].ToString ());
				remain = remain - split [i];
			}
		} else {
			for (int i = 0; i < numOfStats; i++) {
				split [i] = Random.Range (minStatValue, (rWeight / numOfStats) + rWeight % numOfStats);
				//For Testing: print ("Random value for stat #" + i.ToString () + " : " + split [i].ToString ());
			}
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/*
		 	Sum all the stats together to so we can compare it to weight.
		 	Calculate the expected sum and actual sum diff for our AdjustStats call.
		*/
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		int sumR;
		AdjustStats (split, rWeight);

		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/*
			 Finally check that the final sum is correct.
		 	 If the sum is correct set AImade true and carry on!
			 Otherwise throw nasty errors and annoy us.
			 TODO Have this loop back to AdjustStats, error if it fails to properly adjust it again.(We should never
			 hit this second call ideally.)
		*/
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		//Testing
		//for(int j = 0; j < numOfStats; j++){
			//print("Final random stats" + split[j].ToString());
		//}
		//

		sumR = sumArray (split);
		//print ("The sum is " + sumR.ToString () + " It is supposed to be " + rWeight.ToString ());

		if (sumR == rWeight) {	
			AImade = true;	
		}


		if (AImade) {
			AssignStats(split);
		} else {
			Debug.LogError ("AIManager failed to create a proper AIBuild");
		}

		AIConfig config = new AIConfig ();	

		#pragma warning disable
		BehaviorTypes assignBehavior = new BehaviorTypes ();
		#pragma warning restore

		switch(type){
		case "mob":

			if(member.health > 0){
				//Visual adjustments for health thresholds.
			}
			if(member.mana > 0){
				//Visual adjustments for mana thresholds.
				hasMagic = true;
			}
			if(member.strength > (weightedLevel / numOfStats)){
				isMelee = true;
				isAggressive = true;
				//Visual adjustments for being melee.
			}
			if(member.intellect > (weightedLevel / numOfStats) && member.strength >= member.intellect){
				isCautious = true;
				isHybrid = true;
				//Visual adjustments for being cautious.
			}
			if(member.intellect > (weightedLevel / numOfStats) && member.intellect >= member.strength){
				isStalker = true;
				//Visual adjustments for being a stalker.
			}

			config.isMelee = isMelee;
			config.isHybrid = isHybrid;
			config.hasMagic = hasMagic;
			config.isAggressive = isAggressive;
			config.isCautious = isCautious;
			config.isStalker = isStalker;

			if(isHybrid)
				config = assignBehavior.intializeHybridGeneric (config);
			else if(isMelee)
				config = assignBehavior.intializeMeleeGeneric (config);
			else
				config = assignBehavior.intializeRangedGeneric (config);


			AssignModifiers(config);
			config.isMade = AImade;
			break;
		case "npc":
			//Stuff might go here?
			break;
		case "boss":
			//Boss mechanics to be decided.
			break;
		default:
			Debug.LogError (type + ": is not a supported type.");
			break;
		}


		print(config.ToString ());

		return config;
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/*
		Function to assign stats from our normalized sum, to their variables in our collection of stats.
		Apply any stat modifiers to de-normalize the values in the array. These will be balance constants.
		IN: int array of finalized stats, a reference to the statcollection being assigned
		OUT: Changes stat collection by reference, no returns.
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	private void AssignStats(int[] split){

		if (numOfStats < 3) {
			return;
		}

		StatCollectionClass toSet = gameObject.GetComponent<StatCollectionClass> ();

		/// Health
		toSet.health = split[0];
		/// Strength
		toSet.strength = split [1];
		/// Intellect
		toSet.intellect = split [2];
		/// XP
		toSet.xp = toSet.strength + toSet.intellect;
		/// Level
		toSet.playerLevel = sumArray(split);

		print(toSet.ToString ());
	}


	private void AssignModifiers(AIConfig ai){
		StatCollectionClass toSet = gameObject.GetComponent<StatCollectionClass> ();

		//toSet.initialHealth = 100.0f;

		if (ai.healthMultiplier > 0) {
			toSet.initialHealth = toSet.health * ai.healthMultiplier;
			toSet.health = toSet.initialHealth;
			Debug.Log("Assigning health");
		}
		else
			Debug.LogError ("Health and health multiplier must be intialized before applying AssignModifiers.");

		if (ai.attackMultiplier > 0) {
			toSet.baseMeleeDamage = toSet.strength * ai.attackMultiplier;
			toSet.baseRangedDamage = toSet.intellect * ai.attackMultiplier;
			Debug.Log ("Assigning baseattack");
		}
		else
			Debug.LogError ("Strength/Intellect and attack multiplier must be intialized before applying AssignModifiers.");

		if (toSet.strength > 0 && toSet.intellect > 0) {
			toSet.baseDefense = toSet.strength / 2 + toSet.intellect / 2;
			Debug.Log ("Assigning basedefense");
		}
		else
			Debug.LogError ("Strength and intellect must be intialized before applying AssignModifiers.");

		if (ai.manaMultiplier > 0) {
			toSet.initialMana = toSet.intellect * ai.manaMultiplier;
			toSet.mana = toSet.initialMana;
			Debug.Log("Assigning mana");
		}
		else
			Debug.LogError ("intellect and mana multiplier must be intialized before applying AssignModifiers.");
	}
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/*
		Check the random distribution of normalized stats for consistency with the weight that the entity is 
		required to have.
	 	Look at the diff between required sum and actual sum.
	 	If the diff is 0 do nothing.
	 	If the diff is less then zero remove stats randomly until sum is right. Check to make sure random stat is not
	 	already at or below the min.
	 	If the diff is greater than zero, add stats until the sum is right. Check to make sure random stat is not
	 	above or equal to the max.
	 	IN: array of ints, integer weight of the AI
	 	OUT: adjusts the array by reference, no returns.
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	private void AdjustStats(int[] split, int rWeight){

		int sumR = sumArray (split);
		int diff = rWeight - sumR;
		if (diff != 0) {
			if(diff < 0){
				//For testing: print ("We have an abundance of: " + diff.ToString () + " stats.");	
				for (int m = 0; m > diff; m--) {
					sumR = sumArray (split);
					//For testing: print ("The sum is " + sumR.ToString () + " It is supposed to be " + rWeight.ToString ());
					if (sumR == rWeight) {
						break;
					}
					int ranInt = Random.Range (0, numOfStats - 1);
					if(split[ranInt] <= minStatValue){
						m++;
					}
					else{
						split [ranInt] = split [ranInt] - 1;
					}
				}
			}
			else if(diff > 0){
				//For testing: print ("We are missing: " + diff.ToString () + " stats.");
				for (int m = 0; m < diff; m++) {
					sumR = sumArray (split);
					//For testing: print ("The sum is " + sumR.ToString () + " It is supposed to be " + rWeight.ToString ());
					if (sumR == rWeight) {
						break;
					}
					
					int ranInt = Random.Range (0, numOfStats - 1);
					
					if(split[ranInt] > maxStatValue){
						m--;
					}
					else{
						split [ranInt] = split [ranInt] + 1;
					}
				}
			}
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/*
			Helper function to sum the array of normalized stats. I ended up using this code alot so it has been
			refactored.
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	private int sumArray(int[] split){
		int sumR =0;
		for (int k = 0; k < numOfStats; k++) {
			sumR = sumR + split [k];
		}
		return sumR;
	}
}
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/* 
	
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////