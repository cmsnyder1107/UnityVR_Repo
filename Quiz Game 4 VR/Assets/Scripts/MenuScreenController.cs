using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //want to be able to load a different scene from here

public class MenuScreenController : MonoBehaviour, TimedInputHandler
{

	public void StartGame()
    {
        //SceneManager.LoadScene("Game"); //to be called from the StartButton
    }

    public void QuitGame() //to be called from the Exit button
    {
        Application.Quit(); //quits the application
    }

    public void HandleTimedInput()//required because inheriting from TimedInputHandler
    {
        SceneManager.LoadScene("Game"); //to be called from the StartButton
    }
}
