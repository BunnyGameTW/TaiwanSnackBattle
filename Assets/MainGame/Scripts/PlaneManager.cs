using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour {
    //生成幾個
    //生成類型
    //生成位置  
    public int initialNum;//一開始要幾個
    [System.Serializable]
    public struct PlaneList
    {
        public GameObject plane;
        public int chance;
    }
    public PlaneList[] _planes;

    // Update is called once per frame
    void Update()
    {
        //TODO:每隔幾秒隨機產生or消失
    }
    public void randomGenerate(int n)
    {
       
        int mother = 0;
        foreach (PlaneList _plane in _planes)
        {
            mother += _plane.chance;
        }

        for (int i = 0; i < n; i++)
        {
            int son = Random.Range(1, mother + 1);
            int index = 999;
            int tmp;
            if (son <= _planes[0].chance) index = 0;
            else
            {
                tmp = _planes[0].chance;
                for (int j = 1; j <= _planes.Length; j++)
                {
                    if (son > tmp && son <= tmp + _planes[j].chance)
                    {
                        index = j;
                        break;
                    }
                    else
                    {
                        tmp += _planes[j].chance;
                    }
                }
            }
            //for (int j = 0; j < n; j++) //TODO: 其實沒隨機 弄個串列把選過的剔除在隨機選
            //{
            //    int x = Random.Range(0, GameManager.game.planeRanPos.Length);// 5 0~4
            //    if (!GameManager.game.planeRanPos[0].GetComponent<randomPlane>().hasPlane)
            //    {

            //        Instantiate(_planes[index].plane, planePos.transform.position, transform.rotation, planePos.transform);
            //        planePos.GetComponent<randomPlane>().hasPlane = true;
            //        break;
            //    }
            //}
            foreach (GameObject planePos in GameManager.game.planeRanPos)
            {
                if (!planePos.GetComponent<randomPlane>().hasPlane)
                {
                    Instantiate(_planes[index].plane, planePos.transform.position, transform.rotation,planePos.transform);
                    planePos.GetComponent<randomPlane>().hasPlane = true;
                    break;
                }
            }
             
        }
    }
    public void generateSpecial(int _type, int num)//產生第幾種物品
    {
        for (int i = 0; i < num; i++)
        {
            //TODO:產生位置
            Instantiate(_planes[_type].plane, transform.position, transform.rotation);
        }

    }
}
