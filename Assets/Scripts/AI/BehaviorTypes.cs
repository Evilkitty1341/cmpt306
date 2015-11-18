using UnityEngine;
using System.Collections;

public class BehaviorTypes : MonoBehaviour {

	static float movSpeedScale = 2.5f;

	float bThreatZone = 25.00f;
	float bIdealRange = 10.0f;
	float bFriendlyMinDis = 5.0f;
	float bKiteDistance = 20.0f;
	float bMinHealth = 3;
	float bHealthMultiplier = 1.0f;
	float bAttackMultiplier = 1.0f;
	float bManaMultiplier = 1.0f;
	float bMovementSpeed = 10.0f;
	
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
		ai.movementSpeed = 10.0f * movSpeedScale;
		return ai;
	}

	public AIConfig intializeMeleeGeneric(AIConfig ai){
		ai.threatZone = 20.00f;
		ai.idealRange = 0f;
		ai.friendlyMinDis = 5.0f;
		ai.kiteDistance = 10.0f;
		ai.minHealth = 1;
		ai.healthMultiplier = 1.0f;
		ai.attackMultiplier = 1.0f;
		ai.manaMultiplier = 1.0f;
		ai.movementSpeed = 12.0f * movSpeedScale;
		return ai;
	}

	public AIConfig intializeHybridGeneric(AIConfig ai){
		ai.threatZone = 25.00f;
		ai.idealRange = 2.0f;
		ai.friendlyMinDis = 5.0f;
		ai.kiteDistance = 20.0f;
		ai.minHealth = 3;
		ai.healthMultiplier = 1.0f;
		ai.attackMultiplier = 1.0f;
		ai.manaMultiplier = 1.0f;
		ai.movementSpeed = 10.0f * movSpeedScale;
		return ai;
	}

	public void intializeRangedBoss(AIConfig ai){

	}

	public void intializeMeleeBoss(AIConfig ai){

	}

	public void intializeHyrbidBoss(AIConfig ai){

	}


}
