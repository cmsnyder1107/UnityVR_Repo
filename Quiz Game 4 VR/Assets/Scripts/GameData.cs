using UnityEngine;
using System.Collections;

[System.Serializable]//needed if you want to serialize
public class GameData
{
    public RoundData[] allRoundData;//this is what will be serialized to the Json file, can add more if desired
}
