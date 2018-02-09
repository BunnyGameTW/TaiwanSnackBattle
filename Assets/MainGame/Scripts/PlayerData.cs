using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    
    public float Speed, JumpHeight, JumpSpeed, DownSpeed;
    public float pSpeed;
    public float score;
    public PlayKeyCode playKeyCode;
    [System.Serializable]
    public struct PlayKeyCode
    {
        public KeyCode Jump;      
        public KeyCode Left;
        public KeyCode Right;
    }
    public enum PlayerType { Player1, Player2 }
    public PlayerType Type;
    public enum PlayerStatus { Idle, JumpUp, JumpDown }
    public PlayerStatus _status;
    public bool canJump, canMove;

    void Start()
    {
        canJump = canMove = true;
        pSpeed = Speed;
    }
}


