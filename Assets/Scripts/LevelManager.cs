using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	public GameObject player;
	
	public StatCollectionClass stat;
	
	//new add
	public GameObject levelUpWord;
	
	public GameObject levelUpEffect;
	
	private bool isHidden = true;
	//new add
	
	
	// Use this for initialization
	//set all values related to level a default with level 1
	void Start () {
		
		stat.xp = 0;
		
		stat.xpNextLevel = 50;
		
		stat.playerLevel = 1;
		
		stat.agility = 1;
		
		stat.intellect = 1;
		
		stat.strength = 1;
		
	}
	
	//if level up update each value to new one
	void Update () {
		
		// current xp bigger or equal to xp needed for next level
		if (stat.xp >= stat.xpNextLevel) {
			
			//new add
			levelUpEffect.SetActive(true);
			
			levelUpWord.SetActive(true);
			
			Invoke("MakeEffectDisappear", 1f);
			
			Invoke("MakeWordDisappear", 1f);
			//new add
			
			stat.xp = 0;
			
			stat.playerLevel++;
			
			stat.skillPoint++;
			
			stat.xpNextLevel *= 2;
			
			stat.agility++;
			
			stat.intellect++;
			
			stat.strength++;
			
			stat.initialHealth += 20;
			
			stat.health = stat.initialHealth;
			
			stat.initialMana += 20;
			
			stat.mana = stat.initialMana;
			
		}
		
		stat.baseMagicDamage = stat.intellect * 0.5f;
		
		stat.baseMeleeDamage = stat.strength * 0.5f;
		
		stat.baseRangedDamage = stat.agility * 0.5f;
		
		stat.baseDefense = stat.agility * 0.5f;
		
		
		
	}
	
	//new add
	void MakeEffectDisappear () {
		
		
		levelUpEffect.SetActive (false);
		
	}
	
	void MakeWordDisappear () {
		
		
		levelUpWord.SetActive (false);
		
	}
	//new add
	
}
