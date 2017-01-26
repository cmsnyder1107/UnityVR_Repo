using UnityEngine;
using System.Collections;
using UnityEngine.UI; //to use and access UI components

public class AnswerButton : MonoBehaviour, TimedInputHandler
{

    public Text answerText; //stores a reference to the button text so we can use that to display the string for the answer
    private AnswerData answerData;
    private GameController gameController;

	// Use this for initialization
	void Start ()
    {
        gameController = FindObjectOfType<GameController>();//gets and sets a reference to the game controller object
	}
	
    public void Setup(AnswerData data)
    {
        answerData = data;
        answerText.text = answerData.answerText; //the string we setup in the AnswerData class will get passed to the button...
                                                 //...and dispalyed
    }

    public AnswerData GetAnswerData()
    {
        return answerData;
    }

    public void HandleClick() //what to do when the button is clicked
    {
        gameController.AnswerButtonClicked(answerData.isCorrect);//passes in if the answer is correct or not
    }

    public void HandleTimedInput()//required because inheriting from TimedInputHandler
    {
        gameController.AnswerButtonClicked(answerData.isCorrect);//passes in if the answer is correct or not
    }

    // Update is called once per frame
    void Update () {
	
	}
}
