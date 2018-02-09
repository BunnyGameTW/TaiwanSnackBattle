using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CircleCloud : MonoBehaviour {
    
    const int cloudCount = 4;

    const float cloudLimit_x = -7.4f;
    //const float cloudMove_x = -1f;
    const float cloudMove_x_max = -0.25f;
    const float cloudMove_x_min = -0.5f;
    float[] cloudMove_x = new float[cloudCount];
    const float cloudRotate_x = 15.4f;

    const float menuCloudLimit_x = -120f;
    const float menuCloudMove_x = -1f;
    const float menuCloudRotate_x = 220f;

    const float cloudDelay = 0.15f;
    const float changeDelay = 2f;
    public GameObject[] cloud = new GameObject[cloudCount];

	// Use this for initialization
	void Start () {
        string _sceneName = SceneManager.GetActiveScene().name;
        if (_sceneName == "GameMenu")
        {
            InvokeRepeating("MoveMenuCloud", 0f, cloudDelay);
        }
        else if (_sceneName == "Sangel")
        {
            ChangeSpeed();
            InvokeRepeating("MoveCloud", 0f, cloudDelay);
            InvokeRepeating("ChangeSpeed", changeDelay, changeDelay);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // For GameMenu
    void MoveMenuCloud()
    {
        for (int i = 0; i < cloudCount; i++)
        {
           // Debug.Log("move cloud " + i.ToString());
            cloud[i].transform.position += new Vector3(menuCloudMove_x, 0, 0);
         //   Debug.Log(i + " " + cloud[i].transform.position.x);
        }

        CheckMenuCloudPosition();
    }
    void CheckMenuCloudPosition()
    {
        for (int i = 0; i < cloudCount; i++)
        {
            if (cloud[i].transform.position.x <= menuCloudLimit_x)
            {
                //Debug.Log(i + " " + cloud[i].transform.position.x);
                cloud[i].transform.position += new Vector3(menuCloudRotate_x, 0, 0);
                cloud[i].SetActive(false);
                Invoke("Activate" + i.ToString(), 12f);
            }
        }
    }
    void Activate0() { cloud[0].SetActive(true); }
    void Activate1() { cloud[1].SetActive(true); }
    void Activate2() { cloud[2].SetActive(true); }
    void Activate3() { cloud[3].SetActive(true); }

    // For MainScene
    void MoveCloud()
    {
        for(int i = 0; i < cloudCount; i ++)
        {
            cloud[i].transform.position += new Vector3(cloudMove_x[i], 0, 0);
            //Debug.Log("move cloud " + i.ToString());
        }
        
        CheckCloudPosition();
    }
    void ChangeSpeed()
    {
        for (int i = 0; i < cloudCount; i++)
        {
            cloudMove_x[i] = Random.Range(cloudMove_x_min, cloudMove_x_max);
        }
    }
    void CheckCloudPosition()
    {
        for (int i = 0; i < cloudCount; i++)
        {
            if(cloud[i].transform.position.x <= cloudLimit_x)
            {
                //Debug.Log(cloud[i].transform.position.x + " " + cloudLimit_x);
                cloud[i].transform.position += new Vector3(cloudRotate_x, 0, 0);
            }
        }
    }
}
