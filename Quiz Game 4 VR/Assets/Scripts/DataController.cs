using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //needed so we can load scenes
using System.IO; //needed to load data from an outside file

public class DataController : MonoBehaviour {

    public RoundData[] allRoundData; //enables us to have multiple rounds, an array of RoundData classes
                                    //set to private because we're loading the game data from a Json file,
                                    //not setting it in the Inspector
    private PlayerProgress playerProgress;
    private string gameDataFileName = "data.json";//file to load game data from

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject); //passes in the game object this script is attached to.
                                       //prevents this object from being destroyed when the new scene is loaded
        //LoadGameData();//loads game data from a Json file
        LoadPlayerProgress();//loads any saved player progress/data
        SceneManager.LoadScene("MenuScreen");
        //PlayerPrefs.DeleteKey("highestScore");//clear out the highest score value
        //PlayerPrefs.DeleteAll();
	}
	
    public RoundData GetCurrentRoundData()
    {
        return allRoundData[2]; //currently on have a single round
    }

    private void LoadPlayerProgress()
    {
        playerProgress = new PlayerProgress();

        //PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey("highestScore"))//looks to see if this Key exsists
        {
            playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");//loads the value if the Key exists
        }
    }

    private void SavePlayerProgress()
    {
        PlayerPrefs.SetInt("highestScore", playerProgress.highestScore);
    }

    //this function checks at the end of every round if the new score is higher than the current highest score
    public void SubmitNewPlayerScore(int newScore)
    {
        if (newScore > playerProgress.highestScore)
        {
            playerProgress.highestScore = newScore;
            SavePlayerProgress();
        }
    }

    //returns the highest score to be displayed at the end of the round
    public int GetHighestPlayerScore()
    {
        return playerProgress.highestScore;
    }

    private void LoadGameData() //load game data from a file instead of using the Inspector
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);//where to look for and load data

        if (File.Exists(filePath))//check to see if the file exists
        {
            string dataAsJson = File.ReadAllText(filePath);//store the data as a string
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);//Deserializes the string into a Game Data object
            allRoundData = loadedData.allRoundData;//make the loaded data available to the game
        }
        else
        {
            Debug.LogError("Cannot load game data!");//throw an error if there's no data there/no file
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
