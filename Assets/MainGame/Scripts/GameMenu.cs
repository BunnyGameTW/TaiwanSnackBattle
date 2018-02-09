using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip button;
    // Use this for initialization
    void Start () {

	}

	
	// Update is called once per frame
	void Update () {
		
	}


    public void ClickButtonStart()
    {
        audioSource.PlayOneShot(button);
        Invoke("LoadMainScene", button.length);
    }
    public void ClickButtonHelp()
    {
        audioSource.PlayOneShot(button);
        Invoke("LoadHelpScene", button.length);
    }
    public void ClickButtonMember()
    {

    }
    public void ClickButtonExit()
    {
        audioSource.PlayOneShot(button);
        Invoke("ExitGame", button.length);
    }

    
    void LoadMainScene()
    {

        SceneManager.LoadScene("Sangel");
    }
    void LoadHelpScene()
    {
        SceneManager.LoadScene("Help");
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
