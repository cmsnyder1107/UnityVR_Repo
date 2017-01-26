using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic; //so we can use Generic lists



public class GameController : MonoBehaviour {

    public SimpleObjectPool answerButtonObjectPool; //will be set in the Inspector
    public Transform answerButtonParent; //references our Answer Button Panel to populate with answer buttons

    private DataController dataController;//a reference to the data controller
    private RoundData currentRoundData;
    private QuestionData[] questionPool; //questions we're using this round
    private bool isRoundActive; //has the timer runout, is the game going, have we run out of questions?
    private float timeRemaining; //time remaining in the round
    private float timeToWait;
    private int questionIndex; //what number question we're on
    private int playerScore;//to report the score at the end of the round
    public Text scoreDisplayText; //Text component to display the player's score
    public Text questionDisplayText; //a variable to display the question text
    public Text timeRemainingDisplayText;
    public Text highScoreDisplay;//to display the high score at the end of the game
    public Text yourScoreDisplay;
    public GameObject questionDisplay;//used to activate/deactivate the question panel
    public GameObject roundEndDisplay;//used to activate/deactivate the round over panel
    public GameObject scaryDisplay;//used to scare people

    //A List is good because we have a dynamic size of AnswerButtons for each question and we want to be able to resize...
    //...this list readily.
    private List<GameObject> answerButtonGameObjects = new List<GameObject>(); //stores all the AnswerButton game objects...
                                                                               //...that are currently in use and being displayed

    public AudioClip correctSound;//used for the correct sound effect
    public AudioClip wrongSound;//used for the incorrect sound effect
    public AudioClip screamSound;//used to scream and scare people
    private AudioSource soundSource;

    void Awake() //happens before Start()
    {
        soundSource = GetComponent<AudioSource>();//finds and attaches the object's AudioSource component if it exists
    }

	// Use this for initialization
	void Start ()
    {
        isRoundActive = true; //the round has now started
        dataController = FindObjectOfType<DataController>(); //finds and assigns a reference to the Data Controller
        currentRoundData = dataController.GetCurrentRoundData(); //get the data from the DataController, store in in our 
                                                                 //...currentRoundData
        questionPool = currentRoundData.questions; //get the question pool from the RoundData
        timeRemaining = currentRoundData.timeLimitInSeconds;
        timeToWait = 4;//wait 4 seconds on the scary screen
        UpdateTimeRemainingDisplay();//sets and displays the time in the UI

        playerScore = 0; //starts with a 0 score
        questionIndex = 0; //start at the list beginning
        roundEndDisplay.SetActive(false);//turns off the round over panel
        ShowQuestions(); //calls function to display the questions and answers
        
	}

    private void ShowQuestions()
    {
        roundEndDisplay.SetActive(false);//turns off the round over panel
        RemoveAnswerButtons(); //first thing is to remove all old AnswerButtons so we're ready display the new ones
        QuestionData questionData = questionPool[questionIndex]; //this will hold data about the current question we're
                                                                 //displaying answers for. This goes to our pool of
                                                                 //questions,see what question we're on, get that from
                                                                 //the questionPool and store it in questionData

        questionDisplayText.text = questionData.questionText; //This reaches into our pool of questions, gets a question, from
                                                              //the question gets the string that is the actual question,
                                                              //and displays it using our QuestionText UI element

        //Next we're going to loop over all of the answers and display those as well and add buttons as needed
        for (int i = 0; i < questionData.answers.Length; i++) //for however many answers this question has, we'll get
                                                              //buttons to display them
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();//Gets a spawned button that's
                                                                                   //not currently being used
            
            answerButtonGameObject.transform.SetParent(answerButtonParent); //sets the Answer Button Panel as the
                                                                            //parent, so all the buttons fall into the 
                                                                            //Vertical Layout Group
            answerButtonGameObjects.Add(answerButtonGameObject); //adds the button to the List of game objects
            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();//gets a reference to the
                                                                                            //answer button script
            answerButton.Setup(questionData.answers[i]);//calls the button's setup function and passes in the
                                                        //answer data and sets the text of the button to display
                                                        //the relevant answer text
            
        }
    }

    private void RemoveAnswerButtons()
    {
        while(answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]); //Whatever game object is element 0 in the...
                                                                             //List will be returned to the Object Pool,...
                                                                             //meaning it will be deactivated and made...
                                                                             //ready to be recycled and reused

            answerButtonGameObjects.RemoveAt(0); //removes the AnswerButton from the List of active AnswerButton game objects

            
        }
    }
	
    //
    public void AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect) //checks to see if the correct answer was clicked
        {
            soundSource.PlayOneShot(correctSound);//plays the 'Correct Sound' for a correct answer
            playerScore += currentRoundData.pointsAddedForCorrectAnswer;//adds points to the score
            scoreDisplayText.text = "Score: " + playerScore.ToString();//displays the new score, could also have a static
                                                                       //UI element that just says "Score" and another one
                                                                       //that takes and displays the score
        }
        else
        {
            soundSource.PlayOneShot(wrongSound);//plays the 'Wrong Sound' for a wrong answer
        }

        //checks to see if we've run out of questions
        if(questionPool.Length > questionIndex + 1)
        {
            questionIndex++;//updates the question index
            ShowQuestions();//shows a new question
        }
        else
        {
            //StartCoroutine(EndRound());
            EndRound();//ends the current round
        }
    }

    public void ReturnToMenu() //function for the button on the Round Over Panel
    {
        SceneManager.LoadScene("MenuScreen");//returns us to the Menu Screen
    }

    public void EndRound()
    {
        isRoundActive = false; //ends the round
        dataController.SubmitNewPlayerScore(playerScore);//sends to the dataController to see if it's a new high score
        highScoreDisplay.text = "High Score: " + dataController.GetHighestPlayerScore().ToString();//display the high score
        yourScoreDisplay.text = "Your Score: " + playerScore;

        questionDisplay.SetActive(false);//turns off the question display panel
        roundEndDisplay.SetActive(true);//turns on the round over panel
    }

    public void UpdateTimeRemainingDisplay()
    {
        timeRemainingDisplayText.text = "Time: " + Mathf.Round(timeRemaining).ToString();
    }

	// Update is called once per frame
	void Update ()
    {
        if (isRoundActive)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimeRemainingDisplay();//updates the UI element
        }
        else
        {
            //timeToWait -= Time.deltaTime;
        }

        //End the round when time runs out
        if(timeRemaining <= 0f)
        {
            //StartCoroutine(EndRound());
            EndRound();
        }

        if(timeToWait <= 0f)
        {
            Application.Quit();
        }
	}
}
