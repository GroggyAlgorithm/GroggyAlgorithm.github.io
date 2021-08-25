using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; //Use unitys random
using UnityEngine.Tilemaps; //Tile maps
using System.Linq; //Library allows us to query collections




/**********************************************************************************************************************************************
 Class		public class DungeonManager : MonoBehaviour
 Abstract	Controls initialization of the Rooms and dungeon
**********************************************************************************************************************************************/
[RequireComponent(typeof(DungeonGenerator2D))]
public class DungeonManager : MonoBehaviour {

    //Variables

    [SerializeField]
    protected DungeonParameters2D dungeonParameters; //The parameters for the dungeon
    

    private DungeonGenerator2D dungeonGenerator; //Reference to the RandomWalk2D generator script

    [HideInInspector] 
    public static DungeonManager instance = null; //Used to make sure only one 1 instance of this script can be in the world
    
    private Transform DungeonManagerTransform; //The transform of the object attached to this script


    public GameObject Dungeon; //Parent for dungeon objects

    public Tilemap floorTileMap; //The tile map for floor tiles

    public Tilemap wallTileMap; //The tile map for floor tiles
    private TilemapCollider2D wallCollider;


    public Tilemap outerWallTileMap; //The tile map for floor tiles

    //Tiles
    [SerializeField]
    private TileBase[] floorTiles; //floor tiles
    [SerializeField]
    private TileBase[] wTiles; //wall tiles
    [SerializeField]
    private TileBase[] wTilesU; //wall tiles top
    [SerializeField]
    private TileBase[] wTilesR; //wall tiles right
    [SerializeField]
    private TileBase[] wTilesD; //wall tiles down
    [SerializeField]
    private TileBase[] wTilesL; //wall tiles left

    private List<Vector3> TilePositions = new List<Vector3>(); //List for possible tile locations
    private List<Vector2> RoomPositions = new List<Vector2>(); //List for possible tile locations

    private GameObject[] ClosedDoors; //Game object array for the closed doors
    private GameObject[] LockedDoors; //Game Objects array for the locked doors

    //To be removed 

    //Tiles as Game objects------------------------------------------------------
    private GameObject[] FloorTiles; //Array of floor tiless
    private GameObject[][] Doors; //Game Objects array for the doors
    private GameObject[] WallTiles; //Game Objects array for wall tiles
    private GameObject[] WallTLTiles;
    private GameObject[] WallTRTiles;
    private GameObject[] WallShadows;
    private GameObject[] EnvirementObjects; //Array of object prefabs in the envirement
    private GameObject[] Enemies; //Array of enemies 

    //Outer Wall
    private GameObject[] TOuterWallTiles;
    private GameObject[] LOuterWallTiles;
    private GameObject[] ROuterWallTiles;
    private GameObject[] TLOuterWallTiles;
    private GameObject[] TROuterWallTiles;
    private GameObject[] BLOuterWallTiles;
    private GameObject[] BROuterWallTiles;

    




    //Called before Start()
    private void Awake() {


        DungeonManagerTransform = this.gameObject.transform; //Set variable for this game objects transform
        dungeonGenerator = this.GetComponent<DungeonGenerator2D>(); //Set variable for the dungeon generator


    }



    // Start is called before the first frame update
    void Start() {
       // ClearTileMaps(); //Clear the tile maps on start
       // CreateAbstractDungeon(); //Create the dungeon

    }

    // Update is called once per frame
    void Update() {

    }



    /**********************************************************************************************************************************************
     Name		public void CreateAbstractDungeon()
     Abstract	Creates an abstract procedurally designed dungeon
    **********************************************************************************************************************************************/
    public void CreateAbstractDungeon() {

		//Commented for editor use only

		DungeonManagerTransform = this.gameObject.transform; //Set variable for this game objects transform
		dungeonGenerator = this.gameObject.GetComponent<DungeonGenerator2D>();

		HashSet<Vector2Int> floor_pos = new HashSet<Vector2Int>();
        HashSet<Vector2Int> wall_pos = new HashSet<Vector2Int>();
        dungeonGenerator.CorridorFirstTileGeneration2D(floorTileMap, floorTiles, wallTileMap, wTiles, floor_pos, wall_pos);
        dungeonGenerator.GenerateCardinalNeighbor(wall_pos, outerWallTileMap, wTilesU, wTilesR, wTilesD, wTilesL);
        dungeonGenerator.AddTileColliders(wallTileMap, wall_pos);
        int spawn_point = Random.Range(0, floor_pos.Count);
        int i = 0;
        var empty = dungeonGenerator.FindEmptyNeighborPositions(floor_pos);

        foreach (var position in floor_pos) {

            if (i == spawn_point) {
				if (empty.Contains(position)) {
                    spawn_point = Random.Range(i, floor_pos.Count);

                }
				else {
                    Debug.Log("Found spawn point at " + position);
				}

            }
            i++;

        }

    }


    

    //Returns the objects dungeon parameters scriptable object
    [SerializeField]
    public DungeonParameters2D GetParameters() {

        return dungeonParameters;


    }

    
    //sets the objects dungeon parameters scriptable object
    [SerializeField]
    public void SetScriptableObject(DungeonParameters2D paramAsset) {

        dungeonParameters = paramAsset;

	}



    /**********************************************************************************************************************************************
     Name		public void ClearTileMaps()
     Abstract	Clears the tile maps
    **********************************************************************************************************************************************/
    public void ClearTileMaps() {

        floorTileMap.ClearAllTiles();
        wallTileMap.ClearAllTiles();
        outerWallTileMap.ClearAllTiles();
        dungeonParameters.savedFloorTilePositions.Clear();

    }



    /**********************************************************************************************************************************************
     Name		public void ClearTileMaps()
     Abstract	Initializes, clears, and prepares list for the poistions
    **********************************************************************************************************************************************/
    HashSet<Vector2Int> List_init_(Vector2Int start_pos, int Height, int Width) {

        HashSet<Vector2Int> positions = new HashSet<Vector2Int>();

        //Loop through the x axis for the columns
        for (int x = 0; x < Width; x += 1) {

            //Loop through the y axis for the rows
            for (int y = 0; y < Height; y += 1) {

                //At each index, add a new Vector 3 to our list, as a 2d game, Z axis will stay at 0
                positions.Add(new Vector2Int(x, y)); //O
            }

        }

        return positions;
    }


   






}
