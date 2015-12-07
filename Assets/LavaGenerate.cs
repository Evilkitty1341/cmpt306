using UnityEngine;
using System.Collections;
//using System;
using System.IO;

public class LavaGenerate : MonoBehaviour {
	public GameObject wall;
	public GameObject rocks;
	public GameObject obstacle;
	public GameObject spawn;
	public GameObject flower;
	public GameObject tree;
	public GameObject stone;
	//add for generate items
	public GameObject Bow;
	public GameObject Sword;
	public GameObject Armor;
	//set up room numbers
	public int XroomNum;
	public int YroomNum;
	
	public int RoomStartX;
	public int RoomStartY;
	
	public float wallDimensions;
	public int mapWidth = 1;
	public int mapHeight = 1;
	public int type = 0;
	public float mapOffsetX = 0f; // Used to determine where the algorithm will start from
	public float mapOffsetY = 0f; // Used to determine where the algorithm will start from
	public int wallsInGroup = 1;
	public float minWallDensityInLevel = 1f;
	public float maxWallDensityInLevel = 1f;
	public float minWallInLevel = 1f;
	public float maxWallInLevel = 1f;
	public int minSpawnsInLevel = 1;
	public int maxSpawnsInLevel = 1;
	public int minObstaclesInLevel =1;
	public int maxObstaclesInLevel =1;
	private float realSize = 0.6f;
	
	private GameObject[ , ] wallSet; // Storage for walls
	private Sprite[] flowerSprites;
	private Sprite[] treeSprites;
	private Sprite[] stoneSprites;
	
	//0 for empty
	//1 for lava pool
	//2 for items
	//3 for enemy spwan
	//4 for tunnel( can not put anything on it)
	//5 for obstical 
	private int[ , ] typeSet;
	
	string rrrr;
	
	// Use this for initialization
	void Start () {
		flowerSprites = Resources.LoadAll<Sprite> ("flowers");
		treeSprites = Resources.LoadAll<Sprite> ("smallTrees");
		stoneSprites = Resources.LoadAll<Sprite> ("stones");
		typeSet = new int[mapWidth, mapHeight];
		
		wallSet = new GameObject[mapWidth, mapHeight]; // This array now represents the entire  map of the level
		for(int i = 0; i < mapWidth; i++) {
			
			for(int j = 0; j < mapHeight; j++){
				wallSet[i, j] = null;
				typeSet[i, j] =0;
			}
		}
		
		minWallDensityInLevel = Mathf.Max(Mathf.Min (minWallDensityInLevel, 1),0);
		maxWallDensityInLevel = Mathf.Max(Mathf.Min (maxWallDensityInLevel, 1),0);
		
		
		CreateRoom (RoomStartX,RoomStartY);
		
		//		GenerateWallGroup (5, 5, 4);
		GenerateWalls ();  //Generates random walls and adds them into the wallSet array
		//GenerateMaps ();
		//GenerateSpawns (); // Generates random enemy spawns and adds them into the wallSet array
		//GenerateFlowers ();
		//GenerateTrees ();
		//GenerateStones ();
		//GenerateSword (); //Generates random positon items into wallSet array
		//GenerateBow ();
		//GenerateArmor ();
		for (int i = 0; i < mapWidth/3; i++) {
			for (int j = 0; j < mapHeight; j++) {
				rrrr += typeSet[i,j] + " ";
			}
			rrrr += "\n";
		}
		// Create an instance of StreamWriter to write text to a file.
		// The using statement also closes the StreamWriter.
		using (StreamWriter sw = new StreamWriter("TestFile.txt")) 
		{
			// Add some text to the file.
			sw.Write("This is the ");
			sw.WriteLine("header for the file.");
			sw.WriteLine("-------------------");
			// Arbitrary objects can also be written to the file.
			for (int j = 0; j < mapHeight; j++) {
				for (int i = 0; i < mapWidth/3; i++) {
					if (typeSet[i, j] == 1)
						sw.Write(1);
					else
						sw.Write("-");
				}
				sw.WriteLine("***");
			}
		}
		//TypeSetToWallSet ();
		GameObject.Find ("SpawnController").GetComponent<EnemySpawn> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//check is position(x,y) a path
	bool notPath(int x, int y){
		
		if (typeSet [x, y] == 4) {
			return false;
		} else
			return true;
	}
	
	// Tests if x,y is within the bounds of the map (array)
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
	
	// Returns the the wall at x,y if one exists and was 
	// created by this object. Otherwise returns null
	GameObject GetFlower (int x, int y){
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
			if (wallSet [x, y] == null&&typeSet[x, y]==0) {
				GameObject tempWall = Instantiate (wall) as GameObject;
				tempWall.transform.position = new Vector3 (x * wallDimensions - mapOffsetX, -y * wallDimensions + mapOffsetY, 0f);
				
				wallSet [x, y] = tempWall;
				
				typeSet[x, y] = 1;
			}
		}
	}
	
	// Places a flower object at x,y if one that was created 
	// by this object does not already exist
	//	void PutFlower (int x, int y) {
	//		if (WithinBounds (x, y)) {
	//			if (wallSet [x, y] == null) {
	//				GameObject tempFlower = Instantiate (flower) as GameObject;
	//				tempFlower.transform.position = new Vector3 (x * wallDimensions - mapOffsetX, y * wallDimensions - mapOffsetY, 0f);
	//				tempFlower.GetComponent<SpriteRenderer> ().sprite = flowerSprites [Random.Range (0, flowerSprites.Length)];
	//				wallSet [x, y] = tempFlower;
	//				typeSet[x, y] = 1;
	//			}
	//		}
	//	}
	
	// Places a tree at x,y if it is within bounds and null
	void PutTree (int x, int y) {
		if (WithinBounds (x, y)) {
			if (wallSet [x, y] == null) {
				GameObject tempTree = Instantiate (tree) as GameObject;
				tempTree.transform.position = new Vector3 (x * wallDimensions - mapOffsetX, y * wallDimensions - mapOffsetY, 0f);
				tempTree.GetComponent<SpriteRenderer> ().sprite = treeSprites [Random.Range (0, treeSprites.Length)];
				wallSet [x, y] = tempTree;
				typeSet[x, y] = 1;
			}
		}
	}
	
	// Places a stone at x,y if it is within bounds and null
	void PutStone (int x, int y) {
		if (WithinBounds (x, y)) {
			if (wallSet [x, y] == null) {
				GameObject tempStone = Instantiate (stone) as GameObject;
				tempStone.transform.position = new Vector3 (x * wallDimensions - mapOffsetX, y * wallDimensions - mapOffsetY, 0f);
				tempStone.GetComponent<SpriteRenderer> ().sprite = stoneSprites [Random.Range (0, stoneSprites.Length)];
				wallSet [x, y] = tempStone;
				typeSet[x, y] = 1;
			}
		}
	}
	
	// Places a rock object at x,y if one that was created 
	// by this object does not already exist	 
	void PutRocks (int x, int y) {
		if (WithinBounds (x, y)) {
			if (wallSet [x, y] == null&&typeSet[x, y]==0) {
				GameObject tempRock = Instantiate (rocks) as GameObject;
				tempRock.transform.position = new Vector3 (x * wallDimensions - mapOffsetX, y * wallDimensions - mapOffsetY, 0f);
				wallSet [x, y] = tempRock;
				typeSet[x, y] = 2;
			}
		}
	}
	
	// Places an obstacle object at x,y if one that was created 
	// by this object does not already exist	 
	void PutObstacles (int x, int y) {
		if (WithinBounds (x, y)) {
			if (wallSet [x, y] == null&&typeSet[x, y]==0) {
				GameObject tempObstacle = Instantiate (obstacle) as GameObject;
				tempObstacle.transform.position = new Vector3 (x * wallDimensions - mapOffsetX, y * wallDimensions - mapOffsetY, 0f);
				wallSet [x, y] = tempObstacle;
				typeSet[x, y] = 5;
			}
		}
	}
	
	// Places an enemy spawn point at x,y if it is within bounds and null
	void PutSpawn (int x, int y) {
		if (WithinBounds (x, y)&&typeSet[x, y]==0) {
			if (wallSet [x, y] == null) {
				GameObject tempSpawn = Instantiate (spawn) as GameObject;
				tempSpawn.tag = "Spawn";
				tempSpawn.transform.position = new Vector3 (x * wallDimensions - mapOffsetX, y * wallDimensions - mapOffsetY, 0f);
				wallSet [x, y] = tempSpawn;
				typeSet[x, y] = 3;
			}
		}
	}
	
	// place a item object at x,y if it in bounds and nothing here
	void PutItem (int x, int y, GameObject item) {
		if (WithinBounds (x, y)&&typeSet[x, y]==0) {
			if (wallSet [x, y] == null) {
				GameObject temp = Instantiate (item) as GameObject;
				temp.transform.position = new Vector3 (x * wallDimensions - mapOffsetX, y * wallDimensions - mapOffsetY, 0f);
				wallSet [x, y] = temp;
				typeSet[x, y] = 2;
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
			
			if (WithinBounds (x,y) && typeSet[x,y]!=4) {
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
	
	// Generates a squiggly line of flower objects starting at the position startX,startY
	// Generates a maximum of numWall number of flowers
	// If the line would go off the map, the function will return to the starting point 
	void GenerateFlowerGroup (int startX, int startY, int numWalls) {
		int x = startX; //the "pointer" x coord
		int y = startY; //the "pointer" y coord
		int direction = Random.Range (0, 4);
		Sprite tempSprite = flowerSprites [Random.Range (0, flowerSprites.Length)];
		
		for (int i = 0; i < numWalls/2; i++) {
			switch(direction) {	// Moves the "pointer" position to the next location
			case 0: //going right
				while(WithinBounds (x,y) && GetFlower(x, y) != null) {
					x++;
				}
				break;
			case 1: //going up
				while(WithinBounds (x,y) && GetFlower(x, y) != null) {
					y--;
				}
				break;
			case 2: //going left
				while(WithinBounds (x,y) && GetFlower(x, y) != null) {
					x--;
				}
				break;
			case 3: //going down
				while(WithinBounds (x,y) && GetFlower(x, y) != null) {
					y++;
				}
				break;
			default:
				break;
			}
			
			if (WithinBounds (x,y)) {
				if (wallSet [x, y] == null) {
					GameObject tempFlower = Instantiate (flower) as GameObject;
					tempFlower.transform.position = new Vector3 (x * wallDimensions - mapOffsetX, y * wallDimensions - mapOffsetY, 0f);
					tempFlower.GetComponent<SpriteRenderer> ().sprite = tempSprite;
					wallSet [x, y] = tempFlower;// Places a flower at the "pointer" position
				}
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
	
	//Generates several squiggly lines of walls
	void GenerateWalls () {
		for (int i=0; i<(Random.Range(minWallInLevel,maxWallInLevel))/**(mapWidth/3)*mapHeight)*/; i++) {
			GenerateWallGroup (Random.Range (0, mapWidth/3), Random.Range (0, mapHeight), Mathf.Max(Random.Range (1, wallsInGroup), Random.Range (1, wallsInGroup)));
		}
	}
	
	// Generates several squiggly lines of flowers
	void GenerateFlowers () {
		for (int i=0; i<Random.Range(minWallDensityInLevel*(mapWidth/3)*mapHeight,maxWallDensityInLevel*(mapWidth/3)*mapHeight); i++) {
			GenerateFlowerGroup (Random.Range (0, mapWidth/3), Random.Range (0, mapHeight), Random.Range (1, wallsInGroup));
		}
	}
	
	// Generates a random number of trees in the level
	void GenerateTrees () {
		int x;
		int y;
		int n = 0;
		for (int i=0; i<Random.Range(minWallDensityInLevel*(mapWidth/3)*mapHeight/2,maxWallDensityInLevel*(mapWidth/3)*mapHeight/2); i++) {
			do {
				x = Random.Range (0, mapWidth/3); 
				y = Random.Range (0, mapHeight); 
				n++;
			} while(!(WithinBounds (x,y)&& wallSet [x, y] == null) && (n<300));
			PutTree (x, y);
		}
	}
	
	// Generates a random number of rocks in the level
	void GenerateStones () {
		int x;
		int y;
		int n = 0;
		for (int i=0; i<Random.Range(minWallDensityInLevel*(mapWidth/3)*mapHeight/2,maxWallDensityInLevel*(mapWidth/3)*mapHeight/2); i++) {
			do {
				x = Random.Range (0, mapWidth/3); 
				y = Random.Range (0, mapHeight); 
				n++;
			} while(!(WithinBounds (x,y)&& wallSet [x, y] == null) && (n<300));
			PutStone (x, y);
		}
	}
	
	//set up bound and obstacles
	void GenerateMaps() {
		//put obstacles
		int x;
		int y;
		int n = 0;
		for (int i=1; i<Random.Range(minObstaclesInLevel,maxObstaclesInLevel); i++) {
			do {
				x = Random.Range (this.RoomStartX, mapWidth); 
				y = Random.Range (0, mapHeight); 
				n++;
			} while(!(WithinBounds (x,y)&& wallSet [x , y] == null&&notPath(x,y)) && (n<300));
			PutObstacles (x, y);
		}
	}
	
	void TypeSetToWallSet() {
		int [ , ] tempTypeSet = new int[mapWidth/3, mapHeight];
		for (int i = 0; i < mapWidth/3; i++) {
			for (int j = 0; j < mapHeight; j++) {
				tempTypeSet[i , j] = typeSet[i , j];
			}
		}
		//int aOOB=0;
		bool isAGroup = true;
		for (int groupSize = 3; groupSize > 0; groupSize--) {
			for (int i = 0; i < mapWidth/3; i++) {
				for (int j = 0; j < mapHeight; j++) {
					isAGroup = true;
					for (int m = 0; m <= groupSize; m++) {
						for (int n = 0; n <= groupSize; n++) {
							if (WithinBounds (i+m, j+n) && (i+m<mapWidth/3)) { // If i+m,j+n is in the map, and on the correct third of the map
								//if (n+m>1)
								//print("Found a partial group of "+(n+m)+" items! (out of "+(groupSize*groupSize)+" wanted items)");
								isAGroup = isAGroup && (tempTypeSet[i+m , j+n] == 1);
							}
							else {
								//aOOB++;
								isAGroup = false;
							}
						}
					}
					if (isAGroup) {
						//if (groupSize!=1)
						//print(groupSize);
						GameObject tempWall = Instantiate (wall) as GameObject;
						tempWall.transform.position = new Vector3 ((i) * wallDimensions - mapOffsetX, (j) * wallDimensions - mapOffsetY, 0f);
						if (groupSize>0)
							tempWall.transform.localScale += new Vector3((groupSize)*2.5f, (groupSize)*2.5f, 0);
						//print("in a group");
						for (int m = 0; m <= groupSize; m++) {
							for (int n = 0; n <= groupSize; n++) {
								//wallSet [i+m , j+n] = tempWall;
								tempTypeSet [i+m , j+n] = 0;
							}
						}
					}
				}
			}
		}
		for (int i = 0; i < mapWidth/3; i++) {
			for (int j = 0; j < mapHeight; j++) {
				if (tempTypeSet[i , j] == 1) {
					GameObject tempWall = Instantiate (wall) as GameObject;
					tempWall.transform.position = new Vector3 ((i * wallDimensions) - mapOffsetX, (j * wallDimensions) - mapOffsetY, 0f);
					wallSet [i , j] = tempWall;
				}
			}
		}
		//print ("# of indeces not in array: "+aOOB);
		//print ("Expected # of indeces not in array: "+(mapWidth * 12));
	}
	
	// give map a room like seperate spaces
	void CreateRoom(int StartX, int StartY) {
		
		int x = StartX;
		int y = StartY;
		
		int roomHeight = (mapHeight)/YroomNum;
		int roomWidth = (mapWidth-x) /XroomNum;
		
		//X direction path
		
		for (int i =y+roomHeight/2; i<=mapHeight; i+=roomHeight) {
			
			for(int j =x; j<mapWidth-1; j++)
			{
				typeSet[j,i]=4;
				typeSet[j,i+1]=4;
				typeSet[j,i-1]=4;
			}
		}
		
		//Y direction path
		
		for (int i =x+ roomWidth/2; i<=mapWidth; i+=roomWidth) {
			
			for (int j=y+2; j<mapHeight-1; j++)
			{
				typeSet[i,j]=4;
				typeSet[i+1,j]=4;
				typeSet[i-1,j]=4;
			}
		}
		
		//create room by rocks seperate
		
		//X direction rocks
		
		for (int i =y; i<=mapHeight-roomHeight; i+=roomHeight) {
			
			for(int j =x; j<mapWidth; j++)
			{
				if(typeSet[j,i]!=4){
					PutRocks(j,i);
				}
			}
			
		}
		
		//Y direction rocks
		
		for (int i = x+roomWidth; i<=mapWidth-roomWidth; i+=roomWidth) {
			
			for (int j=y; j<mapHeight; j++)
			{
				if(typeSet[i,j]!=4){
					PutRocks(i,j);
				}
			}
			
		}
	}
	
	// Generates a random number of enemy spawn points in the level
	void GenerateSpawns () {
		int x;
		int y;
		int n;
		for (int i=0; i<Random.Range(minSpawnsInLevel,maxSpawnsInLevel); i++) {
			n=0;
			do {
				
				x = Random.Range (0, mapWidth/3); 
				y = Random.Range (0, mapHeight); 
				n++;
				
			} while(!(WithinBounds (x,y)&& wallSet [x, y] == null) && (n<300));
			PutSpawn (x, y);
		}
		for (int i=0; i<Random.Range(minSpawnsInLevel,maxSpawnsInLevel); i++) {
			n = 0;
			do {
				
				x = Random.Range (RoomStartX, mapWidth); 
				y = Random.Range (0, mapHeight); 
				n++;
				
			} while(!(WithinBounds (x,y)&& wallSet [x, y] == null) && (n<300));
			PutSpawn (x, y);
		}
	}
	
	//Generates items by get random x,y within map bounds and check is there any other object
	void GenerateSword() {
		int x;
		int y;
		int n=0;
		do {
			x = Random.Range (0, mapWidth); 
			y = Random.Range (0, mapHeight); 
			n++;
		} while(!(WithinBounds (x,y)&& wallSet [x, y] == null) && (n<300));
		
		PutItem (x,y,Sword);
	}
	
	void GenerateBow() {
		int x;
		int y;
		int n = 0;
		do {
			x = Random.Range (0, mapWidth); 
			y = Random.Range (0, mapHeight); 
			n++;
		} while(!(WithinBounds (x,y)&& wallSet [x, y] == null) && (n<300));
		
		PutItem (x,y,Bow);
	}
	
	void GenerateArmor() {
		int x;
		int y;
		int n=0;
		do {
			x = Random.Range (0, mapWidth); 
			y = Random.Range (0, mapHeight); 
		} while(!(WithinBounds (x,y)&& wallSet [x, y] == null) && (n<300));
		
		PutItem (x,y,Armor);
	}
	
}

