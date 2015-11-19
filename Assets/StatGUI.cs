using UnityEngine;
using System.Collections;

public class StatGUI : MonoBehaviour {

	//Rectangle for GUI
	Rect winPos;

	//Stat Collection attached to player
	StatCollectionClass stats;

	//bool to decide if showing
	public bool showing = false;

	// Use this for initialization
	void Start () {

		//initializing
		winPos = new Rect (((Screen.width / 2) - 260), ((Screen.height / 2) - 150), 512, 256);
		stats = gameObject.GetComponent<StatCollectionClass>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI ()
	{
		//if GUI is showing, setting size, title, etc.
		if (showing)
		{
			winPos = GUI.Window(3, winPos, StatWindow, "Stats:");
		}
	}
	
	void StatWindow(int ID)
	{
				GUILayout.Box("stat info...");
	}
}
