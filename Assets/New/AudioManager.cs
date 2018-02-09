using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
   
    public bool BgmIsPlaying;
    public static AudioManager audios;
    GameObject Bgm;
    private void Awake()
    {
        audios = this;
     
        DontDestroyOnLoad(gameObject);
    }
 
    public AudioClip[] sounds;

    // Use this for initialization
    //player die, jump,  player almost die
    //eat item , hunger, change food,
    //time,
    void Start()
    {

        Bgm = new GameObject();
        Bgm.transform.parent = transform;
        Bgm.AddComponent(typeof(AudioSource));
        Bgm.name = "Bgm";
    }

    // Update is called once per frame
    void Update()
    {


     


    }
  public  void PlayBgm(int type)
    {
        
        if (BgmIsPlaying) {
            return;
        }

    
        Bgm.GetComponent<AudioSource>().clip = sounds[type];
        Bgm.GetComponent<AudioSource>().Play();
        BgmIsPlaying = true;
  
    }
    public void ChooseBgm(int type)
    {

        Bgm.GetComponent<AudioSource>().clip = sounds[type];
        Bgm.GetComponent<AudioSource>().Play();
        BgmIsPlaying = true;

    }

  
   public void PlaySE(int type)
    {
        GameObject go = new GameObject();
        go.transform.parent = transform;
        go.AddComponent(typeof(AudioSource));
        go.GetComponent<AudioSource>().clip = sounds[type];
        go.GetComponent<AudioSource>().Play();
        BgmIsPlaying = true;

        go.name = "SE";


    }

}
