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
        if(other.tag == "Player" && other.GetComponent<PlayerData>().canControl)
        {
            switch (_planeData.type)
            {
                case 0:                  
                    break;
                case 1://slow speed
                    other.GetComponent<PlayerData>().Speed += _planeData.slowSpeed;
                    Debug.Log("123456");
                    if (other.GetComponent<PlayerData>().Speed < 0) other.GetComponent<PlayerData>().Speed = 2;//TODO:給最低限速
                    other.GetComponent<Player>().StartCoroutine("speedTimer", _planeData.effectTime);

                    break;
                case 2://spring
                    other.GetComponent<Rigidbody>().AddForce(new Vector3(0, _planeData.springForce * Time.deltaTime, 0), ForceMode.Impulse);
                    other.gameObject.GetComponents<AudioSource>()[1].PlayOneShot(AudioMag._audio.jump);

                    break;
                case 3://split
                    StartCoroutine("splitTimer", _planeData.effectTime);
                    break;

            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
      
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<PlayerData>().canControl)
        {
            if(_planeData.type == 3)
            {
                StopCoroutine("splitTimer");
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerData>().canControl)
        {
            if (_planeData.type == 2)
            {
                collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, _planeData.springForce * Time.deltaTime, 0), ForceMode.Impulse);
                collision.gameObject.GetComponents<AudioSource>()[1].PlayOneShot(AudioMag._audio.jump);

            }
            if (_planeData.type == 3)
            {
                StartCoroutine("splitTimer", _planeData.effectTime);

            }
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
