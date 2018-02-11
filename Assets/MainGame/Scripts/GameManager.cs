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
    public float MaxScore;//TODO:
    int nowGameTime;
    public uiManager _uiMag;
    // Use this for initialization
    //TODO: 透視相機不能跟隨  
    void Awake () {
		if(game == null)
        {
            game = this;
        }

        planeRanPos = GameObject.FindGameObjectsWithTag("target");
        InvokeRepeating("changeFood", 0, changeItemTime);
        _planeMag.randomGenerate(_planeMag.initialNum);
        _itemMag.randomGenerate(_itemMag.initialNum);
        Invoke("checkWinner", gameTime);
        InvokeRepeating("gameTimer", 0, 1);
        nowGameTime = gameTime;
        FindObjectOfType<AudioMagTest>().GetComponent<AudioSource>().clip = FindObjectOfType<AudioMag>().battleBGM;
        FindObjectOfType<AudioMagTest>().GetComponent<AudioSource>().Play();

    }
    void gameTimer()
    {
        _uiMag.updateTimeTxt(nowGameTime);
        nowGameTime--;
       
    }
    void changeFood()
    {

        int i = Random.Range(0, _itemMag._items.Length);//0 1 2 3
       ItemData [] itemsInScene =  FindObjectsOfType<ItemData>();//get all itemData and set their scores

        for (int num = 0; num < itemsInScene.Length; num++)
        {
            if (itemsInScene[num].index == i)
            {
                itemsInScene[num].score = mainFoodScore;
            }
            else
            {
                itemsInScene[num].score = notMainFoodScore;
            }
        }
        for (int j = 0; j < _itemMag._items.Length; j++)
        {
            if (j == i)
            {
                _itemMag._items[j].item.GetComponent<ItemData>().score = mainFoodScore;
            }
            else
            {
                _itemMag._items[j].item.GetComponent<ItemData>().score = notMainFoodScore;

            }
        }
        GetComponent<AudioSource>().PlayOneShot(FindObjectOfType<AudioMag>().changeFood); 
     //   Debug.Log("main item: " + i);
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
        //Debug.Log(GameObject.Find("Player1").GetComponent<PlayerData>().score + "score" + player1.GetComponent<PlayerData>().Type);
        //Debug.Log(player2.GetComponent<PlayerData>().score + "score" + player2.GetComponent<PlayerData>().Type);

        if (GameObject.Find("Player1").GetComponent<PlayerData>().score > GameObject.Find("Player2").GetComponent<PlayerData>().score) {
            gameOver(player2.GetComponent<PlayerData>().Type);
        }
        else
        {
            gameOver(player1.GetComponent<PlayerData>().Type);
        }
    }
    public void gameOver(PlayerData.PlayerType _pType)//傳入死亡或低分的人
    {
        Debug.Log("loser: " + _pType);
        CancelInvoke("changeFood");
        CancelInvoke("gameTimer");
        _itemMag.gameOver();
        foreach (ItemData item in FindObjectsOfType<ItemData>())
        {
            Destroy(item.gameObject);
        }
       
        FindObjectOfType<CameraControl>().Over(_pType);
        FindObjectsOfType<PlayerData>()[0]._status = PlayerData.PlayerStatus.Over;
        FindObjectsOfType<PlayerData>()[1]._status = PlayerData.PlayerStatus.Over;
    }
    
}
