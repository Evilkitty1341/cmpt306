using UnityEngine;
using System.Collections;

// This class holds the various stats that will be used by the player and enemies in our game
// Each object will have their own version of a stat collection
public class StatCollectionClass : MonoBehaviour {

	public bool godMode;

	public int playerDirection;
	
	public float health;

	public float initialHealth;
	
	public float mana;

	public float initialMana;
	
	//connect to player's normal attack damage, uesd for item.
	public float damage;

	public float baseMeleeDamage;

	public float baseRangedDamage;

	public float baseMagicDamage;
	
	//connect to player's ablity to decrease damage from enemy, linked with item.
	public float defend;

	public float baseDefense;
	
	public int strength;
	
	public int intellect;

	public int agility;
	
	public int xp;

	public float xpNextLevel;
	
	public int playerLevel;

	public int skillPoint;
	
	//add bool for item get and item equip for item manager
	public bool itemSword;
	
	public bool SwordEquip;
	
	public bool itemArmor;
	
	public bool ArmorEquip;
	
	public bool itemBow;
	
	public bool BowEquip;
	
	//add end
	
	public bool FireBallUnlocked=true;
	
	public bool FireBreathUnlocked;
	
	public bool SunStrikeUnlocked;

	public void copyStats(StatCollectionClass target){

		godMode = target.godMode;
		health = target.health;
		initialHealth = target.initialHealth;
		mana = target.mana;
		initialMana = target.initialMana;
		damage = target.damage;
		baseMeleeDamage = target.baseMeleeDamage;
		baseRangedDamage = target.baseRangedDamage;
		baseMagicDamage = target.baseMagicDamage;
		defend = target.defend;
		baseDefense = target.baseDefense;
		strength = target.strength;
		intellect = target.intellect;
		agility = target.agility;
		xp = target.xp;
		playerLevel = target.playerLevel;
		itemSword = target.itemSword;
		SwordEquip = target.SwordEquip;
		itemArmor = target.itemArmor;
		ArmorEquip = target.ArmorEquip;
		itemBow = target.itemBow;
		BowEquip = target.BowEquip;
		FireBallUnlocked = target.FireBallUnlocked;
		FireBreathUnlocked = target.FireBreathUnlocked;
		SunStrikeUnlocked = target.SunStrikeUnlocked;
	}

	public void doDamage(float damage){
		if (!godMode) {
			Debug.Log ("Godmode is set to: " + godMode.ToString() + " and " + damage.ToString() + " was applied to health.");
			float check = damage - baseDefense - defend;
			if (check >= health && check < 0) {
				health = 0;
			} else {
				health -= check;
			}
		}
	}
}

