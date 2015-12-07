using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DungeonGenerate : MonoBehaviour {
	
	public class Room {
		public int x = 0;
		public int y = 0;
		public int w = 0;
		public int h = 0;
		public int roomNum = 0;
		public List<int> connectedRooms = new List<int> ();
		public int numConnected = 0;
		public Type roomType = Type.NONE;
	}
	
	public class Point {
		public int x = 0;
		public int y = 0;
	}
	
	public class Corridor {
		public Room start = null;
		public Room end = null;
	}
	
	public GameObject wall;
	public GameObject floor;
	public GameObject corridor;
	public GameObject boss;
	public GameObject spawn;
	public GameObject loot;
	
	//Where to start drawing the dungeon
	public int startX = 0;
	public int startY = 0;
	
	//Max size the dungeon can be
	public int maxWidth = 1;
	public int maxHeight = 1;
	
	//Min size the dungeon can be
	public int minWidth = 1;
	public int minHeight = 1;
	private int dungeonWidth;
	private int dungeonHeight;
	
	//How many rooms to generate; random number between min and max
	public int minRooms = 1;
	public int maxRooms = 1;
	private int numRooms;
	
	//Size of the rooms
	public int minRoomSize = 1;
	public int maxRoomSize = 1;
	
	//Number of times to move rooms to adjust the "closeness" of all rooms
	//Smaller number will cause rooms to be further from each other(on average)
	public int numSqueeze = 1;
	
	//Tile type for each space
	private enum Tile {EMPTY, FLOOR, WALL, CORRIDOR, ENTRANCE, BOSS, SPAWN, LOOT};
	//Type for each room
	public enum Type {NONE, ENTRANCE, BOSS, SPAWN, LOOT};
	
	Tile [,] map;
	Room [] rooms;
	GameObject [,] dungeon;
	
	// Use this for initialization
	void Start () {
		numRooms = Random.Range (minRooms, maxRooms);
		rooms = new Room[numRooms];
		dungeonWidth = Random.Range (minWidth, maxWidth);
		dungeonHeight = Random.Range (minHeight, maxHeight);
		dungeon = new GameObject[dungeonWidth, dungeonHeight];
		map = new Tile[dungeonWidth, dungeonHeight];
		for (int i = 0; i < numRooms; i++) {
			rooms[i] = null;
		}
		for (int i = 0; i < dungeonWidth; i++) {
			for (int j = 0; j < dungeonHeight; j++) {
				map[i,j] = Tile.EMPTY;
				dungeon[i,j] = null;
			}
		}
		
		GenerateRooms ();
		GenerateCorridors ();
		ConnectRooms ();
		FillRooms ();
		AddWalls ();
		AddEntrance ();
		AddBoss ();
		AddSpawns ();
		AddLoot ();
		DrawDungeon ();
		
		using (StreamWriter sw = new StreamWriter("MapTest.txt")) 
		{
			// Add some text to the file.
			sw.Write("This is the ");
			sw.WriteLine("header for the file.");
			sw.WriteLine("-------------------");
			// Arbitrary objects can also be written to the file.
			for (int j = 0; j < dungeonHeight; j++) {
				for (int i = 0; i < dungeonWidth; i++) {
					if (map[i,j] == Tile.WALL)
						sw.Write("|");
					if (map[i,j] == Tile.FLOOR)
						sw.Write(" ");
					if (map[i,j] == Tile.CORRIDOR)
						sw.Write(" ");
					if (map[i,j] == Tile.EMPTY)
						sw.Write ("X");
				}
				sw.WriteLine("***");
			}
		}
	}
	
	public void GenerateRooms() {
		int collideCount = 0;
		for (int i = 0; i < numRooms; i++) {
			Room r = new Room();
			
			r.w = Random.Range(minRoomSize, maxRoomSize);
			r.h = Random.Range(minRoomSize, maxRoomSize);
			r.x = Random.Range(1, dungeonWidth - r.w - 1);
			r.y = Random.Range(1, dungeonHeight - r.h - 1);
			
			if (Collides(r, 1000)) {
				collideCount++;
				i--;
				if(collideCount >= 500) {
					numRooms--;
					collideCount = 0;
				}
				continue;
			}
			r.w--;
			r.h--;
			rooms[i] = r;
			r.roomNum = i;
			collideCount = 0;
		}
		//Assign "Entrance" room
		Room e = rooms [0];
		for (int i = 1; i < numRooms; i++) {
			if (rooms[i].x + rooms[i].h - rooms[i].y < e.x + e.h - e.y)
				e = rooms[i];
		}
		e.roomType = Type.ENTRANCE;
		
		//Assign "Boss" room
		Room b = rooms [0];
		for (int i = 1; i < numRooms; i++) {
			if (rooms[i].y - rooms[i].x < b.y - b.x)
				b = rooms[i];
		}
		b.roomType = Type.BOSS;
	}
	
	public void GenerateCorridors() {
		for (int i = 0; i < numRooms; i++) {
			if (rooms [i] != null) {
				Room r = rooms [i];
				Room c = FindClosest (r);
				if (!(r.connectedRooms.Contains (c.roomNum) || c.connectedRooms.Contains (r.roomNum))) {
					r.connectedRooms.Add (c.roomNum);
					r.numConnected++;
					c.connectedRooms.Add (r.roomNum);
					c.numConnected++;
					
					Point rPoint = new Point ();
					Point cPoint = new Point ();
					
					rPoint.x = (int)Random.Range ((float)r.x, (float)r.x + (float)r.w);
					rPoint.y = (int)Random.Range ((float)r.y, (float)r.y + (float)r.h);
					cPoint.x = (int)Random.Range ((float)c.x, (float)c.x + (float)c.w);
					cPoint.y = (int)Random.Range ((float)c.y, (float)c.y + (float)c.h);
					
					while ((cPoint.x != rPoint.x) || (cPoint.y != rPoint.y)) {
						if (cPoint.x != rPoint.x) {
							if (cPoint.x > rPoint.x) 
								cPoint.x--;
							else
								cPoint.x++;
						} else if (cPoint.y != rPoint.y) {
							if (cPoint.y > rPoint.y)
								cPoint.y--;
							else
								cPoint.y++;
						}
						map [cPoint.x, cPoint.y] = Tile.CORRIDOR;
					}	
				}
			}
		}
		
		//Does a second pass over the dungeon, adding another corridor to any rooms that only have 1
		for (int i = 0; i < numRooms; i++) {
			if (rooms [i] != null && rooms[i].numConnected < 2) {
				Room r = rooms [i];
				Room c = FindClosest (r);
				if (!(r.connectedRooms.Contains (c.roomNum) || c.connectedRooms.Contains (r.roomNum))) {
					r.connectedRooms.Add (c.roomNum);
					r.numConnected++;
					c.connectedRooms.Add (r.roomNum);
					c.numConnected++;
					
					Point rPoint = new Point ();
					Point cPoint = new Point ();
					
					rPoint.x = (int)Random.Range ((float)r.x, (float)r.x + (float)r.w);
					rPoint.y = (int)Random.Range ((float)r.y, (float)r.y + (float)r.h);
					cPoint.x = (int)Random.Range ((float)c.x, (float)c.x + (float)c.w);
					cPoint.y = (int)Random.Range ((float)c.y, (float)c.y + (float)c.h);
					
					while ((cPoint.x != rPoint.x) || (cPoint.y != rPoint.y)) {
						if (cPoint.x != rPoint.x) {
							if (cPoint.x > rPoint.x) 
								cPoint.x--;
							else
								cPoint.x++;
						} else if (cPoint.y != rPoint.y) {
							if (cPoint.y > rPoint.y)
								cPoint.y--;
							else
								cPoint.y++;
						}
						map [cPoint.x, cPoint.y] = Tile.CORRIDOR;
					}	
				}
			}
		}
	}
	
	public void ConnectRooms() {
		Room r1 = rooms[0];
		Room r2 = rooms[0];
		for (int i = 0; i < numRooms; i++) {
			if (rooms[i].roomType == Type.ENTRANCE)
				r1 = rooms[i];
			if (rooms[i].roomType == Type.BOSS)
				r2 = rooms[i];
		}
		r1.connectedRooms.Add (r2.roomNum);
		r1.numConnected++;
		r2.connectedRooms.Add (r1.roomNum);
		r2.numConnected++;
		
		Point r1Point = new Point ();
		Point r2Point = new Point ();
		
		r1Point.x = (int)Random.Range ((float)r1.x, (float)r1.x + (float)r1.w);
		r1Point.y = (int)Random.Range ((float)r1.y, (float)r1.y + (float)r1.h);
		r2Point.x = (int)Random.Range ((float)r2.x, (float)r2.x + (float)r2.w);
		r2Point.y = (int)Random.Range ((float)r2.y, (float)r2.y + (float)r2.h);
		
		while ((r2Point.x != r1Point.x) || (r2Point.y != r1Point.y)) {
			if (r2Point.x != r1Point.x) {
				if (r2Point.x > r1Point.x) 
					r2Point.x--;
				else
					r2Point.x++;
			} else if (r2Point.y != r1Point.y) {
				if (r2Point.y > r1Point.y)
					r2Point.y--;
				else
					r2Point.y++;
			}
			map [r2Point.x, r2Point.y] = Tile.CORRIDOR;
		}
	}
	
	public void FillRooms() {
		for (int i = 0; i < numRooms; i++) {
			if (rooms[i] != null) {
				Room r = rooms [i];
				for (int x = r.x; x < r.x + r.w; x++) {
					for (int y = r.y; y < r.y + r.h; y++) {
						map[x,y] = Tile.FLOOR;
					}
				}
			}
		}
	}
	
	public void AddWalls() {
		for (int x = 0; x < dungeonWidth; x++) {
			for (int y = 0; y < dungeonHeight; y++) {
				if (map[x,y] == Tile.FLOOR || map[x,y] == Tile.CORRIDOR) {
					for (int x2 = x - 1; x2 <= x + 1; x2++) {
						for (int y2 = y - 1; y2 <= y + 1; y2++) {
							if (map[x2,y2] == Tile.EMPTY)
								map[x2,y2] = Tile.WALL;
						}
					}
				}
			}
		}
	}
	
	public Room FindClosest (Room r) {
		Point mid = new Point();
		mid.x = r.x + (r.w / 2);
		mid.y = r.y + (r.h / 2);
		
		Room closest = null;
		float closestDist = 1000f;
		for (int i = 0; i < numRooms; i++) {
			if (rooms[i] != null) {
				Room check = rooms[i];
				if (check == r)
					continue;
				
				
				Point checkMid = new Point();
				checkMid.x = check.x + (check.w / 2);
				checkMid.y = check.y + (check.h / 2);
				
				float dist = Mathf.Max(Mathf.Abs(mid.x - checkMid.x) - (r.w / 2) - (check.w / 2), Mathf.Abs(mid.y - checkMid.y) - (r.h / 2) - (check.h / 2));
				if (dist < closestDist && !Connected (r, check)) {
					closestDist = dist;
					closest = check;
				}
			}
		}
		return closest;
	}
	
	public bool Connected(Room r1, Room r2) {
		return (r1.connectedRooms.Contains (r2.roomNum) || r2.connectedRooms.Contains (r1.roomNum));
	}
	
	public bool Collides(Room r, int ignore) {
		for (int i = 0; i < numRooms; i++) {
			if (rooms[i] != null) {
				if (i == ignore)
					continue;
				Room check = rooms[i];
				if (!((r.x + r.w < check.x) || (r.x > check.x + check.w) || (r.y + r.h < check.y) || (r.y > check.y + check.h)))
					return true;
			}
		}
		return false;
	}
	
	public void SqueezeRooms() {
		for (int i = 0; i < numSqueeze; i++) {
			for (int j = 0; j < numRooms; j++) {
				if (rooms[j] != null) {
					Room r = rooms[j];
					while(true) {
						Point oldPos = new Point();
						oldPos.x = r.x;
						oldPos.y = r.y;
						
						if (r.x > 1)
							r.x--;
						if (r.y > 1)
							r.y--;
						if ((r.x == 1) && (r.y == 1))
							break;
						if (Collides (r, j)) {
							r.x = oldPos.x;
							r.y = oldPos.y;
							break;
						}
					}
				}
			}
		}
	}
	
	public void AddEntrance() {
		Room r = null;
		for (int i = 0; i < numRooms; i++) {
			if (rooms[i].roomType == Type.ENTRANCE)
				r = rooms[i];
		}
		int x = r.x - 1;
		int y = r.y + r.h / 2;
		bool entrancePlaced = false;
		while (!entrancePlaced && x >= 0) {
			if(map[x,y] == Tile.WALL && map[x,y+1] == Tile.WALL && map[x,y-1] == Tile.WALL && FindEntranceHelper(x,y) && FindEntranceHelper(x,y+1) && FindEntranceHelper(x,y-1)) {
				map[x,y] = Tile.ENTRANCE;
				map[x,y+1] = Tile.ENTRANCE;
				map[x,y-1] = Tile.ENTRANCE;
				entrancePlaced = true;
			} else {
				x--;
				if(map[x,y] == Tile.EMPTY && FindEntranceHelper(x,y)) {
					x++;
					if (map[x,y] == Tile.WALL && map[x,y+1] == Tile.WALL)
						y++;
					else if (map[x,y] == Tile.WALL && map[x,y-1] == Tile.WALL)
						y--;
					else if (map[x,y] == Tile.EMPTY && map[x,y+1] == Tile.WALL)
						y += 2;
					else
						y -= 2;
				}
			}
		} 
	}
	
	public bool FindEntranceHelper(int x, int y) {
		bool result = true;
		for (int i = 0; i < x; i++) {
			if (map[i,y] != Tile.EMPTY)
				result = false;
		}
		return result;
	}
	
	public void AddBoss() {
		Room r = rooms [0];
		for (int i = 1; i < numRooms; i++) {
			if (rooms[i].roomType == Type.BOSS)
				r = rooms[i];
		}
		map [r.x + r.w / 2, r.y + r.h / 2] = Tile.BOSS;
	}
	
	public void AddSpawns() {
		for (int i = 0; i < numRooms/3; i++) {
			int r = Random.Range(0, numRooms);
			if (rooms[r] != null) {
				if (rooms[r].roomType == Type.NONE) {
					rooms[r].roomType = Type.SPAWN;
					int x = (int)Random.Range ((float)rooms[r].x, (float)rooms[r].x + (float)rooms[r].w-2);
					int y = (int)Random.Range ((float)rooms[r].y, (float)rooms[r].y + (float)rooms[r].h-2);
					map[x+1,y+1] = Tile.SPAWN;
				}
				else
					i--;
			}
			else
				i--;
		}
	}
	
	public void AddLoot() {
		for (int i = 0; i < numRooms; i++) {
			if (rooms[i] != null) {
				if (rooms[i].roomType == Type.NONE) {
					rooms[i].roomType = Type.LOOT;
					int x = (int)Random.Range ((float)rooms[i].x, (float)rooms[i].x + (float)rooms[i].w-2);
					int y = (int)Random.Range ((float)rooms[i].y, (float)rooms[i].y + (float)rooms[i].h-2);
					map[x+1,y+1] = Tile.LOOT;
				}
			}
		}
	}
	
	public void DrawDungeon() {
		for (int x = 0; x < dungeonWidth; x++) {
			for (int y = 0; y < dungeonHeight; y++) {
				switch(map[x,y]) {
				case Tile.FLOOR:
					GameObject tempFloor = Instantiate (floor) as GameObject;
					tempFloor.transform.position = new Vector3 ((float)(x*2.5 + startX), (float)(y*2.5 + startY), 9f);
					dungeon[x,y] = tempFloor;
					break;
				case Tile.CORRIDOR:
					GameObject tempCorridor = Instantiate (corridor) as GameObject;
					tempCorridor.transform.position = new Vector3 ((float)(x*2.5 + startX), (float)(y*2.5 + startY), 0f);
					dungeon[x,y] = tempCorridor;
					break;
				case Tile.WALL:
					GameObject tempWall = Instantiate (wall) as GameObject;
					tempWall.transform.position = new Vector3 ((float)(x*2.5 + startX), (float)(y*2.5 + startY), 0f);
					dungeon[x,y] = tempWall;
					break;
				case Tile.ENTRANCE:
					GameObject tempFloor1 = Instantiate (floor) as GameObject;
					tempFloor1.transform.position = new Vector3 ((float)(x*2.5 + startX), (float)(y*2.5 + startY), 9f);
					dungeon[x,y] = tempFloor1;
					break;
				case Tile.BOSS:
					GameObject tempFloor2 = Instantiate (floor) as GameObject;
					tempFloor2.transform.position = new Vector3 ((float)(x*2.5 + startX), (float)(y*2.5 + startY), -1f);
					GameObject tempBoss = Instantiate (boss) as GameObject;
					tempBoss.transform.position = new Vector3 ((float)(x*2.5 + startX), (float)(y*2.5 + startY), 0f);
					dungeon[x,y] = tempBoss;
					break;
				case Tile.SPAWN:
					GameObject tempFloor3 = Instantiate (floor) as GameObject;
					tempFloor3.transform.position = new Vector3 ((float)(x*2.5 + startX), (float)(y*2.5 + startY), -1f);
					GameObject tempSpawn = Instantiate (spawn) as GameObject;
					tempSpawn.transform.position = new Vector3 ((float)(x*2.5 + startX), (float)(y*2.5 + startY), 0f);
					dungeon[x,y] = tempSpawn;
					break;
				case Tile.LOOT:
					GameObject tempFloor4 = Instantiate (floor) as GameObject;
					tempFloor4.transform.position = new Vector3 ((float)(x*2.5 + startX), (float)(y*2.5 + startY), -1f);
					GameObject tempLoot = Instantiate (loot) as GameObject;
					tempLoot.transform.position = new Vector3 ((float)(x*2.5 + startX), (float)(y*2.5 + startY), 0f);
					dungeon[x,y] = tempLoot;
					break;
				default:
					continue;
				}
			}
		}
	}
}
