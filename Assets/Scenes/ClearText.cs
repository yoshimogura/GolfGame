using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClearText : MonoBehaviour
{
    public Text Totaltext;
    public Text commnent;
    void Start()
    { // 保存されたスコアを取得する 
        int TotalShotScore = PlayerPrefs.GetInt("TotalShot");
        Debug.Log("取得したスコア: " + TotalShotScore);
        Totaltext.text = "合計のショット数は" + TotalShotScore.ToString() + "回！";
        if (TotalShotScore > 10)
        {
            commnent.text = "もう少しショットの回数を減らせるようにがんばろう！";
        }
        else if (TotalShotScore > 5)
        {
            commnent.text = "おしい！あと少しショットの回数を減らせるようにがんばろう！";
        }
        else
        {
            commnent.text = "すごい！君はこのゲームのマスターだ！";
        }
    }

}
