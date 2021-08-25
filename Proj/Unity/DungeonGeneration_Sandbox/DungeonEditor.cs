using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;


//Set cutomer editor
[CustomEditor(typeof(DungeonManager), true)]
public class DungeonEditor : Editor {

    //Variables
    public DungeonParameters2D dungeonParameters; //The parameters for the dungeon
    DungeonManager parent; //The parent this script will be attachted to

    bool showParams = true; //bool for the fold out
    bool showRandomWalkParams = true; //Create boolean for showing the info 
    bool showCooridorParams = true; //Create boolean for showing the info 
    bool showRoomParams = true; //Create boolean for showing the info 
    string fileName = "Asset Name";
    string notes = "Enter notes for the scriptable object";

    string parameterLabel = "Dungeon Parameters";
    string randomWalkParamLabel = "Random Walk Values";
    string cooridorParamLabel = "Cooridor Values";
    string roomParamLabel = "Room Values";


    bool showDungeonCreation = true;
    bool save_pressed = false; //If saved was pressed
    string dungeonCreationLabel = "Dungeon Creation";

    void Awake() {
        parent = (DungeonManager)target; // Set the target to the parent object
    }


    //Override the inspector gui
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        dungeonParameters = parent.GetParameters();

        GUILayout.Space(10);

        ShowingParameters(); //Shows the parameters
        ParamBoundsCheck(); //Checks the parameter bounds
        ShowingDungeonCreation(); //Shows the dungeon creation buttons

    }



    

 
// utility method
    static void HorizontalLine(Color color) {
        //Variables
        GUIStyle horizontalLine = new GUIStyle(); //Create return for the gui style
        horizontalLine.normal.background = EditorGUIUtility.whiteTexture; //Set background to the white texture
        horizontalLine.margin = new RectOffset(0, 0, 4, 4); //Set height offset
        horizontalLine.fixedHeight = 1; //Set the fixed height

        //Create new color variable
        var c = GUI.color;

        //Set the color to the color passed
        GUI.color = color;

        //Create a box with no content and a horizontal line style
        GUILayout.Box(GUIContent.none, horizontalLine);

        //Set to the color??
        GUI.color = c;

    }


    //Controls showing the dungeon creation
    void ShowingDungeonCreation() {

        showDungeonCreation = EditorGUILayout.Foldout(showDungeonCreation, dungeonCreationLabel, true);
        HorizontalLine(Color.black);
        GUILayout.Space(10);


        if (showDungeonCreation) {

            
            GUILayout.Space(10);


            //If the button is the create abstract....
            if (GUILayout.Button("Create Dungeon")) {
                parent.CreateAbstractDungeon();

            }
            GUILayout.Space(5);






            if (GUILayout.Button("Clear Tilemaps")) {
                parent.ClearTileMaps();
            }
            GUILayout.Space(5);
            //GUILayout.Add
            save_pressed = GUILayout.Button("Save scriptable object");
            GUILayout.Space(5);
            
            
            fileName = GUILayout.TextField(fileName);
            GUILayout.Space(5);
            notes = GUILayout.TextArea(notes, GUILayout.Height(50)); //Get entry from new text field for notes


            if (save_pressed == true) {
                try {
                    var instanceId = dungeonParameters.GetInstanceID(); //The instance id of the parameters
                    var path = AssetDatabase.GetAssetPath(instanceId);
                    var copyPath = ("Assets/ScriptableObjects/" + fileName);
                    var noteCopy = ("Notes \n-----------------------------------------\n\n" + notes);
                    
                    if (AssetDatabase.CopyAsset(path, (copyPath + ".asset"))) {
                        Debug.Log("Scriptable object Copied");
                        File.WriteAllText((copyPath + ".txt"), noteCopy + ".txt");
                    }
                    else {
                        Debug.LogError("Saving scriptable object failed");
                    }

                    fileName = "Asset Name";
                    notes = "Enter notes for the scriptable object";


                }
                catch {
                    Debug.LogError("Saving scriptable object failed");


                }
         
            }

            save_pressed = false;
        }



    }




    //Controls showing the dungeon parameters
	void ShowingParameters() {


        showParams = EditorGUILayout.Foldout(showParams, parameterLabel, true);
        HorizontalLine(Color.black);
        GUILayout.Space(10);


        if (showParams) {



            //Random walk values     
            showRandomWalkParams = EditorGUILayout.Foldout(showRandomWalkParams, randomWalkParamLabel, true);
            HorizontalLine(Color.black);

            if (showRandomWalkParams) {

                GUILayout.BeginHorizontal();
                GUILayout.Label("Iterations");
                dungeonParameters.Iterations = EditorGUILayout.IntField(dungeonParameters.Iterations, GUILayout.Width(75));
                GUILayout.EndHorizontal();
                GUILayout.Space(3);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Walk Length");
                dungeonParameters.WalkLength = EditorGUILayout.IntField(dungeonParameters.WalkLength, GUILayout.Width(75));
                GUILayout.EndHorizontal();
                GUILayout.Space(3);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Start At Random Iteration");
                dungeonParameters.StartAtRandomIteration = EditorGUILayout.Toggle(dungeonParameters.StartAtRandomIteration, GUILayout.Width(75));
                GUILayout.EndHorizontal();
                GUILayout.Space(3);
                GUILayout.Space(5);
            }


            showCooridorParams = EditorGUILayout.Foldout(showCooridorParams, cooridorParamLabel, true);
            HorizontalLine(Color.black);

            if (showCooridorParams) {

                GUILayout.Label("Cooridors");
                GUILayout.BeginHorizontal();
                GUILayout.Space(15);
                GUILayout.Label("Min");
                dungeonParameters.minCooridors = EditorGUILayout.IntField(dungeonParameters.minCooridors);
                GUILayout.Label("Max");
                dungeonParameters.maxCooridors = EditorGUILayout.IntField(dungeonParameters.maxCooridors);
                GUILayout.EndHorizontal();
                GUILayout.Space(3);

                GUILayout.Label("Length");
                GUILayout.BeginHorizontal();
                GUILayout.Space(15);
                GUILayout.Label("Min");
                dungeonParameters.minCooridorLength = EditorGUILayout.IntField(dungeonParameters.minCooridorLength);
                GUILayout.Label("Max");
                dungeonParameters.maxCooridorLength = EditorGUILayout.IntField(dungeonParameters.maxCooridorLength);
                GUILayout.EndHorizontal();
                GUILayout.Space(3);



                GUILayout.Label("Width");
                GUILayout.BeginHorizontal();
                GUILayout.Space(15);
                GUILayout.Label("Min");
                dungeonParameters.minCooridorWidth = EditorGUILayout.IntField(dungeonParameters.minCooridorWidth);
                GUILayout.Label("Max");
                dungeonParameters.maxCooridorWidth = EditorGUILayout.IntField(dungeonParameters.maxCooridorWidth);
                GUILayout.EndHorizontal();
                GUILayout.Space(3);


                GUILayout.Space(5);

            }



            showRoomParams = EditorGUILayout.Foldout(showRoomParams, roomParamLabel, true);
            HorizontalLine(Color.black);

            if (showRoomParams) {


                GUILayout.BeginHorizontal();
                GUILayout.Label("Room Percentage");
                GUILayout.Space(15);
                dungeonParameters.roomPercent = EditorGUILayout.Slider(dungeonParameters.roomPercent, 0.01f, 1.0f);
                GUILayout.EndHorizontal();
                GUILayout.Space(3);

                GUILayout.Label("Rooms");
                GUILayout.BeginHorizontal();
                GUILayout.Space(15);
                GUILayout.Label("Min");
                dungeonParameters.MinimumRooms = EditorGUILayout.IntField(dungeonParameters.MinimumRooms);
                GUILayout.Label("Max");
                dungeonParameters.MaximumRooms = EditorGUILayout.IntField(dungeonParameters.MaximumRooms);
                GUILayout.EndHorizontal();
                GUILayout.Space(3);

                GUILayout.Label("Columns");
                GUILayout.BeginHorizontal();
                GUILayout.Space(15);
                GUILayout.Label("Min");
                dungeonParameters.MinimumColumns = EditorGUILayout.IntField(dungeonParameters.MinimumColumns);
                GUILayout.Label("Max");
                dungeonParameters.MaximumColumns = EditorGUILayout.IntField(dungeonParameters.MaximumColumns);
                GUILayout.EndHorizontal();
                GUILayout.Space(3);



                GUILayout.Label("Rows");
                GUILayout.BeginHorizontal();
                GUILayout.Space(15);
                GUILayout.Label("Min");
                dungeonParameters.MinimumRows = EditorGUILayout.IntField(dungeonParameters.MinimumRows);
                GUILayout.Label("Max");
                dungeonParameters.MaximumRows = EditorGUILayout.IntField(dungeonParameters.MaximumRows);
                GUILayout.EndHorizontal();
                GUILayout.Space(3);
                GUILayout.Space(5);

            }

            HorizontalLine(Color.black);
            GUILayout.Space(10);

        }


        




    }



    //Checks the maximum and minimum values of the parameters for safety
    void ParamBoundsCheck() {

        if (dungeonParameters.Iterations <= 0) dungeonParameters.Iterations = 1;
        if (dungeonParameters.WalkLength <= 0) dungeonParameters.WalkLength = 1; //Length of each walk
        if (dungeonParameters.minCooridorLength <= 0) dungeonParameters.minCooridorLength = 1; //Minimum Length of a cooridor

        if (dungeonParameters.maxCooridorLength <= 0) dungeonParameters.maxCooridorLength = 1; //Maximum Length of a cooridor
        if (dungeonParameters.minCooridorWidth <= 0) dungeonParameters.minCooridorWidth = 1; ;
        if (dungeonParameters.maxCooridorWidth <= 0) dungeonParameters.maxCooridorWidth = 1;
        if (dungeonParameters.minCooridors <= 0) dungeonParameters.minCooridors = 1;
        if (dungeonParameters.maxCooridors <= 0) dungeonParameters.maxCooridors = 1;

        if (dungeonParameters.MinimumColumns <= 0) dungeonParameters.MinimumColumns = 1; //The minimum amount of columns allowed to be spawned
        if (dungeonParameters.MaximumColumns <= 0) dungeonParameters.MaximumColumns = 1; //The maximum amount of columns allowed to be spawned
        if (dungeonParameters.MinimumRows <= 0) dungeonParameters.MinimumRows = 1; //The minimum amount of Rows allowed to be spawned
        if (dungeonParameters.MaximumRows <= 0) dungeonParameters.MaximumRows = 1; //The maximum amount of Rows allowed to be spawned
        if (dungeonParameters.MinimumRooms <= 0) dungeonParameters.MinimumRooms = 1; //The minimum amount of Rooms allowed per dungeon
        if (dungeonParameters.MaximumRooms <= 0) dungeonParameters.MaximumRooms = 1; //The maximum amount of rooms allowed to be spawned


        if (dungeonParameters.maxCooridors < dungeonParameters.minCooridors) {
            dungeonParameters.maxCooridors = dungeonParameters.minCooridors;

        }

        if (dungeonParameters.maxCooridorLength < dungeonParameters.minCooridorLength) {
            dungeonParameters.maxCooridorLength = dungeonParameters.minCooridorLength;

        }

        if (dungeonParameters.maxCooridorWidth < dungeonParameters.minCooridorWidth) {
            dungeonParameters.maxCooridorWidth = dungeonParameters.minCooridorWidth;

        }
        if (dungeonParameters.MaximumRooms < dungeonParameters.MinimumRooms) {
            dungeonParameters.MaximumRooms = dungeonParameters.MinimumRooms;

        }

        if (dungeonParameters.MaximumColumns < dungeonParameters.MinimumColumns) {
            dungeonParameters.MaximumColumns = dungeonParameters.MinimumColumns;

        }

        if (dungeonParameters.MaximumRows < dungeonParameters.MinimumRows) {
            dungeonParameters.MaximumRows = dungeonParameters.MinimumRows;

        }


    }


  




}

