using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DungeonParameters2D_", menuName = "ScriptableObjects/DungeonData")]
public class DungeonParameters2D : ScriptableObject {
    

    //Random walk values
    public int Iterations = 10;
    public int WalkLength = 10; //Length of each walk
    public bool StartAtRandomIteration = true; //Bool to start at a random iteration


    //Cooridors
    public int minCooridorLength = 15; //Minimum Length of a cooridor
    public int maxCooridorLength = 15; //Maximum Length of a cooridor
    public int minCooridorWidth = 4;
    public int maxCooridorWidth = 4;
    public int minCooridors = 4;
    public int maxCooridors = 4;


    [Range(0.01f, 1)]
    public float roomPercent = 0.8f;


    //Columns, Rows, and Rooms
    public int MinimumColumns = 6; //The minimum amount of columns allowed to be spawned
    public int MaximumColumns = 25; //The maximum amount of columns allowed to be spawned
    public int MinimumRows = 25; //The minimum amount of Rows allowed to be spawned
    public int MaximumRows = 50; //The maximum amount of Rows allowed to be spawned
    public int MinimumRooms = 5; //The minimum amount of Rooms allowed per dungeon
    public int MaximumRooms = 50; //The maximum amount of rooms allowed to be spawned

    public int corridorCount = 5; //Count of cooridors
    public int RoomCount = 5; //Count of cooridors
    public int cooridorLength = 0;
    public int cooridorWidth = 0;

    public HashSet<Vector2Int> savedFloorTilePositions = new HashSet<Vector2Int>(); //The saved hash set of tile positions after creating a dungeon

}
