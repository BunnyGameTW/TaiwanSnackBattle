using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour {
    public ItemData _itemdata;
    Collider playerCol;
    
	// Use this for initialization
	void Start () {
        Invoke("timer", _itemdata.existTime);
	}
	void timer()
    {
       
        GetComponentInParent<PlaneData>().hasItem = false;
        Destroy(this.gameObject);
    }
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        playerCol = other;
        if(other.gameObject.GetComponent<PlayerData>().canControl) effect();


    }
   
    //普通 加速
    void effect() {
        switch (_itemdata.type)
        {
            case 0:
              
                playerCol.gameObject.GetComponent<PlayerData>().score += _itemdata.score;               
                GameManager.game._uiMag.updateScore(playerCol.gameObject.GetComponent<PlayerData>());
                GetComponentInParent<PlaneData>().hasItem = false;
                playerCol.gameObject.GetComponents<AudioSource>()[0].PlayOneShot(AudioMag._audio.pick);
                Destroy(this.gameObject);             
                break;
            case 1:
                //set player speed up;
                playerCol.gameObject.GetComponent<PlayerData>().Speed += _itemdata.addSpeed;
                playerCol.gameObject.GetComponent<PlayerData>().score += _itemdata.score;
                GameManager.game._uiMag.updateScore(playerCol.gameObject.GetComponent<PlayerData>());
                playerCol.gameObject.GetComponents<AudioSource>()[0].PlayOneShot(AudioMag._audio.pick);

                playerCol.gameObject.GetComponent<Player>().StartCoroutine("speedTimer", _itemdata.effectTime);
                GetComponentInParent<PlaneData>().hasItem = false;
                Destroy(this.gameObject);
                break;
            case 2:

                //TODO:飛行玩家狀態
               // playerCol.gameObject.GetComponent<PlayerData>().playerState = PlayerData.PlayerState.Fly;
                break;
        }
    }
}
