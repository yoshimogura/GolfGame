using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject boll;
    public Transform ball; // ボールのTransform
    private Transform cup;  // カップのTransform
    public float distanceFromBall = 10f; // ボールからカメラをどれだけ離すか  
    void Start(){
        SetFirstPosition();
        
    }
    // Update is called once per frame
    void Update()
    {   
  


    }
    void SetFirstPosition(){
        this.transform.position = new Vector3(148, 25, -61);
    }
    void SetCupPosition(){
         this.transform.position = new Vector3(106, 20, -61);
    }
    void SetBallPosition(){
        GameObject cupObject = GameObject.Find("frag"); 
      if (cupObject != null)
        {
            // カップ（旗）のTransformを取得
            cup = cupObject.transform;

            // ボールとカップ（旗）の間の方向を計算
            Vector3 direction = (cup.position - ball.position).normalized;

            // カメラの位置をボールから一定の距離に設定
            transform.position = ball.position - direction * distanceFromBall;

            // カメラがカップ（旗）を向くように設定

            transform.LookAt(cup);
        }
        else
        {
            Debug.LogError("Cupオブジェクトが見つかりません。");
        }
    }
    }
