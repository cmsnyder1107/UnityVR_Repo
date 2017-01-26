using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //want to be able to load a different scene from here
using UnityEngine.UI; //to use and access UI components


public class TimedInputObject : MonoBehaviour, TimedInputHandler
{

    private AnswerData answerData;
    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController>();//gets and sets a reference to the game controller object
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void HandleTimedInput()//required because inheriting from TimedInputHandler
    {
        gameController.AnswerButtonClicked(answerData.isCorrect);//passes in if the answer is correct or not
    }
}
