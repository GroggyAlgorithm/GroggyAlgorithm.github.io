using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //Library allows us to query collections
using Random = UnityEngine.Random; //Use unitys random

public static class RandomWalk2D  {




    //-----------------------------------------------------------------------------------
    // Name: RandomWalk
    // Abstract: Random walk algorithm
    // Params: Vector2Int start_position, int distance
    // Returns: HashSet<Vector2Int> path
    //-----------------------------------------------------------------------------------
    public static HashSet<Vector2Int> RandomWalk(Vector2Int start_position, int distance) {
        //Variables
        HashSet<Vector2Int> path = new HashSet<Vector2Int>(); //Create variable for the random walk path return
        Vector2Int previous_position; //The previous position on the grid.
        Vector2Int new_position; //The new position we're moving to

        

        //Add the starting position to the path and set the previous position to the current starting position
        path.Add(start_position);
        previous_position = start_position;


        //Loop through the distance
        for(int i = 0; i < distance; i += 1) {
            //Set the new position to the previous position and add a random position
            new_position = previous_position + Direction2D.GetRandomCardinalDirection(); 
            path.Add(new_position); //Add the new position
            previous_position = new_position; //Set the previous position to the newly created positio


        }

        //return the path
        return path;


    }



    //-----------------------------------------------------------------------------------
    // Name: RandomWalkCooridor
    // Abstract: Random walk algorithm for creating cooridors
    // Params: Vector2Int start_position, int cooridorLength
    // Returns: HashSet<Vector2Int> path
    //-----------------------------------------------------------------------------------
    public static List<Vector2Int> RandomWalkCooridor(Vector2Int start_position, int cooridorLength, int cooridorWidth) {
        //Variables
        List<Vector2Int> cooridor = new List<Vector2Int>(); //Create variable for the random walk list cooridor return
        Vector2Int new_position; //The new position we're moving to
        var direction = Direction2D.GetRandomCardinalDirection(); //Get a random direction


        //Add the starting position to the cooridor and set the previous position to the current starting position
        cooridor.Add(start_position);
        new_position = start_position;

        //Loop through the cooridorLength
        for (int i = 0; i < cooridorLength; i += 1) {
            
            new_position += direction; //add the direction to the position
            cooridor.Add(new_position); //Add the new position

            if(direction.x != 0) {
                var temp_position = new_position;
                var temp_direction = 0;
                //var multiplier = 1;
                for (int y = 0; y < cooridorWidth; y++) {
                    temp_direction += 1;
                    //temp_direction += Mathf.Abs(temp_direction) + 1;
                    //temp_direction *= multiplier;
                    cooridor.Add(new Vector2Int(temp_position.x, (temp_position.y + temp_direction)));
                    cooridor.Add(new Vector2Int(temp_position.x, (temp_position.y + (temp_direction * -1))));
                    //multiplier *= -1;
                }
			}
            else if (direction.y != 0) {
                var temp_position = new_position;
                var temp_direction = 0;
                //var multiplier = 1;
                for (int x = 0; x < cooridorWidth; x++) {
                    temp_direction += 1;
                    //temp_direction += Mathf.Abs(temp_direction) + 1;
                    //temp_direction *= multiplier;
                    cooridor.Add(new Vector2Int((temp_position.x + temp_direction),temp_position.y));
                    cooridor.Add(new Vector2Int((temp_position.x + (temp_direction*-1)),temp_position.y));
                   // multiplier *= -1;
                }
            }


        }

        //return the path
        return cooridor;


    }









    //Add info here 




    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight) {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while (roomsQueue.Count > 0) {
            var room = roomsQueue.Dequeue();
            if (room.size.y >= minHeight && room.size.x >= minWidth) {
                if (Random.value < 0.5f) {
                    if (room.size.y >= minHeight * 2) {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth * 2) {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight) {
                        roomsList.Add(room);
                    }
                }
                else {
                    if (room.size.x >= minWidth * 2) {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2) {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight) {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room) {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room) {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }










}







public static class Direction2D {

    //List of vector 2 directions for the 4 cardinal point directions
    public static List<Vector2Int> CardinalDirections = new List<Vector2Int>{
            new Vector2Int(0,1), //Up
            new Vector2Int(1,0), //Right
            new Vector2Int(0,-1), //Down
            new Vector2Int(-1,0), //Left
    };

    public static Vector2Int GetRandomCardinalDirection() {

        return CardinalDirections[Random.Range(0, CardinalDirections.Count)];

	}



    //Create a list of vector 2 directions for the 4 cardinal point directions
    public static List<Vector2Int> DiagonalDirections = new List<Vector2Int>{
            new Vector2Int(1,1), //Up-Right
            new Vector2Int(1,-1), //Down-Right
            new Vector2Int(-1,1), //Up-Left
            new Vector2Int(-1,-1), //Down-Left
    };

    public static Vector2Int GetRandomDiagonalDirection() {

        return DiagonalDirections[Random.Range(0, DiagonalDirections.Count)];

    }



    //Create a list of vector 2 directions for the 4 cardinal point directions
    public static List<Vector2Int> EightDirections = new List<Vector2Int>{
            new Vector2Int(0,1), //Up
            new Vector2Int(1,1), //Up-Right
            new Vector2Int(1,0), //Right
            new Vector2Int(1,-1), //Down-Right
            new Vector2Int(0,-1), //Down
            new Vector2Int(-1,-1), //Down-Left
            new Vector2Int(-1,0), //Left
            new Vector2Int(-1,1), //Up-Left
            
    };

    public static Vector2Int GetRandomEightDirection() {

        return EightDirections[Random.Range(0, EightDirections.Count)];

    }



}



public static class Direction3D {

    //Create a list of vector 3 directions for the 4 cardinal point directions
    public static List<Vector3Int> CardinalDirections = new List<Vector3Int>{
            new Vector3Int(0,1,0), //Up
            new Vector3Int(1,0,0), //Right
            new Vector3Int(0,-1,0), //Down
            new Vector3Int(-1,0,0), //Left
    };

    public static Vector3Int GetRandomCardinalDirection() {

        return CardinalDirections[Random.Range(0, CardinalDirections.Count)];

    }



    //Create a list of vector 3 directions for the 4 cardinal point directions
    public static List<Vector3Int> DiagonalDirections = new List<Vector3Int>{
            new Vector3Int(1,1,0), //Up-Right
            new Vector3Int(1,-1,0), //Down-Right
            new Vector3Int(-1,1,0), //Up-Left
            new Vector3Int(-1,-1,0), //Down-Left
    };

    public static Vector3Int GetRandomDiagonalDirection() {

        return DiagonalDirections[Random.Range(0, DiagonalDirections.Count)];

    }



    //Create a list of vector 3 directions for the 4 cardinal point directions
    public static List<Vector3Int> EightDirections = new List<Vector3Int>{
            new Vector3Int(0,1,0), //Up
            new Vector3Int(1,1,0), //Up-Right
            new Vector3Int(1,0,0), //Right
            new Vector3Int(1,-1,0), //Down-Right
            new Vector3Int(0,-1,0), //Down
            new Vector3Int(-1,-1,0), //Down-Left
            new Vector3Int(-1,0,0), //Left
            new Vector3Int(-1,1,0), //Up-Left
            
    };

    public static Vector3Int GetRandomEightDirection() {

        return EightDirections[Random.Range(0, EightDirections.Count)];

    }



}
