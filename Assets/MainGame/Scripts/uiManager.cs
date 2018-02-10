using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class uiManager : MonoBehaviour {
    public GameObject leftUI, rightUI;
    public Text timeTxt;
    public Text timeTxt_buttom;
	
    public void updateTimeTxt(int i)
    {
        if (i < 0) i = 0;
        timeTxt.text = "TIME:" + i;
        timeTxt_buttom.text = "TIME:" + i;
    }
    public void updateScore(PlayerData _data)
    {
        int _score = (int)_data.score;
        if (_score > GameManager.game.MaxScore) _score = (int)GameManager.game.MaxScore;
        if (_data.Type == PlayerData.PlayerType.Player1) {
          
            
            for (int i = 0; i < _score; i++) {
                leftUI.GetComponentsInChildren<Image>()[i].enabled = true;
            }
            for(int i=_score; i< leftUI.GetComponentsInChildren<Image>().Length; i++)
            {
                leftUI.GetComponentsInChildren<Image>()[i].enabled = false;

            }
        }
        else
        {
            for (int i = 0; i < _score; i++)
            {
                rightUI.GetComponentsInChildren<Image>()[i].enabled = true;
            }
            for (int i = _score; i < leftUI.GetComponentsInChildren<Image>().Length; i++)
            {
                rightUI.GetComponentsInChildren<Image>()[i].enabled = false;

            }
        }
    }
}
