using UnityEngine;
using System.Collections;
using UnityEditor;//needed to access the Unity Editor
using System.IO;//needed to work with files

public class GameDataEditor : EditorWindow
{
    private string gameDataProjectFilePath = "/StreamingAssets/data.json";//where to look for and load the data
    public GameData gameData;//game object to hold the read in Json file

    //adds the Game Data Editor to the Window menu and calls this function when a user selects that option
    //from the Window Menu
    [MenuItem("Window/Game Data Editor")]
    static void Init()
    {
        GameDataEditor window = (GameDataEditor)EditorWindow.GetWindow(typeof(GameDataEditor));//creates a new
                    //...game data editor window using EditorWindow.GetWindow to get another instance of that
                    //...window. It returns another instance of window type T which is currently on the screen and
                    //...casts it to a GameDataEditor so we can store it in the 'window' variable
                    //***This will be viewable from the Window Menu in Unity...NOT IN THE GAME!***
        window.Show();//shows the window
    }

    void OnGUI()//like Update() for a MonoBehavior. Runs continuously when the window is in focus in the editor, or there's been a mouse click/movement
    {
        if(gameData != null) //does gameData exist/been loaded?
        {
            SerializedObject serializedObject = new SerializedObject(this); //'this' is the game data editor
            SerializedProperty serializedProperty = serializedObject.FindProperty("gameData"); //searches thru 'serialzedObject'
                                                                                               //and finds the property with the
                                                                                               //same name as we pass in ('gameData')
            EditorGUILayout.PropertyField(serializedProperty, true);//displays an editor for this property. 'true' tells it to display
                                                                    //the children of the property
            serializedObject.ApplyModifiedProperties();//in case the user has changed anything since the last time OnGUI() was run.
                                                       //Will update the serializedObject with those changes

            if (GUILayout.Button("Save Data")) //This draws a button with the label "Save data"
            {
                SaveGameData(); //if that button is pressed
            }
        }

        if (GUILayout.Button("Load Data")) //whether gameData is null or not, we can load our data
        {
            LoadGameData();
        }
    }

    private void LoadGameData()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;//get the TOTAL file path

        if (File.Exists(filePath))//checks to make sure the file exists
        {
            string dataAsJson = File.ReadAllText(filePath);//read in the file to a string
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);//load the string to a Game Data object
        }
        else
        {
            gameData = new GameData();//create new game data if the file doesn't exist
        }
    }

    private void SaveGameData()
    {
        string dataAsJson = JsonUtility.ToJson(gameData);//create Json data from the file
        string filePath = Application.dataPath + gameDataProjectFilePath;//sets the TOTAL file path
        File.WriteAllText(filePath, dataAsJson);//writes text to the Json file
    }
}
