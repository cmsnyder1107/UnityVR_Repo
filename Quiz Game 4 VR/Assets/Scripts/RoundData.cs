using UnityEngine;
using System.Collections;

[System.Serializable]
public class RoundData{

    public string name; //holds the round's name
    public int timeLimitInSeconds;
    public int pointsAddedForCorrectAnswer;
    public QuestionData[] questions; //a QuestionData class array to hold the round's questions


}
