using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeImage : MonoBehaviour
{
    // Start is called before the first frame update
    public Image targetImage; // Imageコンポーネントをアタッチ 
    public Sprite sprite1; // 1つ目の画像 
    public Sprite sprite2; // 2つ目の画像 
    private bool isFirstImage = true; void Start()
    { // 初期画像を設定 
        if (sprite1 != null) { targetImage.sprite = sprite1; }
    }
    public void SwitchImage()
    { // 画像を切り替える 
        if (isFirstImage && sprite2 != null)
        {
            targetImage.sprite = sprite2;
        }
        else if (sprite1 != null)
        {
            targetImage.sprite = sprite1;
        }

        isFirstImage = !isFirstImage; // 状態を切り替える }
    }
}
