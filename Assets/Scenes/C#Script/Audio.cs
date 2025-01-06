
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    // Start is called before the first frame update
    private static Audio instance = null; // シングルトンインスタンス 
    public static Audio Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this; // インスタンスを設定 
            DontDestroyOnLoad(gameObject); // オブジェクトを破棄しないように設定 
        }
        else if (instance != this)
        {
            Destroy(gameObject); // 既に存在する場合は破棄 } }
        }
    }
}