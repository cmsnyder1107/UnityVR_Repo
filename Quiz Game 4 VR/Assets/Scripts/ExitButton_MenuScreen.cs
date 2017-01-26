using UnityEngine;
using System.Collections;

public class ExitButton_MenuScreen : MonoBehaviour, TimedInputHandler {

	// Use this for initialization
	void Start () {
	
	}

    //this function is called after the user gazes at the button for a period of time
    public void HandleTimedInput()//required because inheriting from TimedInputHandler
    {
        Application.Quit(); //quits the application
    }

    // Update is called once per frame
    void Update () {
	
	}
}
