using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour {

    public PlayerData playerData;
    float times;
    public float Timer;
    int Rcount,Lcount;
    float Rcoller,Lcoller;
    public Vector2 DashPower;
    public Player player;
    Rigidbody rb;
    private void Start()
    {
        player.GetComponent<Player>();
           playerData = GetComponent<PlayerData>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(playerData.playKeyCode.Left))
        {
         
         
            if (Lcoller > 0 && Lcount == 1/*Number of Taps you want Minus One*/)
            {
               
              
               // if (playerData._status == PlayerData.PlayerStatus.Idle) {
                    rb.AddForce(-DashPower, ForceMode.Impulse);
                    Lcount = 0;
               // }
            }
            else
            {
                Lcoller = Timer;
                Lcount += 1;
            }
        }
        if (Lcoller > 0)
        {

            Lcoller -= 1 * Time.deltaTime;

        }
        else
        {
            Lcount = 0;
        }

        if (Input.GetKeyDown(playerData.playKeyCode.Right))
        {

            if (Rcoller > 0 && Rcount == 1/*Number of Taps you want Minus One*/)
            {
              //  if (playerData._status == PlayerData.PlayerStatus.Idle)
              //  {
                    rb.AddForce(DashPower, ForceMode.Impulse);
                    Rcount = 0;
              //  }
            }
            else
            {
                Rcoller = Timer;
                Rcount += 1;
            }
        }
        if (Rcoller > 0)
        {

            Rcoller -= 1 * Time.deltaTime;

        }
        else
        {
            Rcount = 0;
        }
    }
}
