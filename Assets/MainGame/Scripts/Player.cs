using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody),typeof(PlayerData),typeof(BoxCollider))]
public class Player : MonoBehaviour
{
    Vector2 ve;
    PlayerData playerData;
    Rigidbody rb;
    public bool OntheGround;
 
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerData = GetComponent<PlayerData>();
        OntheGround = false;
    }

    // Update is called once per frame
    void Update()
    {
 //       Debug.Log("velocity:" + rb.velocity);
        Control();
        //
        //死亡判定
        //掉到底下不能控制N秒
        //衝刺
    }
    void Control() {
        if (playerData.canMove) { Move(); }
        if (playerData.canJump && OntheGround) { Jump(); }

        if (rb.velocity.y > 0) { playerData._status = PlayerData.PlayerStatus.JumpUp; }//TODO:看一下jump值
        else playerData._status = PlayerData.PlayerStatus.JumpDown;
        //if (rb.velocity.y == 0) playerData.canJump = true;
        //    Debug.Log(rb.velocity.y);
        //穿透
        if (playerData._status == PlayerData.PlayerStatus.JumpUp)
        {
            GetComponent<BoxCollider>().isTrigger = true;          
        }
        else if (playerData._status == PlayerData.PlayerStatus.JumpDown)
        {
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }
    void Move()
    {

        if (Input.GetKey(playerData.playKeyCode.Left))
        {
            ve = new Vector2(-1, 0);
        }
        else if (Input.GetKey(playerData.playKeyCode.Right))
        {
            ve = new Vector2(1, 0);
        }
        else
        {
            ve = new Vector2(0, 0);
        }

        if (!Input.GetKey(playerData.playKeyCode.Left) && !Input.GetKey(playerData.playKeyCode.Right))
        {
            ve = new Vector2(0, 0);
        }

        transform.Translate(new Vector2(ve.x, 0) * playerData.Speed * Time.deltaTime);

    }
    void Jump()
    {
        if (Input.GetKeyDown(playerData.playKeyCode.Jump))
        {
            rb.AddForce(new Vector3(0, playerData.JumpHeight, 0), ForceMode.Impulse);
            Debug.Log("force: " + playerData.JumpHeight);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.x - transform.position.x > 0)//物體在右
            {
                rb.AddForce(new Vector3(-30 * Time.deltaTime, 0), ForceMode.Impulse);
                collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(90 * Time.deltaTime, 0), ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(new Vector3(30 * Time.deltaTime, 0), ForceMode.Impulse);
                collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-90 * Time.deltaTime, 0), ForceMode.Impulse);

            }

        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag =="startPlane")
        {
            OntheGround = true;           
        }
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.x - transform.position.x > 0)//物體在右
            {
                rb.AddForce(new Vector3(-30 * Time.deltaTime, 0), ForceMode.Impulse);
                collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(30 * Time.deltaTime, 0), ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(new Vector3(30 * Time.deltaTime, 0), ForceMode.Impulse);
                collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(-30 * Time.deltaTime, 0), ForceMode.Impulse);
                
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "startPlane")
        {
            OntheGround = false;
           
        }
    }
    public void SetpSpeed()
    {
       //回復之前的速度
        playerData.Speed = playerData.pSpeed;
    }
    IEnumerator speedTimer(float t)
    {
        yield return new WaitForSeconds(t);
        SetpSpeed();
    }   
}
