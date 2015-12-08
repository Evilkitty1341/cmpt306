using UnityEngine;
using System.Collections;

public class flameThrowerAtack : MonoBehaviour {

	StatCollectionClass player;

	// Use this for initialization
	void Start () {
		player = gameObject.GetComponent<StatCollectionClass>();
	}
	
	// Update is called once per frame
	void Update () {

		if (player.playerDirection == 1)
		{
			transform.rotation = Quaternion.AngleAxis (0, Vector3.forward);
		}
		else if (player.playerDirection == 2)
		{
			transform.rotation = Quaternion.AngleAxis (-90, Vector3.forward);
		}
		else if (player.playerDirection == 3)
		{
			transform.rotation = Quaternion.AngleAxis (180, Vector3.forward);
		}
		else if (player.playerDirection == 4)
		{
			transform.rotation = Quaternion.AngleAxis (270, Vector3.forward);
		}
	
	}
}
