﻿using UnityEngine;
using System.Collections;

public class BehaviorTypes : MonoBehaviour {

	float movSpMultiplier = 10; // Global stat to scale all speeds.

	// Use this for initialization
	void Start () {
	
	}

	public AIConfig intializeRangedGeneric(AIConfig ai){
		ai.threatZone = 12.00f;
		ai.idealRange = 7.5f;
		ai.friendlyMinDis = 5.0f;
		ai.kiteDistance = 10.0f;
		ai.minHealth = 3;
		ai.healthMultiplier = 2.0f;
		ai.attackMultiplier = 0.75f;
		ai.manaMultiplier = 0.0f;
		ai.movementSpeed = 6f * movSpMultiplier;
		return ai;
	}

	public AIConfig intializeMeleeGeneric(AIConfig ai){
		ai.threatZone = 12.00f;
		ai.idealRange = 1.5f;
		ai.friendlyMinDis = 5.0f;
		ai.kiteDistance = 12.0f;
		ai.minHealth = 1;
		ai.healthMultiplier = 5.0f;
		ai.attackMultiplier = 1.0f;
		ai.manaMultiplier = 0.0f;
		ai.movementSpeed = 7f * movSpMultiplier;
		return ai;
	}

	public AIConfig intializeHybridGeneric(AIConfig ai){
		ai.threatZone = 25.00f;
		ai.idealRange = 5.0f;
		ai.friendlyMinDis = 5.0f;
		ai.kiteDistance = 20.0f;
		ai.minHealth = 3;
		ai.healthMultiplier = 4.5f;
		ai.attackMultiplier = 1.0f;
		ai.manaMultiplier = 1.0f;
		ai.movementSpeed = 8f * movSpMultiplier;
		return ai;
	}

	public AIConfig intializeTest(AIConfig ai){
		ai.threatZone = 25.00f;
		ai.idealRange = 10.0f;
		ai.friendlyMinDis = 5.0f;
		ai.kiteDistance = 20.0f;
		ai.minHealth = 3;
		ai.healthMultiplier = 1.0f;
		ai.attackMultiplier = 1.0f;
		ai.manaMultiplier = 1.0f;
		ai.movementSpeed = 10f * movSpMultiplier;
		return ai;
	}

	public AIConfig intializeRangedBoss(AIConfig ai){
		ai.AIType = "boss";
		ai.threatZone = 25.00f;
		ai.idealRange = 10.0f;
		ai.friendlyMinDis = 5.0f;
		ai.kiteDistance = 20.0f;
		ai.minHealth = 3;
		ai.healthMultiplier = 1.0f;
		ai.attackMultiplier = 1.0f;
		ai.manaMultiplier = 1.0f;
		ai.movementSpeed = 10f * movSpMultiplier;
		return ai;

	}

	public AIConfig intializeMeleeBoss(AIConfig ai){
		ai.AIType = "boss";
		ai.threatZone = 25.00f;
		ai.idealRange = 10.0f;
		ai.friendlyMinDis = 5.0f;
		ai.kiteDistance = 20.0f;
		ai.minHealth = 3;
		ai.healthMultiplier = 1.0f;
		ai.attackMultiplier = 1.0f;
		ai.manaMultiplier = 1.0f;
		ai.movementSpeed = 10f * movSpMultiplier;
		return ai;

	}

	public AIConfig intializeHyrbidBoss(AIConfig ai){
		ai.AIType = "boss";
		ai.threatZone = 25.00f;
		ai.idealRange = 10.0f;
		ai.friendlyMinDis = 5.0f;
		ai.kiteDistance = 20.0f;
		ai.minHealth = 3;
		ai.healthMultiplier = 20.0f;
		ai.attackMultiplier = 3.0f;
		ai.manaMultiplier = 10.0f;
		ai.movementSpeed = 10f * movSpMultiplier;
		return ai;

	}


}
