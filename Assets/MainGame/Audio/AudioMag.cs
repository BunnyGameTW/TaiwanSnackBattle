using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMag : MonoBehaviour {
    public AudioClip win;
    public AudioClip pick;
    public AudioClip jump;
    public AudioClip changeFood;
    public static AudioMag _audio;
    public AudioClip startBGM;
    public AudioClip battleBGM;

    void Awake()
    {
        if (_audio == null) _audio = this;
    }
}
