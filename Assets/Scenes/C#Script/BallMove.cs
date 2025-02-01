using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;
using System;
public enum BallState
{
  Placed,
  KeyInput,
  MoveStarted,
  Moving
}
public class BallMove : MonoBehaviour
{
  private BallState currentState = BallState.Placed;
  public float forceAmount = 3f; // ボールに与える力の量
  public bool first = true;
  public float startMonitoringSpeed = 0.4f;
  // スピードが停止したと判断する閾値
  public float stopSpeedThreshold = 0.5f;


  // 力を加えたフラグ（グッと押した１回のキー入力を１回として捉える）
  float maxShotPower = 20f;     // 最大のショット強さ

  private Vector3 shotDirection;       // ショットの方向
  private float shotPower;             // 現在のショット強さ
  public Text shotText; // 打てるかどうかを表すText（Legacy）



  public float distanceFromBall = 11f; // ボールからカメラをどれだけ離すか

  Plane plane = new Plane();
  float distance = 0;
  public bool Cupin = false;
  public Rigidbody rb;
  private Global globalScript;
  public ChangeImage ChangeImageScript;
  //坂の影響を考慮して、停止カウントが一定になった時に完全に停止したと判定する
  int stopcount = 0;
  int a = 0;
  void Start()
  {

    GameObject globalObject = GameObject.Find("Global");
    if (globalObject != null)
    {
      globalScript = globalObject.GetComponent<Global>();
      if (globalScript == null)
      {
        Debug.LogError("Global script is not attached to the Global object!");
      }
    }
    else
    {
      Debug.LogError("Global object not found!");
    }
    rb = GetComponent<Rigidbody>();
    Debug.Log(transform.forward);

  }

  void FixedUpdate()
  {

    switch (currentState)
    {
      case BallState.Placed:

        // マウスからレイを飛ばして衝突地点を取得
        // カメラとマウスの位置を元にRayを準備

        currentState = BallState.KeyInput;


        break;

      case BallState.KeyInput:
        //音
        Ray Powerray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(Powerray, out hit))
        {
          // 衝突地点とボールの位置から距離と方向を計算
          Vector3 hitPosition = hit.point;
          // shotDirection = (hitPosition - this.transform.position).normalized;
          shotPower = Mathf.Clamp(Vector3.Distance(this.transform.position, hitPosition), 0, maxShotPower);

          // UIに現在のショット強さを表示

          // powerText.text = $"Power: {shotPower * 2.5:F1}";
          globalScript.SwitchShotPower(shotPower);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
          globalScript.ShotBall();
          //マウスの位置で方向を決定
          // カメラとマウスの位置を元にRayを準備
          var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          // プレイヤーの高さにPlaneを更新して、カメラの情報を元に地面判定して距離を取得
          plane.SetNormalAndPosition(Vector3.up, transform.localPosition);
          if (plane.Raycast(ray, out distance))
          {
            // 距離を元に交点を算出して、交点の方を向く
            var lookPoint = ray.GetPoint(distance);
            transform.LookAt(lookPoint);
          }

          //その方向に力を加える
          Debug.Log("space key down");

          rb.AddForce(transform.forward * (shotPower / 12), ForceMode.Impulse);
          Debug.Log(transform.forward * (shotPower / 12));

          currentState = BallState.MoveStarted;


        }


        break;







      case BallState.MoveStarted:
        ChangeImageScript.SwitchImage();
        a++;
        currentState = BallState.Moving;


        break;

      case BallState.Moving:
        //ボールが止まった判定
        if (rb.velocity.magnitude < startMonitoringSpeed && !Cupin)
        {
          stopcount++;
          if (stopcount >= 150)
          {
            a++;
            Debug.Log(a);
            globalScript.StopBall();
            Debug.Log("stop ball");
            // ボールを完全に停止させる
            globalScript.ChangeCamera();//カメラの視点変更
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            ChangeImageScript.SwitchImage();

            // Rigidbodyの物理演算を停止して完全に静止させる

            currentState = BallState.KeyInput;
          }
        }
        else
        {
          stopcount = 0;
        }


        break;
    }





    // 速度監視が開始されている場合、速度を監視する


  }
  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.name == "Pond")
    {
      globalScript.ballFalling();

    }
    //穴に入った判定
    if (collision.gameObject.name == "HollDetection" && !Cupin)
    {

      globalScript.cameraPermission = false;
      globalScript.SwitchScene();
    }
  }


}
