using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClearComment : MonoBehaviour
{
    // Start is called before the first frame update
    public Text commnent;
    void Start()
    {
        int TotalShotScore = PlayerPrefs.GetInt("TotalShot");
        if (TotalShotScore > 10)
        {
            commnent.text = "もう少しショットの回数を減らせるようにがんばろう！";
        }
        else if (TotalShotScore > 5)
        {
            commnent.text = "おしい！あと少しショットの回数を減らそう！";
        }
        else
        {
            commnent.text = "すごい！君はこのゲームのマスターだ！";
        }

    }
}
