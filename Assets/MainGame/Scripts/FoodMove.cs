using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FoodMove : MonoBehaviour
{

    public float Smooth = 0.1f;
     Vector3[] playerUI = new Vector3[2];
   public bool startMove;
    public PlayerData.PlayerType Playertype;
    [Range(0, 10)]
    public float Player1X=1;
    [Range(0, 10)]
    public float Player1Y=9;
    [Range(0, 10)]
    public float Player2X=9;
    [Range(0, 10)]
    public float Player2Y=9;
    // Use this for initialization
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        playerUI[0] = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width /10 * Player1X, Screen.height/10* Player1Y, 2));
        playerUI[1] = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 10 * Player2X, Screen.height / 10 * Player2Y, 2));
        //Debug.Log(playerUI[0]);
        if (!startMove) {
            return;
        }
        MoveToPlayerUI();

    }

    public void MoveToPlayerUI()
    {


        GetComponent<Food>().enabled = false;
       
        if (Playertype == PlayerData.PlayerType.Player1)
        {
            if (Vector3.Distance(transform.localPosition, playerUI[0]) <= 0.5f)
            {
                GetComponent<ItemEffect>().AddScore();

                Destroy(gameObject);
                 return ;
            }
            transform.SetParent(transform.root.parent);
         transform.localPosition = Vector3.Lerp(transform.localPosition, playerUI[0], Smooth * Time.deltaTime);
            GetComponent<SphereCollider>().enabled = false;
        }
        else if (Playertype == PlayerData.PlayerType.Player2)
        {

            if (Vector3.Distance(transform.localPosition, playerUI[1]) <= 0.5f)
            {
                GetComponent<ItemEffect>().AddScore();
                Destroy(gameObject);
                 return ;
            }
            transform.SetParent(transform.root.parent);
            transform.localPosition = Vector3.Lerp(transform.localPosition, playerUI[1], Smooth * Time.deltaTime);
            GetComponent<SphereCollider>().enabled = false;

        }
       

    }
}






