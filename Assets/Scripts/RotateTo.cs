using UnityEngine;
using System.Collections;

public class RotateTo : MonoBehaviour {

	/*****
	 *  have the code to check and change the string is in AllQuests, might be a bit awkward
	 * but it's late and I can't think of a less awkward place to put it.. so...
	 * -kas
	 * *****/
	
	//Quest1
	public GameObject Items1;
	public GameObject QuestGiver1;
	
	//Quest2
	public GameObject QuestGiver2;
	
	
	//Quest3
	public GameObject Guard1;
	public GameObject Gigabyte;
	public GameObject Selina;
	public GameObject Guard2;
	
	public GameObject Boss;

	private GameObject Current;

	private Vector3 target;

	string rotateTo;

	// Use this for initialization
	void Start () {

		Items1 = GameObject.Find ("Item1Prefab(Clone)");
		QuestGiver1 = GameObject.Find("QuestGiver1Prefab");
		QuestGiver2 = GameObject.Find("QuestGiver2Prefab(Clone)");
		Guard2 = GameObject.Find("Guard2");
		Guard1 = GameObject.Find("Guard1");
		Selina = GameObject.Find("Town1");
		Gigabyte = GameObject.Find("townRobot");


		if(Items1 == null)
		{
			print ("Items1 is null!!");
		}
		if (QuestGiver1 == null) {
			print ("QuestGivers1 is null");
		}

		target = Vector3.zero;
		Current = QuestGiver1;
		target = Current.transform.position;
		this.transform.position = (GameObject.FindGameObjectWithTag ("Player")).transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = (GameObject.FindGameObjectWithTag ("Player")).transform.position;
		target = Current.transform.position;

		float rotZ = Mathf.Atan2 (target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (rotZ + 90f, Vector3.forward);
	}

	//This function checks the string and points the arrow towards that game object
	public void setRotateTo(string rotateTo)
	{
		if(rotateTo == "QuestGiver1")
		{
			Current = QuestGiver1;
		}
		else if(rotateTo == "Items1")
		{
			Current = Items1;
		}
		else if(rotateTo == "QuestGiver2")
		{
			Current = QuestGiver2;
		}
		else if(rotateTo == "Guard1")
		{
			Current = Guard1;
		}
		else if(rotateTo == "Gigabyte")
		{
			Current = Gigabyte;
		}
		else if(rotateTo == "Selina")
		{
			Current = Selina;
		}
		else if(rotateTo == "Guard2")
		{
			Current = Guard2;
		}
		else if(rotateTo == "Boss")
		{
			Current = Boss;
		}
	}
}
