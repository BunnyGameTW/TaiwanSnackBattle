using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public static GameManager game;
    public int gameTime;
    public int changeItemTime;//? 還是被吃到就換下一個
    public GameObject player1, player2;
    [HideInInspector]
    public GameObject[] planeRanPos;
   public ItemManager _itemMag;
    public PlaneManager _planeMag;
    public float mainFoodScore;
    public float notMainFoodScore;
    public float HungerScore;
    // Use this for initialization
    void Awake () {
		if(game == null)
        {
            game = this;
        }

        planeRanPos = GameObject.FindGameObjectsWithTag("target");
        InvokeRepeating("changeFood", 0, changeItemTime);//TODO:
        _planeMag.randomGenerate(_planeMag.initialNum);
        _itemMag.randomGenerate(_itemMag.initialNum);
        
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
    }
    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
    public void restart()
    {
        //TODO:
    }
   
    void gameOver()
    {

    }
    public void AddPlayerScore(GameObject _player,float _score) {
        //
        //_player.GetComponent<PlayerData>().score += _score;
        //update UI player score
        //UImanager.updateScore;
    }
}
