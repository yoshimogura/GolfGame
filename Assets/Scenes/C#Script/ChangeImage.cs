using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeImage : MonoBehaviour
{
    // Start is called before the first frame update
    public Image targetImage; // Imageコンポーネントをアタッチ 
    public Sprite sprite1; // 打てない画像 
    public Sprite sprite2; // 打てる画像

    void Start()
    { // 初期画像を設定 

        targetImage.sprite = sprite2;
        Debug.Log("Start時打てる画像に");

    }
    public void Available()

    { // 画像を切り替える 
        Debug.Log("SwitchImageに来た");

        targetImage.sprite = sprite2;
        Debug.Log("SwitchImageで打てるのにした");



    }
    public void NotAvailable()
    {
        targetImage.sprite = sprite1;
        Debug.Log("SwitchImageで打てないのにした");
    }
}
