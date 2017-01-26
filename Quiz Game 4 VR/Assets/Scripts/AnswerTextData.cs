using UnityEngine;
using System.Collections;

public class AnswerTextData : MonoBehaviour, TimedInputHandler {

    private AnswerButton answerButton;
    private GameController gameController;
    private AnswerData answerData;

    // Use this for initialization
    void Start () {
        answerButton = GetComponent<AnswerButton>();//find the parent answer button
        gameController = FindObjectOfType<GameController>();//gets and sets a reference to the game controller object
        answerData = answerButton.GetAnswerData();
    }

    public void HandleTimedInput()//required because inheriting from TimedInputHandler
    {
        gameController.AnswerButtonClicked(answerData.isCorrect);//passes in if the answer is correct or not
    }

    // Update is called once per frame
    void Update () {
	
	}
}
