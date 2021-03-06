﻿using UnityEngine;
using System.Collections;

public class ProceduralAI : MonoBehaviour {
	
	public int numOfStats = 3;

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/*
		The AIBuilder method creates a config for a AI enabled entity.
		Currently it psuedorandomly assigns stats, eventually options for chosen stats and weighted random stats 
		will be added.
		The AIBuilder will require switch logic to pick a set of nonconflicting behaviors.
	*/
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////	
	public AIConfig AIBuilder (string type, int rWeight, int minStatValue, int maxStatValue){
		if(rWeight > (maxStatValue * numOfStats)){
			Debug.LogError("Weight is too high for the number of stats given their maximums.");
			return default(AIConfig);
		}

		GameObject unitySucks = new GameObject ();

		AIConfig config = new AIConfig ();
		config.statExchange = unitySucks.AddComponent<StatCollectionClass> ();

		if (config.statExchange.Equals (null)) {
			Debug.LogError("Unity sucks");
		}

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
				remain = remain - split [i];
			}
		} else {
			for (int i = 0; i < numOfStats; i++) {
				split [i] = Random.Range (minStatValue, (rWeight / numOfStats) + rWeight % numOfStats);
			}
		}
		
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/*
		 	Sum all the stats together to so we can compare it to weight.
		 	Calculate the expected sum and actual sum diff for our AdjustStats call.
		*/
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
		int sumR;
		AdjustStats (split, rWeight, minStatValue, maxStatValue);
		
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/*
			 Finally check that the final sum is correct.
		 	 If the sum is correct set AImade true and carry on!
			 Otherwise throw nasty errors and annoy us.
			 TODO Have this loop back to AdjustStats, error if it fails to properly adjust it again.(We should never
			 hit this second call ideally.)
		*/
		////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		
		sumR = sumArray (split);		
		if (sumR == rWeight) {	
			AImade = true;	
		}
			
		if (AImade) {
			AssignStats(split, unitySucks.GetComponent<StatCollectionClass>());
		} else {
			Debug.LogError ("AIManager failed to create a proper AIBuild");
		}
			
		#pragma warning disable
		BehaviorTypes assignBehavior = new BehaviorTypes ();
		#pragma warning restore
		
		switch(type){
		case "mob":
			if(config.statExchange.health > 0){
			}
			if(config.statExchange.mana > 0){
				hasMagic = true;
			}
			if(config.statExchange.strength > 6){
				isMelee = true;
				isAggressive = true;
			}
			if(config.statExchange.intellect > 6 && config.statExchange.strength >= config.statExchange.intellect){
				isCautious = true;
				isHybrid = true;
			}
			if(config.statExchange.intellect > 6 && config.statExchange.intellect >= config.statExchange.strength){
				isStalker = true;
			}


			if(isMelee && !isHybrid){
				float str = config.statExchange.strength;
				float scale = (1.5f * (1f + str/50f));
				transform.localScale = new Vector3(scale, scale, 0);
				config.AIType = "Melee";
			}else if(isHybrid){
				transform.localScale = new Vector3(2f, 2f, 0);
				config.AIType = "Hybrid";
			}else{
				transform.localScale = new Vector3(1.8f, 1.8f, 0);
				config.AIType = "Ranged";
			}

			config.isMelee = isMelee;
			config.isHybrid = isHybrid;
			config.hasMagic = hasMagic;
			config.isAggressive = isAggressive;
			config.isCautious = isCautious;
			config.isStalker = isStalker;

			if(isHybrid)
				config = assignBehavior.intializeHybridGeneric(config);
			else if(isMelee)
				config = assignBehavior.intializeMeleeGeneric(config);
			else
				config = assignBehavior.intializeRangedGeneric(config);

			
			AssignModifiers(config);
			config.isMade = AImade;
			break;
		case "npc":
			//Stuff might go here?
			break;
		case "boss":
			if(config.statExchange.health > 0){
			}
			if(config.statExchange.mana > 0){
				hasMagic = true;
			}
			if(config.statExchange.strength > 6){
				isMelee = true;
				isAggressive = true;
			}
			if(config.statExchange.intellect > 6 && config.statExchange.strength >= config.statExchange.intellect){
				isCautious = true;
				isHybrid = true;
			}
			if(config.statExchange.intellect > 6 && config.statExchange.intellect >= config.statExchange.strength){
				isStalker = true;
			}

			if(isMelee && !isHybrid){
				float str = config.statExchange.strength;
				float scale = (1.5f * (1f + str/50f));
				transform.localScale = new Vector3(scale, scale, 0);
				config.AIType = "Melee";
			}else if(isHybrid){
				transform.localScale = new Vector3(2f, 2f, 0);
				config.AIType = "Hybrid";
			}else{
				transform.localScale = new Vector3(1.8f, 1.8f, 0);
				config.AIType = "Ranged";
			}
			config.isMelee = isMelee;
			config.isHybrid = isHybrid;
			config.hasMagic = hasMagic;
			config.isAggressive = isAggressive;
			config.isCautious = isCautious;
			config.isStalker = isStalker;
			
			if(isHybrid)
				config = assignBehavior.intializeHybridGeneric(config);
			else if(isMelee)
				config = assignBehavior.intializeMeleeGeneric(config);
			else
				config = assignBehavior.intializeRangedGeneric(config);
			
			
			AssignModifiers(config);
			config.isMade = AImade;
			break;
		default:
			Debug.LogError (type + ": is not a supported type.");
			break;
		}
		print(config.ToString ());

		StartCoroutine ("waitToDestroy", unitySucks);
		//Destroy (unitySucks);
		return config;
	}

	public void AssignStats(int[] split, StatCollectionClass toSet){
		if (numOfStats < 3) {
			return;
		}	
		//StatCollectionClass toSet = stats.GetComponent<StatCollectionClass> ();
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
	}
	
	
	public void AssignModifiers(AIConfig ai){
		
		//toSet.initialHealth = 100.0f;

		if (ai.statExchange.Equals (null)) {
			Debug.LogError("statcollection not found |assignmodifiers|");
			return;
		}
		
		if (ai.healthMultiplier >= 0) {
			ai.statExchange.initialHealth = ai.statExchange.health * ai.healthMultiplier;
			ai.statExchange.health = ai.statExchange.initialHealth;
			//Debug.Log("Assigning health");
		}
		else
			Debug.LogError ("Health and health multiplier must be intialized before applying AssignModifiers.");
		
		if (ai.attackMultiplier >= 0) {
			ai.statExchange.baseMeleeDamage = ai.statExchange.strength * ai.attackMultiplier;
			ai.statExchange.baseRangedDamage = ai.statExchange.intellect * ai.attackMultiplier;
			//Debug.Log ("Assigning baseattack");
		}
		else
			Debug.LogError ("Strength/Intellect and attack multiplier must be intialized before applying AssignModifiers.");
		
		if (ai.statExchange.strength >= 0 && ai.statExchange.intellect >= 0) {
			ai.statExchange.baseDefense = ai.statExchange.strength / 2 + ai.statExchange.intellect / 2;
			//Debug.Log ("Assigning basedefense");
		}
		else
			Debug.LogError ("Strength and intellect must be intialized before applying AssignModifiers.");
		
		if (ai.manaMultiplier >= 0) {
			ai.statExchange.initialMana = ai.statExchange.intellect * ai.manaMultiplier;
			ai.statExchange.mana = ai.statExchange.initialMana;
			//Debug.Log("Assigning mana");
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
	private void AdjustStats(int[] split, int rWeight, int minStatValue, int maxStatValue){
		
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

	IEnumerator waitToDestroy(GameObject something)
	{
		yield return new WaitForSeconds(1.0f);
		Destroy (something);
	}
}
