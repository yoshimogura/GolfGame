using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;

public class TextManager : MonoBehaviour
{
     public Text scoreText; // スコアを表示するText（Legacy）
    private int score = 0; // 初期スコア
    // Start is called before the first frame update
    
    void Start()
    {
        UpdateScoreText();
    }
     void UpdateScoreText()
    {
        // スコアをテキストに反映
        scoreText.text = "shotcount: " + score.ToString();
    }
    // Update is called once per frame
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }
}
