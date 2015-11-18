using UnityEngine;
using System.Collections;

public class WorldGenerate : MonoBehaviour {
	public GameObject wall;
	//add for generate items
	public GameObject Bow;
	public GameObject Sword;
	public GameObject Armor;

	public float wallDimensions;
	public int mapWidth = 1;
	public int mapHeight = 1;
	public float mapOffsetX = 0f; // Used to determine where the algorithm will start from
	public float mapOffsetY = 0f; // Used to determine where the algorithm will start from
	public int wallsInGroup = 1;
	public int minWallsInLevel = 1;
	public int maxWallsInLevel = 1;

	private GameObject[ , ] wallSet; // Storage for walls

	// Use this for initialization
	void Start () {
		wallSet = new GameObject[mapWidth, mapHeight]; // This array now represents the entire  map of the level
		for(int i = 0; i < mapWidth; i++) {
			for(int j = 0; j < mapHeight; j++){
				wallSet[i, j] = null;
			}
		}
		GenerateWalls (); // Generates random walls and adds them into the wallSet array
		GenerateSword (); //Generates random positon items into wallSet array
		GenerateBow ();
		GenerateArmor ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Tests if x,y is within the bounds of the map
	bool WithinBounds (int x, int y) {
		return (x >= 0 && x < mapWidth && y >= 0 && y < mapHeight);
	}

	// Returns the the wall at x,y if one exists and was 
	// created by this object. Otherwise returns null
	GameObject GetWall (int x, int y){
		if (WithinBounds(x, y)) {
			return wallSet [x, y];
		} else {
			return null;
		}
	}

	 // Places a wall object at x,y if one that was created 
	// by this object does not already exist	 
	void PutWall (int x, int y) {
		if (WithinBounds (x, y)) {
			if (wallSet [x, y] == null) {
				GameObject tempWall = Instantiate (wall) as GameObject;
				tempWall.tag = "Wall";
				tempWall.transform.position = new Vector3 (x * wallDimensions - mapOffsetX, y * wallDimensions - mapOffsetY, 0f);
				wallSet [x, y] = tempWall;
			}
		}
	}

	// place a item object at x,y if it in bounds and nothing here
	void PutItem (int x, int y,GameObject item) {
		if (WithinBounds (x, y)) {
			if (wallSet [x, y] == null) {
				GameObject temp = Instantiate (item) as GameObject;
				temp.transform.position = new Vector3 (x - mapOffsetX, y - mapOffsetY, 0f);
				wallSet [x, y] = temp;
			}
		}
	}

	// place a item object at x,y if it in bounds and nothing here
	void PutItem (int x, int y,GameObject item, string tag) {
		if (WithinBounds (x, y)) {
			if (wallSet [x, y] == null) {
				GameObject temp = Instantiate (item) as GameObject;
				temp.tag = tag;
				temp.transform.position = new Vector3 (x - mapOffsetX, y - mapOffsetY, 0f);
				wallSet [x, y] = temp;
			}
		}
	}


	// Removes a wall object at x,y if one exists
	// and was created by this object
	void RemoveWall (int x, int y) {
		if (WithinBounds (x, y)) {
			if (wallSet [x, y] != null) {
				Destroy (wallSet[x, y]);
				wallSet[x, y] = null;
			}
		}
	}




	// Generates a squiggly line of wall objects starting at the position startX,startY
	// Generates a maximum of numWall number of walls
	// If the line would go off the map, the function will return to the starting point 
	void GenerateWallGroup (int startX, int startY, int numWalls) {
		int x = startX; //the "pointer" x coord
		int y = startY; //the "pointer" y coord
		int direction = Random.Range (0, 4);

		for (int i = 0; i < numWalls; i++) {
			switch(direction) {	// Moves the "pointer" position to the next location
			case 0: //going right
				while(WithinBounds (x,y) && GetWall(x, y) != null) {
					x++;
				}
				break;
			case 1: //going up
				while(WithinBounds (x,y) && GetWall(x, y) != null) {
					y--;
				}
				break;
			case 2: //going left
				while(WithinBounds (x,y) && GetWall(x, y) != null) {
					x--;
				}
				break;
			case 3: //going down
				while(WithinBounds (x,y) && GetWall(x, y) != null) {
					y++;
				}
				break;
			default:
				break;
			}

			if (WithinBounds (x,y)) {
				PutWall (x,y);	// Places a wall at the "pointer" position
			} else {
				x = startX;	// Moves the "pointer" back to the start position
				y = startY; // if the pointer is out of the map bounds
			}

			switch (Random.Range(0,3)) { // Change the direction of the pointer (turn left, go straight, turn right)
			case 0:
				direction--;
				if (direction<0) direction=3;
				break;
			case 2:
				direction++;
				if (direction>3) direction=0;
				break;
			default:
				break;
			}

		}
	}

	// Generates several squiggly lines of walls
	void GenerateWalls () {
		for (int i=0; i<Random.Range(minWallsInLevel,maxWallsInLevel); i++) {
			GenerateWallGroup (Random.Range (0, mapWidth), Random.Range (0, mapHeight), Random.Range (1, wallsInGroup));
		}
	}

	//Generates items by get random x,y in map and check is there any other object
	void GenerateSword() {
		int x;
		int y;
		do {
			x = Random.Range (0, mapWidth); //the "pointer" x coord
			y = Random.Range (0, mapHeight); //the "pointer" y coord
		} while(!(WithinBounds (x,y)&& wallSet [x, y] == null));

		PutItem (x,y,Sword);
	}

	void GenerateBow() {
		int x;
		int y;
		do {
			x = Random.Range (0, mapWidth); //the "pointer" x coord
			y = Random.Range (0, mapHeight); //the "pointer" y coord
		} while(!(WithinBounds (x,y)&& wallSet [x, y] == null));
		
		PutItem (x,y,Bow);
	}

	void GenerateArmor() {
		int x;
		int y;
		do {
			x = Random.Range (0, mapWidth); //the "pointer" x coord
			y = Random.Range (0, mapHeight); //the "pointer" y coord
		} while(!(WithinBounds (x,y)&& wallSet [x, y] == null));
		
		PutItem (x,y,Armor);
	}

}
