using UnityEngine;
using System.Collections;

public class BehaviorTypes : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	public AIConfig intializeRangedGeneric(AIConfig ai){
		ai.threatZone = 25.00f;
		ai.idealRange = 10.0f;
		ai.friendlyMinDis = 5.0f;
		ai.kiteDistance = 20.0f;
		ai.minHealth = 3;
		ai.healthMultiplier = 1.0f;
		ai.attackMultiplier = 1.0f;
		ai.manaMultiplier = 1.0f;
		ai.movementSpeed = 10f;
		return ai;
	}

	public AIConfig intializeMeleeGeneric(AIConfig ai){
		ai.threatZone = 20.00f;
		ai.idealRange = 1.0f;
		ai.friendlyMinDis = 5.0f;
		ai.kiteDistance = 10.0f;
		ai.minHealth = 1;
		ai.healthMultiplier = 1.0f;
		ai.attackMultiplier = 1.0f;
		ai.manaMultiplier = 1.0f;
		ai.movementSpeed = 12f;
		return ai;
	}

	public AIConfig intializeHybridGeneric(AIConfig ai){
		ai.threatZone = 25.00f;
		ai.idealRange = 5.0f;
		ai.friendlyMinDis = 5.0f;
		ai.kiteDistance = 20.0f;
		ai.minHealth = 3;
		ai.healthMultiplier = 1.0f;
		ai.attackMultiplier = 1.0f;
		ai.manaMultiplier = 1.0f;
		ai.movementSpeed = 10f;
		return ai;
	}

	public void intializeRangedBoss(AIConfig ai){

	}

	public void intializeMeleeBoss(AIConfig ai){

	}

	public void intializeHyrbidBoss(AIConfig ai){

	}


}
