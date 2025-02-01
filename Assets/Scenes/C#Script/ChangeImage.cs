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
    private bool NowImage;
    void Start()
    { // 初期画像を設定 

        targetImage.sprite = sprite2;
        NowImage = true;//打てる画像
        Debug.Log("Start時打てる画像に");

    }
    public void SwitchImage()

    { // 画像を切り替える 
        Debug.Log("SwitchImageに来た");
        if (!NowImage)
        {
            targetImage.sprite = sprite2;
            NowImage = true;
            Debug.Log("SwitchImageで打てるのにした");
        }
        else
        {
            targetImage.sprite = sprite1;
            NowImage = false;
            Debug.Log("SwitchImageで打てないのにした");
        }
    }
}
