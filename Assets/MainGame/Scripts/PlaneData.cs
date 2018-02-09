using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneData : MonoBehaviour {
    public int type;
    public float effectTime;
    public bool hasItem;
    public float slowSpeed;
    public float springForce;
	// Use this for initialization
	void Awake () {

        init();       
    }
    public void init()
    {
        hasItem = false;
    }



}
