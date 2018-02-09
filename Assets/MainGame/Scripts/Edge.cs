using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour {
    public Transform transport;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //
    private void OnTriggerEnter(Collider other)
    {
        Vector3 v = other.transform.position;
        v.x = transport.transform.position.x;
        other.transform.position = v;
        Debug.Log("trigger");
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("coll");
    }
}
