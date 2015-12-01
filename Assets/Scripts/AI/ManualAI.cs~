using UnityEngine;
using System.Collections;

public class ManualAI : MonoBehaviour {

	public AIConfig LevelOneBoss(AIConfig config){

		GameObject unitySucks = new GameObject ();

		config.statExchange = unitySucks.AddComponent<StatCollectionClass> ();
		
		if (config.statExchange.Equals (null)) {
			Debug.LogError("Unity sucks");
		}

		int[] numbers = new int[3] {10, 10, 10};

		ProceduralAI aihelp = new ProceduralAI();
		aihelp.AssignStats(numbers, config.statExchange);

		BehaviorTypes besetup = new BehaviorTypes();
		config = besetup.intializeHyrbidBoss(config);

		aihelp.AssignModifiers(config);

		return config;
	}
}
