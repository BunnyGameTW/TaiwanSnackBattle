using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Help : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip button;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickBackButton()
    {
        audioSource.PlayOneShot(button);
        Invoke("LoadGameMenu", button.length);
    }
    public void LoadGameMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }
}
