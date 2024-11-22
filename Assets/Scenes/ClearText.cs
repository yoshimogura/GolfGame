using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClearText : MonoBehaviour
{
    public Text Totaltext;
    public Text commnent;
    // public GUIText originalText; // オリジナルのテキスト 
    // public Color outlineColor = Color.black; // 縁取りの色 
    // public Vector2 outlineOffset = new Vector2(1, 1); // 縁取りのオフセット
    void Start()
    { // 保存されたスコアを取得する 
        int TotalShotScore = PlayerPrefs.GetInt("TotalShot");
        Debug.Log("取得したスコア: " + TotalShotScore);
        Totaltext.text = "合計のショット数:" + TotalShotScore.ToString() + "回！";
        if (TotalShotScore > 10)
        {
            commnent.text = "次はショット数を減らしてみよう！";
        }
        else if (TotalShotScore > 5)
        {
            commnent.text = "マスターまであと少し…ショット数をもう少し減らしてみよう！";
        }
        else
        {
            commnent.text = "君はこのゲームのマスターだ！";
            // commnent.text = "マスターまであと少し…ショット数をもう少し減らしてみよう！";
        }
    }
}

