using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;//needed to load different scenes

public class MenuButton_RoundOverPanel : MonoBehaviour, TimedInputHandler {

    public GameObject scaryDisplay;//used to scare people
    public AudioClip screamSound;//used to scream and scare people
    private AudioSource soundSource;

    void Awake() //happens before Start()
    {
        soundSource = GetComponent<AudioSource>();//finds and attaches the object's AudioSource component if it exists
    }

    // Use this for initialization
    void Start () {
	
	}

    public void HandleTimedInput()//required because inheriting from TimedInputHandler
    {
        //SceneManager.LoadScene("MenuScreen");//returns us to the Menu Screen
        scaryDisplay.SetActive(true);//activate the scary display panel
        soundSource.PlayOneShot(screamSound);//plays the scream/scary sound
        //PauseScreen();
        //soundSource.Stop();
        //Application.Quit();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
