using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public static GameManager game;
    public int gameTime;
    public int changeItemTime;//還是被吃到就換下一個?
    public GameObject player1, player2;
    [HideInInspector]
    public GameObject[] planeRanPos;
   public ItemManager _itemMag;
    public PlaneManager _planeMag;
    public float mainFoodScore;
    public float notMainFoodScore;
    public float HungerScore;//need to set minus 一次扣多少血
    public float hungerTime;//幾秒扣一次寫
    public GameObject[] displayFoods;
    public Transform foodCamPos;
    // Use this for initialization
    void Awake () {
		if(game == null)
        {
            game = this;
        }

        planeRanPos = GameObject.FindGameObjectsWithTag("target");
        InvokeRepeating("changeFood", 0, changeItemTime);//TODO:吃到就換食物還是設固定時間換食物
        _planeMag.randomGenerate(_planeMag.initialNum);
        _itemMag.randomGenerate(_itemMag.initialNum);
        Invoke("checkWinner", gameTime);
    }
    void changeFood()
    {
       int i = Random.Range(0, _itemMag._items.Length);
       ItemData [] _items =  FindObjectsOfType<ItemData>();
        foreach (ItemData _item in _items)
        {
            if (_item.type == i) _item.score = mainFoodScore;
            else _item.score = notMainFoodScore;
        }
        Debug.Log("main type: " + i);
        Instantiate(displayFoods[i], foodCamPos);
        StartCoroutine(destroyDisplayFood());
    }
    IEnumerator destroyDisplayFood()
    {
        yield return new WaitForSeconds(changeItemTime);
        Destroy(foodCamPos.GetChild(0).gameObject);
    }
    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
    public void restart()
    {
        //TODO:
        
    }
   void checkWinner()
    {
        if(player1.GetComponent<PlayerData>().score > player2.GetComponent<PlayerData>().score) {
            gameOver(player2.GetComponent<PlayerData>().Type);
        }
        else
        {
            gameOver(player1.GetComponent<PlayerData>().Type);
        }
    }
    public void gameOver(PlayerData.PlayerType _pType)//傳入死亡或低分的人
    {
        if(_pType == PlayerData.PlayerType.Player1) {
            //TODO:change scene or  camera zoom in to PLAYER2
            Debug.Log("player2 winner");
        }
        else
        {
            //TODO:change scene or  camera zoom in to PLAYER1
            Debug.Log("player1 winner");

        }
    }
    
}
