using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public float Range;
    public float Smooth,RoateSpeed;
    float rotation;
    public Type type;
    public Vector3 StartPos;
    // Use this for initialization
    void Start () {
        StartPos = transform.position;

    }
	
	// Update is called once per frame
	void Update () {

        if (transform.position.y >= StartPos .y+ (Range)-0.01f)
        {//最高
            type = Type.Down;


        }
        else if (transform.position.y <= StartPos.y - (Range)+0.01f)
        {
            type = Type.Up;
          
        }

        switch (type)
        {
            case Type.Up:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, StartPos.y + Range, transform.position.z), Smooth * Time.deltaTime);
                //transform.position = new Vector3(transform.position.x ,Mathf.Lerp(transform.position.y, StartPos.y + Range,Smooth*Time.deltaTime),transform.position.z);
                transform.Rotate(new Vector3(0, RoateSpeed, 0));
                break;
            case Type.Down:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, StartPos.y - Range, transform.position.z), Smooth * Time.deltaTime);
              //  transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, StartPos.y - Range, Smooth * Time.deltaTime), transform.position.z);

                transform.Rotate(new Vector3(0, RoateSpeed, 0));
                break;
            default:
                break;
        }
    }

    void Move() {

    }
    public enum Type { Up,Down}
}
