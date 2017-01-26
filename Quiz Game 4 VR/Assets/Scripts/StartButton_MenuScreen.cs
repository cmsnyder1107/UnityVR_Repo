using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //want to be able to load a different scene from here

public class StartButton_MenuScreen : MonoBehaviour, TimedInputHandler {

	// Use this for initialization
	void Start () {
	
	}

    //this function is called after the user gazes at the button for a period of time
    public void HandleTimedInput()//required because inheriting from TimedInputHandler
    {
        SceneManager.LoadScene("Game"); //to be called from the StartButton
    }

    // Update is called once per frame
    void Update () {
	
	}
}
