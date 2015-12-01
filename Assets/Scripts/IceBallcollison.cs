using UnityEngine;
using System.Collections;

public class IceBallcollison : MonoBehaviour {

	StatCollectionClass enemyStat;

	void Start () {
	
	}
	void OnTriggerEnter2D(Collider2D col){
	
		if (col.gameObject.tag == "Enemy") {

			enemyStat= col.GetComponent<StatCollectionClass>();

			enemyStat.doDamage(10);

			if(enemyStat.health==0 && enemyStat.initialHealth>0)
			{
				Destroy(col);
			}
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
