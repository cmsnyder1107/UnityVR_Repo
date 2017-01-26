using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;//needed to load different scenes

public class ScaryButton_ScaryPanel : MonoBehaviour, TimedInputHandler {

	// Use this for initialization
	void Start () {
	
	}

    public void HandleTimedInput()//required because inheriting from TimedInputHandler
    {
        SceneManager.LoadScene("MenuScreen");//returns us to the Menu Screen
        //Application.Quit();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
