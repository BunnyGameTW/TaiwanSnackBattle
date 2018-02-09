using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public float minSize;
    public float MaxSize;
    public float lerpRatio;
    public Transform LB, RT;
    public Transform Player1,Player2;
    public enum Type { Prspective, Orthogonal }
    public Type type;
    public float yOffset;
    float distance;
    public GameObject emp;
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        float offsetX = Mathf.Abs( Player1.position.x - Player2.position.x);
        float offsetY = Mathf.Abs(Player1.position.y - Player2.position.y);

        distance = Vector2.Distance(Player1.position, Player2.position);
        if (offsetY <= 1)
        {
            if (offsetX < 5) { yOffset = 1; }
            else if (offsetX > 10) yOffset = 3;
            else yOffset = 2;
        }
        else if (offsetY > 1 && offsetY <= 3) {
            if (offsetX < 5) { yOffset = 0; }
            else if (offsetX > 10) yOffset = 2;
            else yOffset = 1;
        }
        else yOffset = 0;
        Vector3 v = transform.position;
        v.x = (Player1.position.x + Player2.position.x) * 0.5f;
        v.y = (Player1.position.y + Player2.position.y) * 0.5f + yOffset;
        transform.position = Vector3.Lerp(transform.position, v, lerpRatio * Time.deltaTime);

      
        switch (type)
        {
            case Type.Prspective:
                GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 36f + 76f * (distance / 17), lerpRatio * Time.deltaTime);
                GetComponent<Camera>().orthographic = false;
                break;
            case Type.Orthogonal:
                if (distance < minSize) distance = minSize;
                if (distance > MaxSize) distance = MaxSize;
                GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, distance, lerpRatio * Time.deltaTime);
                GetComponent<Camera>().orthographic = true;
                break;
            default:
                break;
        }
        float VerticalHightSeen = Camera.main.orthographicSize * 2.0f;
        float HorizontalHeightSeen = VerticalHightSeen * Screen.width / Screen.height;
        Vector3 _LB2= GetComponent<Camera>().ScreenToWorldPoint(transform.position);
        Vector3 _RT2 = GetComponent<Camera>().ScreenToWorldPoint(transform.position + new Vector3(GetComponent<Camera>().scaledPixelWidth, GetComponent<Camera>().scaledPixelHeight, 0));

        if (_LB2.x < LB.position.x)
        {
            float tmp = LB.position.x - _LB2.x;
            Vector3 _v = transform.position;
            _v.x += tmp;
            transform.position = _v;

        }
       else if (_RT2.x > RT.position.x)
        {

            float tmp = RT.position.x - _RT2.x;
            Vector3 _v = transform.position;
            _v.x += tmp;
            transform.position = _v;
        }
        if (_LB2.y < LB.position.y)
        {
            float tmp = LB.position.y - _LB2.y;
            Vector3 _v = transform.position;
            _v.y += tmp;
            transform.position = _v;
        }
        else if (_RT2.y > RT.position.y)
        {
            float tmp = RT.position.y - _RT2.y;
            Vector3 _v = transform.position;
            _v.y += tmp;
            transform.position = _v;
        }

    }

}
