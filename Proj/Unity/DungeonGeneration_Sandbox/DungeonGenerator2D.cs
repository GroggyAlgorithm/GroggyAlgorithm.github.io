using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //Library allows us to query collections
using Random = UnityEngine.Random; //Use unitys random
using UnityEngine.Tilemaps; //Tile maps



/**********************************************************************************************************************************************
 Class		public class DungeonGenerator2D : MonoBehaviour
 Abstract	Used for generating 2D Dungeons
**********************************************************************************************************************************************/
public class DungeonGenerator2D : MonoBehaviour {

    //Adding serialized field so we can set the protected value in the editor
    [SerializeField]
    protected Vector2Int start_position = Vector2Int.zero; //Starting position for generation


    public DungeonParameters2D dungeonParameters; //The parameters from the parameters scriptable object

    [HideInInspector] public int DoorsInRoom = 1; //The amount of doors in the room
    [HideInInspector] public int Rooms = 0; //The current amount of Rooms in the dungeon
    [HideInInspector] public int Columns = 0; //The current amount of Columns in the dungeon
    [HideInInspector] public int Rows = 0; //The current amount of rows in the dungeon

    //The amount of starting and exiting points on each level
    [HideInInspector] public int StartPoints = 1; //The amount of entrances per level
    [HideInInspector] public int ExitPoints = 1; //The amounts of exits per level




    /**********************************************************************************************************************************************
     Name		public void GenerateAsObjects(GameObject[] to_instantiate)
     Abstract	Generates the passed game object array. Currently unused
    **********************************************************************************************************************************************/
    public void GenerateAsObjects(GameObject[] to_instantiate) {

        //Create has sets for the position
        HashSet<Vector2Int> positions = StartRandomWalk();

        //For each position in the floor positions hash set...
        foreach (var position in positions) {
            Debug.Log(position); //Debug log the position to view
            Instantiate(to_instantiate[Random.Range(0, to_instantiate.Length)], new Vector3(position.x, position.y, 0f), Quaternion.identity);

        }
    }


    /**********************************************************************************************************************************************
     Name		public HashSet<Vector2Int> StartRandomWalk()
     Abstract	Begins using the random walk algorithm and returns the created hashset
    **********************************************************************************************************************************************/
    public HashSet<Vector2Int> StartRandomWalk() {

        var current_position = start_position; //create variable equal to the start position
        HashSet<Vector2Int> positions = new HashSet<Vector2Int>();

        //Loop through the amount of iterations we have
        for (int i = 0; i < dungeonParameters.Iterations; i++) {
            //Create variable = to the return from the random walk function
            var path = RandomWalk2D.RandomWalk(current_position, dungeonParameters.WalkLength);

            positions.UnionWith(path); //copy path to the hash set with union with

            //If the start at random iteration bool is true...
            if (dungeonParameters.StartAtRandomIteration) {
                //Select random position from the floor positions list
                current_position = positions.ElementAt(Random.Range(0, positions.Count));
            }
        }
        //return the floor positions
        return positions;
    }



    /**********************************************************************************************************************************************
     Name		public HashSet<Vector2Int> StartRandomWalk(Vector2Int startingPosition)
     Abstract	Begins using the random walk algorithm and returns the created hashset. Overload
    **********************************************************************************************************************************************/
    public HashSet<Vector2Int> StartRandomWalk(Vector2Int startingPosition) {

        var current_position = startingPosition; //create variable equal to the start position
        HashSet<Vector2Int> positions = new HashSet<Vector2Int>();

        //Loop through the amount of iterations we have
        for (int i = 0; i < dungeonParameters.Iterations; i++) {
            //Create variable = to the return from the random walk function
            var path = RandomWalk2D.RandomWalk(current_position, dungeonParameters.WalkLength);

            positions.UnionWith(path); //copy path to the hash set with union with

            //If the start at random iteration bool is true...
            if (dungeonParameters.StartAtRandomIteration) {
                //Select random position from the floor positions list
                current_position = positions.ElementAt(Random.Range(0, positions.Count));
            }
        }
        //return the floor positions
        return positions;
    }


    /**********************************************************************************************************************************************
     Name		public HashSet<Vector2Int> StartRandomWalk(Vector2Int startingPosition, int Distance)
     Abstract	Begins using the random walk algorithm and returns the created hashset. Overload
    **********************************************************************************************************************************************/
    public HashSet<Vector2Int> StartRandomWalk(Vector2Int startingPosition, int Distance) {

        var current_position = startingPosition; //create variable equal to the start position
        HashSet<Vector2Int> positions = new HashSet<Vector2Int>();

        //Loop through the amount of iterations we have
        for (int i = 0; i < dungeonParameters.Iterations; i++) {
            //Create variable = to the return from the random walk function
            var path = RandomWalk2D.RandomWalk(current_position, Distance);

            positions.UnionWith(path); //copy path to the hash set with union with

            //If the start at random iteration bool is true...
            if (dungeonParameters.StartAtRandomIteration) {
                //Select random position from the floor positions list
                current_position = positions.ElementAt(Random.Range(0, positions.Count));
            }
        }
        //return the floor positions
        return positions;
    }




    /**********************************************************************************************************************************************
     Name		public void GenerateWallTiles(IEnumerable<Vector2Int> wallPositions, Tilemap tileMap, TileBase[] wallTiles)
     Abstract	Create the wall tiles
    **********************************************************************************************************************************************/
    public void GenerateWallTiles(IEnumerable<Vector2Int> wallPositions, Tilemap tileMap, TileBase[] wallTiles) {
        
        //Loop through the poisitons
        foreach(var poisiton in wallPositions) {
            GenerateSingleTile(tileMap, wallTiles[Random.Range(0, wallTiles.Length)], poisiton); //Generate random wall tile on the tilemap at the position
		}
    }



    /**********************************************************************************************************************************************
     Name		public void GenerateWallTiles(IEnumerable<Vector2Int> wallPositions, Tilemap tileMap, TileBase[] wallTiles, bool addCollider)
     Abstract	Create the wall tiles. Overload method, if specified, can add collider
    **********************************************************************************************************************************************/
    public void GenerateWallTiles(IEnumerable<Vector2Int> wallPositions, Tilemap tileMap, TileBase[] wallTiles, bool addCollider) {

        //For each position, get a random wall tile and place on the tile map
        foreach (var poisiton in wallPositions) {
            var currentTile = wallTiles[Random.Range(0, wallTiles.Length)]; //Set to a random tile
            GenerateSingleTile(tileMap,currentTile, poisiton); //Generate random wall tile on the tilemap at the position

            //If add collider is true...
            if (addCollider == true) {
                //Get the position as a vector 3 int
                var v3 = new Vector3Int();
                v3.x = poisiton.x;
                v3.y = poisiton.y;
                v3.z = 0;
                //And set the collider type to the sprite colider type
                tileMap.SetColliderType(v3, Tile.ColliderType.Sprite);

            }


        }

    }





    /**********************************************************************************************************************************************
     Name		public void GenerateFloorTiles(IEnumerable<Vector2Int> floorPositions, Tilemap floorTileMap, TileBase floorTile) {
     Abstract	Generates the floor tiles. Currently unused
    **********************************************************************************************************************************************/
    public void GenerateFloorTiles(IEnumerable<Vector2Int> floorPositions, Tilemap floorTileMap, TileBase floorTile) {
        //GenerateTiles(floorTileMap, floorTile);
        return;
    }






    /**********************************************************************************************************************************************
     Name		public void GenerateTiles(Tilemap tileMap, TileBase[] tiles, HashSet<Vector2Int> positionsToPlace)
     Abstract	Paints the tiles to the passed tilemap
    **********************************************************************************************************************************************/
    public void GenerateTiles(Tilemap tileMap, TileBase[] tiles, HashSet<Vector2Int> positionsToPlace) {

        foreach (var position in positionsToPlace) {
            GenerateSingleTile(tileMap, tiles[Random.Range(0, tiles.Length)], position);
        }


    }


    /**********************************************************************************************************************************************
      Name		private void GenerateSingleTile(Tilemap tileMap, TileBase tile, Vector2Int position)
      Abstract	Generates a single tile on the passed tile map at the passed vector 2 position
    **********************************************************************************************************************************************/
    private void GenerateSingleTile(Tilemap tileMap, TileBase tile, Vector2Int position) {

        var tilePosition = tileMap.WorldToCell((Vector3Int)position);
        tileMap.SetTile(tilePosition, tile);

    }



    /**********************************************************************************************************************************************
       Name		    public void ClearTileMap(Tilemap tilemap)
       Abstract	    Clears the passed tile map
    **********************************************************************************************************************************************/
    public void ClearTileMap(Tilemap tilemap) {

        tilemap.ClearAllTiles();

    }



    /**********************************************************************************************************************************************
       Name		    public void GameObjectClear(GameObject to_destroy)
       Abstract	    Clears the passed games object. currently unused
    **********************************************************************************************************************************************/
    public void GameObjectClear(GameObject to_destroy) {

        //If the object has children...
        if (to_destroy.transform.GetChild(0) != null) {

            //Get the number of children

            //Begine removing the childre. Get child count and remove until we're at 0
            for (int i = to_destroy.transform.childCount; i >= 0; i--) {

                //Destroy that damn child. Destroy them dead. *poof* no more child bye bye
                Destroy(to_destroy.transform.GetChild(i));

            }
        }

        //Remove game object bye bye
        Destroy(to_destroy);

    }





    /**********************************************************************************************************************************************
       Name		    public HashSet<Vector2Int> GetPositionsInDirection2D(IEnumerable<Vector2Int> positionsToCheck, Vector2Int directionToCheck)
       Abstract	    For each var position in original positions, if it equals the position we're looking for, add to the return positions
    **********************************************************************************************************************************************/
    public HashSet<Vector2Int> GetPositionsInDirection2D(IEnumerable<Vector2Int> positionsToCheck, Vector2Int directionToCheck) {
        //Variables
        HashSet<Vector2Int> newPositions = new HashSet<Vector2Int>(); //The positions we'll return

        foreach (var position in positionsToCheck) {
            //If the position is in the direction we're looking for...
            if (position == directionToCheck) {
                newPositions.Add(position); //Add the position to the new positions
            }

        }
        //Return the new positions
        return newPositions;
    }






    //-----------------------------------------------------------------------------------
    // Name: GetCardinalWallPositions
    // Abstract: Gets wall positions in the 4 cardinal directions
    // Params: HashSet<Vector2Int> floor_positions- The positions of the floor tiles
    // Returns: HashSet<Vector2Int> of the wall positions
    //-----------------------------------------------------------------------------------
    public HashSet<Vector2Int> GetCardinalWallPositions(HashSet<Vector2Int> floor_positions) {

        HashSet<Vector2Int> wall_pos = new HashSet<Vector2Int>(); //The hash set of wall positions

        foreach (var position in floor_positions) {

            foreach (var direction in Direction2D.CardinalDirections) {

                var neighbour_pos = position + direction;
                if (floor_positions.Contains(neighbour_pos) == false) {
                    wall_pos.Add(neighbour_pos);

                }


            }


        }

        //return the positions
        return wall_pos;


    }




    //-----------------------------------------------------------------------------------
    // Name: GenerateCardinalNeighbor
    // Abstract: If there's no neighor in the direction, generate tile 
    // Params: The tilemap to generate the tile on, positions to check, and tiles in each direction
    // Returns: void
    //-----------------------------------------------------------------------------------
    public void GenerateCardinalNeighbor(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase[] tilesUp, TileBase[] tilesRight, TileBase[] tilesDown, TileBase[] tilesLeft) {

        foreach (var position in positions) {

            foreach (var direction in Direction2D.CardinalDirections) {

                var neighbour_pos = position + direction;
                if (positions.Contains(neighbour_pos) == false) {

                    //Up
                    if (direction == new Vector2Int(0, 1)) {
                        GenerateSingleTile(tileMap, tilesUp[Random.Range(0, tilesUp.Length)], position);
                    }
                    //Right
                    else if (direction == new Vector2Int(1, 0)) {
                        GenerateSingleTile(tileMap, tilesRight[Random.Range(0, tilesRight.Length)], position);
                    }
                    //Down
                    else if (direction == new Vector2Int(0, -1)) {
                        GenerateSingleTile(tileMap, tilesDown[Random.Range(0, tilesDown.Length)], position);
                    }
                    //Left
                    else if (direction == new Vector2Int(-1, 0)) {
                        GenerateSingleTile(tileMap, tilesLeft[Random.Range(0, tilesLeft.Length)], position);
                    }

                }

            }
        }

    }





    //-----------------------------------------------------------------------------------
    // Name: FindNeighborPositions
    // Abstract: Finds the neighboring positions if they exist
    // Params: 
    // Returns:
    //-----------------------------------------------------------------------------------
    public HashSet<Vector2Int> FindNeighborPositions(HashSet<Vector2Int> positions) {

        HashSet<Vector2Int> neighbors = new HashSet<Vector2Int>(); //The hash set of positions

        foreach (var position in positions) {

            foreach (var direction in Direction2D.CardinalDirections) {

                var neighbour_pos = position + direction;

                if (positions.Contains(neighbour_pos) == true) {
                    neighbors.Add(neighbour_pos);

                }


            }


        }

        //return the positions
        return neighbors;

    }






    //-----------------------------------------------------------------------------------
    // Name: FindEmptyNeighborPositions
    // Abstract: Finds the neighboring positions if none exist
    // Params: 
    // Returns:
    //-----------------------------------------------------------------------------------
    public HashSet<Vector2Int> FindEmptyNeighborPositions(HashSet<Vector2Int> positions) {

        HashSet<Vector2Int> new_neighbors = new HashSet<Vector2Int>(); //The hash set of new positions

        foreach (var position in positions) {

            foreach (var direction in Direction2D.CardinalDirections) {

                var neighbour_pos = position + direction;

                if (positions.Contains(neighbour_pos) == false) {
                    new_neighbors.Add(neighbour_pos);

                }


            }


        }

        //return the positions
        return new_neighbors;

    }



    //-----------------------------------------------------------------------------------
    // Name: FindEmptyCornerNeighbors
    // Abstract: Finds empty positions in corners using the diagonal directions
    // Params: 
    // Returns:
    //-----------------------------------------------------------------------------------
    public HashSet<Vector2Int> FindEmptyCornerNeighbors(HashSet<Vector2Int> positions) {

        HashSet<Vector2Int> corners = new HashSet<Vector2Int>(); //The hash set of positions

        foreach (var position in positions) {

            foreach (var direction in Direction2D.DiagonalDirections) {

                var neighbour_pos = position + direction;

                if (positions.Contains(neighbour_pos) == false) {
                    corners.Add(neighbour_pos);

                }


            }


        }

        //return the positions
        return corners;

    }



    //-----------------------------------------------------------------------------------
    // Name: FindCornerNeighbors
    // Abstract: Finds occupied positions in corners using the diagonal directions
    // Params: 
    // Returns:
    //-----------------------------------------------------------------------------------
    public HashSet<Vector2Int> FindCornerNeighbors(HashSet<Vector2Int> positions) {

        HashSet<Vector2Int> corners = new HashSet<Vector2Int>(); //The hash set of positions

        foreach (var position in positions) {

            foreach (var direction in Direction2D.DiagonalDirections) {

                var neighbour_pos = position + direction;

                if (positions.Contains(neighbour_pos) == true) {
                    corners.Add(neighbour_pos);

                }


            }


        }

        //return the positions
        return corners;

    }





    //Finds the dead ends in a map
    public List<Vector2Int> FindCardinalDeadEnds(HashSet<Vector2Int> positions) {
        //Variables
        List<Vector2Int> deadEnds = new List<Vector2Int>(); //The list of dead ends

        //For each position in the positions...
        foreach (var position in positions) {

            //Create neighborsCount
            int neighborsCount = 0;

            //For each direction in the Cardinal directions
            foreach (var direction in Direction2D.CardinalDirections) {

                //If the floor position has a neighbor...
                if (positions.Contains(position + direction))
                    neighborsCount++; //Add to the neighbor count

            }
            //If the neighbor count is only at 1...
            if (neighborsCount == 1)
                deadEnds.Add(position); //Add the positions to the dead ends
        }

        //Return the dead ends
        return deadEnds;
    }



    //-----------------------------------------------------------------------------------
    // Name: CorridorFirstTileGeneration2D
    // Abstract: Generates tiles based on the cooridor first design
    // Params: 
    // Returns: void
    //-----------------------------------------------------------------------------------
    public void CorridorFirstTileGeneration2D(Tilemap floorTileMap, TileBase[] floorTiles, Tilemap wallTileMap, TileBase[] wallTiles) {
        //Variables
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>(); //The positions of the floor tiles
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>(); //The positions of the floor tiles
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>(); //The positions of the rooms

        CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindCardinalDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);
        dungeonParameters.savedFloorTilePositions.UnionWith(floorPositions); //Save the positions
        GenerateTiles(floorTileMap, floorTiles, floorPositions);
        wallPositions = GetCardinalWallPositions(floorPositions);
        GenerateTiles(wallTileMap, wallTiles, wallPositions);

        

    }


    //-----------------------------------------------------------------------------------
    // Name: AddTileColliders
    // Abstract: Adds tilecoliders at the position passed
    // Params: 
    // Returns:
    //-----------------------------------------------------------------------------------
    public void AddTileColliders(Tilemap tilemap, HashSet<Vector2Int> positions) {

        TilemapCollider2D collider; //The collider for the tilemap

        //Add a tilemap and get a reference to it
        if (tilemap.gameObject.GetComponent<TilemapCollider2D>() == null) {
            tilemap.gameObject.AddComponent<TilemapCollider2D>();
        }

        collider = tilemap.gameObject.GetComponent<TilemapCollider2D>(); //Incase reference is needed

        foreach (var position in positions) {
            //Get the position as a vector 3 int
            var v3 = new Vector3Int();
            v3.x = position.x;
            v3.y = position.y;
            v3.z = 0;
            //And set the collider type to the sprite colider type
            tilemap.SetColliderType(v3, Tile.ColliderType.Sprite);
        }
    }


    //-----------------------------------------------------------------------------------
    // Name: RemoveTileColliders
    // Abstract: Removes tilecoliders at the position passed
    // Params: 
    // Returns:
    //-----------------------------------------------------------------------------------
    public void RemoveTileColliders(Tilemap tilemap, HashSet<Vector2Int> positions) {

        foreach (var position in positions) {
            //Get the position as a vector 3 int
            var v3 = new Vector3Int();
            v3.x = position.x;
            v3.y = position.y;
            v3.z = 0;
            tilemap.SetColliderType(v3, Tile.ColliderType.None);

        }
    }



    //-----------------------------------------------------------------------------------
    // Name: CorridorFirstTileGeneration2D
    // Abstract: Overides to add positions to be saved
    // Params: 
    // Returns: void
    //-----------------------------------------------------------------------------------
    public void CorridorFirstTileGeneration2D(Tilemap floorTileMap, TileBase[] floorTiles, Tilemap wallTileMap, TileBase[] wallTiles, HashSet<Vector2Int> floorPositions) {
        //Variables
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>(); //The positions of the floor tiles
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>(); //The positions of the rooms

        CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindCardinalDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);
        dungeonParameters.savedFloorTilePositions.UnionWith(floorPositions); //Save the positions
        GenerateTiles(floorTileMap, floorTiles, floorPositions);
        wallPositions = GetCardinalWallPositions(floorPositions);
        GenerateTiles(wallTileMap, wallTiles, wallPositions);



    }


    //-----------------------------------------------------------------------------------
    // Name: CorridorFirstTileGeneration2D
    // Abstract: Overides to add positions to be saved
    // Params: 
    // Returns: void
    //-----------------------------------------------------------------------------------
    public void CorridorFirstTileGeneration2D(Tilemap floorTileMap, TileBase[] floorTiles, Tilemap wallTileMap, TileBase[] wallTiles, HashSet<Vector2Int> floorPositions,
        HashSet<Vector2Int> wallPositions) {
        //Variables
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>(); //The positions of the rooms

        CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindCardinalDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);
        dungeonParameters.savedFloorTilePositions.UnionWith(floorPositions); //Save the positions
        GenerateTiles(floorTileMap, floorTiles, floorPositions);
        wallPositions = GetCardinalWallPositions(floorPositions);
        GenerateTiles(wallTileMap, wallTiles, wallPositions);



    }

    //-----------------------------------------------------------------------------------
    // Name: CorridorFirstTileGeneration2D
    // Abstract: Overides to add positions to be saved
    // Params: 
    // Returns: void
    //-----------------------------------------------------------------------------------
    public void CorridorFirstTileGeneration2D(Tilemap floorTileMap, TileBase[] floorTiles, Tilemap wallTileMap, TileBase[] wallTiles, HashSet<Vector2Int> floorPositions,
        HashSet<Vector2Int> wallPositions, HashSet<Vector2Int> potentialRoomPositions) {

        CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindCardinalDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);
        dungeonParameters.savedFloorTilePositions.UnionWith(floorPositions); //Save the positions
        GenerateTiles(floorTileMap, floorTiles, floorPositions);
        wallPositions = GetCardinalWallPositions(floorPositions);
        GenerateTiles(wallTileMap, wallTiles, wallPositions);



    }



    //-----------------------------------------------------------------------------------
    // Name: NonAbstractCooridorFirstTileGeneration2D
    // Abstract: 
    // Params: 
    // Returns: void
    //-----------------------------------------------------------------------------------
    //public void NonAbstractCooridorFirstTileGeneration2D(Tilemap floorTileMap, TileBase[] floorTiles, Tilemap wallTileMap, TileBase[] wallTiles) {
    //    //Variables
    //    HashSet<Vector2Int> finalfloorPositions = new HashSet<Vector2Int>(); //The positions of the floor tiles
    //    HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>(); //The positions of the floor tiles
    //    Rooms = Random.Range(dungeonParameters.MinimumRooms, dungeonParameters.MaximumRooms);

    //    for (int i = 0; i < Rooms; i++) {

    //        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>(); //The positions of the floor tiles
    //        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>(); //The positions of the rooms
    //        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
    //        List<int> DoorsPerRoom = new List<int>();
    //        List<Vector2Int> RoomEnds = new List<Vector2Int>();


    //        CreateCorridors(floorPositions, potentialRoomPositions);
    //        roomPositions = CreateRooms(potentialRoomPositions);

    //        var currentPosition = start_position;
    //        //potentialRoomPositions = RandomWalk2D.RandomWalk(start_position, 300);
    //        //roomPositions = CreateRooms(potentialRoomPositions);
    //        foreach (var position in floorPositions) {
    //            DoorsPerRoom.Add(Random.Range(1, 4));
    //            Columns = Random.Range(dungeonParameters.MinimumColumns, dungeonParameters.MaximumColumns);
    //            Rows = Random.Range(dungeonParameters.MinimumRows, dungeonParameters.MaximumRows);

    //            for (int x = 0; x < Columns; x++) {
    //                for (int y = 0; y < Rows; y++) {

    //                    finalfloorPositions.Add(new Vector2Int(position.x + x, position.y + y));

    //                }
    //            }
    //        }


    //    }




    //    // = CreateRooms(potentialRoomPositions);



    //    //CreateRoomsAtDeadEnd(deadEnds, roomPositions);



    //    //tilemapVisualizer.PaintFloorTiles(floorPositions);
    //    //WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    //    GenerateTiles(floorTileMap, floorTiles, finalfloorPositions);
    //    wallPositions = GetCardinalWallPositions(finalfloorPositions);
    //    GenerateTiles(wallTileMap, wallTiles, wallPositions);


    //    //randomWalk.GenerateCardinalNeighbor(wall_pos, outerWallTileMap, wTilesU, wTilesR, wTilesD, wTilesL);

    //}




    //-----------------------------------------------------------------------------------
    // Name: CreateRoomsAtDeadEnd
    // Abstract: Creates rooms at the dead ends
    // Params: The dead ends Vector2int list and the room floors hash set vector2int
    // Returns: void
    //-----------------------------------------------------------------------------------
    public void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors) {



        foreach (var position in deadEnds) {

            if (roomFloors.Contains(position) == false) {
                //var room = RandomWalk2D.RandomWalk(position, dungeonParameters.WalkLength);
                var room = StartRandomWalk(position,dungeonParameters.WalkLength);
                //var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }



    //-----------------------------------------------------------------------------------
    // Name: CheckSize
    // Abstract: Creates rooms at the dead ends
    // Params: The dead ends Vector2int list and the room floors hash set vector2int
    // Returns: void
    //-----------------------------------------------------------------------------------
    public void CheckSize(Vector2Int startingPosition, int minHeight, int minWidth, int maxHeight, int maxWidth) {
        HashSet<Vector2Int> posToAdd = new HashSet<Vector2Int>();
        int height = Random.Range(minHeight, maxHeight);
        int width = Random.Range(minWidth, maxWidth);


        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {

                posToAdd.Add(new Vector2Int(startingPosition.x + x, startingPosition.y + 1));

            }
        }
    }
    






    //-----------------------------------------------------------------------------------
    // Name: CreateRooms
    // Abstract: Creates rooms
    // Params: HashSet<Vector2Int> potentialRoomPositions
    // Returns: HashSet<Vector2Int>
    //-----------------------------------------------------------------------------------
    public HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions) {

        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * dungeonParameters.roomPercent);

        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomsToCreate) {
            //var roomFloor = RandomWalk2D.RandomWalk(roomPosition, dungeonParameters.WalkLength);
            var roomFloor = StartRandomWalk(roomPosition, dungeonParameters.WalkLength);
            roomPositions.UnionWith(roomFloor);
        }


        return roomPositions;
    }



    //-----------------------------------------------------------------------------------
    // Name: CreateCorridors
    // Abstract: Creates cooridors
    // Params: HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions
    // Returns: HashSet<Vector2Int>
    //-----------------------------------------------------------------------------------
    public void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions) {
        var currentPosition = start_position;
        potentialRoomPositions.Add(currentPosition);

        for (int i = 0; i < dungeonParameters.corridorCount; i++) {
            var corridor = RandomWalk2D.RandomWalkCooridor(currentPosition, Random.Range(dungeonParameters.minCooridorLength, dungeonParameters.maxCooridorLength), Mathf.RoundToInt(Random.Range(dungeonParameters.minCooridorWidth, dungeonParameters.maxCooridorWidth) / 2));
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
    }
}
