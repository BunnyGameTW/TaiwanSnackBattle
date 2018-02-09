using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneEffect : MonoBehaviour {
    PlaneData _planeData;
	// Use this for initialization
	void Start () {
        _planeData = GetComponent<PlaneData>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            switch (_planeData.type)
            {
                case 0:                  
                    break;
                case 1://slow speed
                    other.GetComponent<PlayerData>().Speed += _planeData.slowSpeed;
                    if (other.GetComponent<PlayerData>().Speed < 0) other.GetComponent<PlayerData>().Speed = 2;//TODO:給最低限速
                    other.GetComponent<Player>().StartCoroutine("speedTimer", _planeData.effectTime);

                    break;
                case 2://spring
                    other.GetComponent<Rigidbody>().AddForce(new Vector3(0, _planeData.springForce * Time.deltaTime, 0), ForceMode.Impulse);
                    break;
                case 3://split
                    StartCoroutine("splitTimer", _planeData.effectTime);
                    break;

            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //if (other.tag == "Player")
        //{
        //    if (_planeData.type == 2)
        //    {
        //        other.GetComponent<Rigidbody>().AddForce(new Vector3(0, _planeData.springForce * Time.deltaTime, 0), ForceMode.Impulse);

        //    }
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if(_planeData.type == 3)
            {
                StopCoroutine("splitTimer");
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (_planeData.type == 2)
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, _planeData.springForce * Time.deltaTime, 0), ForceMode.Impulse);

            }
            if (_planeData.type == 3)
            {
                StartCoroutine("splitTimer", _planeData.effectTime);

            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //if (_planeData.type == 2)
            //{
            //    collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, _planeData.springForce * Time.deltaTime, 0), ForceMode.Impulse);

            //}
        }
    }
    IEnumerator splitTimer(float t)
    {

        yield return new WaitForSeconds(t);
        GetComponent<BoxCollider>().isTrigger = true;
        StartCoroutine("splitRecoverTimer", t);

    }
    IEnumerator splitRecoverTimer(float t)
    {
        yield return new WaitForSeconds(t);
        GetComponent<BoxCollider>().isTrigger = false;
    }
}
