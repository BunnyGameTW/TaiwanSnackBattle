using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour {
    public ItemData _itemdata;
    Collider playerCol;
    
	// Use this for initialization
	void Start () {
   //     _itemdata = GetComponent<ItemData>();
       // StartCoroutine(timer());
        Invoke("timer", _itemdata.existTime);
	}
	void timer()
    {
        // yield return new WaitForSeconds(_itemdata.existTime);
        Debug.Log(GetComponentInParent<PlaneData>());
        GetComponentInParent<PlaneData>().hasItem = false;
        Destroy(this.gameObject);
    }
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        playerCol = other;
        effect();


    }
   
    //普通 加速 升天
    void effect() {
        switch (_itemdata.type)
        {
            case 0:
                Debug.Log("eat item normal");
                playerCol.gameObject.GetComponent<PlayerData>().score += _itemdata.score;
                GetComponentInParent<PlaneData>().hasItem = false;
                Destroy(this.gameObject);             
                break;
            case 1:
                //set player speed up;
                playerCol.gameObject.GetComponent<PlayerData>().Speed += _itemdata.addSpeed;
                playerCol.gameObject.GetComponent<PlayerData>().score += _itemdata.score;               
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
