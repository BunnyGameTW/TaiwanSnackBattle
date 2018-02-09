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
        InvokeRepeating("goDie", GameManager.game.hungerTime, GameManager.game.hungerTime);
       // Debug.Log(playerData.score +" , "+ playerData.Type);
        GameManager.game._uiMag.updateScore(playerData);

    }

    // Update is called once per frame
    void Update()
    {

        if (playerData._status == PlayerData.PlayerStatus.FakeDie || playerData._status == PlayerData.PlayerStatus.Die || playerData._status == PlayerData.PlayerStatus.Over) playerData.canControl = false;
        else playerData.canControl = true;      
       if (playerData.canControl) Control();
       if( playerData._status == PlayerData.PlayerStatus.Idle || playerData._status == PlayerData.PlayerStatus.JumpDown || playerData._status == PlayerData.PlayerStatus.FakeDie || playerData._status == PlayerData.PlayerStatus.JumpUp) checkDie();
        else CancelInvoke("goDie");
        //TODO:衝刺
    }
        void checkDie() {
            if(playerData.score <= 0) {                 
                playerData._status = PlayerData.PlayerStatus.Die;
                GameManager.game.gameOver(playerData.Type);
        }
            else if(playerData.score >= GameManager.game.MaxScore)
        {
            if(playerData.Type == PlayerData.PlayerType.Player1)
            {
                //set player2 die
                GameManager.game.player2.GetComponent<PlayerData>()._status = PlayerData.PlayerStatus.Die;
                GameManager.game.gameOver(GameManager.game.player2.GetComponent<PlayerData>().Type);
            }
            else 
            {
                //set player1 die
                GameManager.game.player1.GetComponent<PlayerData>()._status = PlayerData.PlayerStatus.Die;
                GameManager.game.gameOver(GameManager.game.player1.GetComponent<PlayerData>().Type);
            }
        }
    }
    void goDie()
    {
        playerData.score += GameManager.game.HungerScore;
     //  Debug.Log("player "+playerData.Type+": " + playerData.score);
        GameManager.game._uiMag.updateScore(playerData);
    }
    void Control() {
        if (playerData.canMove) { Move(); }
        if (playerData.canJump && OntheGround) { Jump(); }

        if (rb.velocity.y > 0) { playerData._status = PlayerData.PlayerStatus.JumpUp; }
        else playerData._status = PlayerData.PlayerStatus.JumpDown;
        //if (rb.velocity.y == 0) playerData.canJump = true;
        
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
            setRot(-90.0f);
        }
        else if (Input.GetKey(playerData.playKeyCode.Right))
        {
            ve = new Vector2(1, 0);
            setRot(90.0f);
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
            setRot(180);
          GetComponents<AudioSource>()[1].PlayOneShot(AudioMag._audio.jump);

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" )//相撞彈開
        {
            Camera.main.GetComponent<CameraShake>().shakeDuration = 0.1f;
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
        if (collision.gameObject.tag == "Player" )
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
    public void SetFakeDie() //假死亡
    {
        if (playerData._status != PlayerData.PlayerStatus.Over)
        {
            playerData._status = PlayerData.PlayerStatus.FakeDie;
            StartCoroutine(FakeDieTimer());//觸發的時候贏了
        }
        else
        {
            //贏的時候又死亡
            
        }

    }
    IEnumerator FakeDieTimer()
    {
        rb.AddForce(new Vector3(0, 4, 0), ForceMode.Impulse);
        Vector3 dieRot = new Vector3(0, 0, 180);
        transform.eulerAngles = dieRot;
        GetComponent<BoxCollider>().isTrigger = true;
        yield return new WaitForSeconds(playerData.FakeDieCD);
        if(playerData._status != PlayerData.PlayerStatus.Over)  playerData._status = PlayerData.PlayerStatus.Idle;
        GetComponent<BoxCollider>().isTrigger = false;
        Vector3 normalRot = new Vector3(0, 0, 0);
        transform.eulerAngles = normalRot;
        transform.position = playerData.startPos;     
    }

   public void setRot(float rot)
    {
        Vector3 Rot = new Vector3(0, rot, 0);
        GetComponentsInChildren<Transform>()[1].eulerAngles = Rot;
    }
    
}
