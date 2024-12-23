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
public class BallMove : MonoBehaviour
{

  public float forceAmount = 3f; // ボールに与える力の量


  // private Rigidbody rb; 



  public bool first = true;
  public float startMonitoringSpeed = 0.8f;
  // スピードが停止したと判断する閾値
  public float stopSpeedThreshold = 0.5f;
  // 動いているフラグ 
  public bool move = false;
  // 力を加えたフラグ（グッと押した１回のキー入力を１回として捉える）
  public bool addForce = false;
  float maxShotPower = 20f;     // 最大のショット強さ

  private Vector3 shotDirection;       // ショットの方向
  private float shotPower;             // 現在のショット強さ
                                       //ショットの数



  public Text shotText; // 打てるかどうかを表すText（Legacy）

  bool shot = false;

  public float distanceFromBall = 11f; // ボールからカメラをどれだけ離すか


  string NextScenename = "";




  Plane plane = new Plane();
  float distance = 0;
  private bool isOnSlope = false;
  public bool Cupin = false;


  bool check = true;
  public Rigidbody rb;
  private Global globalScript;
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
    globalScript.ChangeCamera();
    rb = GetComponent<Rigidbody>();

    Debug.Log(transform.forward);


  }

  void OnCollisionStay(Collision collision)
  { // "Slope"というタグを持つオブジェクトに触れているとき 
    if (collision.gameObject.CompareTag("slope")) { isOnSlope = true; }
  }
  void OnCollisionExit(Collision collision)
  { // "Slope"というタグを持つオブジェクトから離れたとき 
    if (collision.gameObject.CompareTag("slope")) { isOnSlope = false; }
  }
  void Update()
  {
    // マウスからレイを飛ばして衝突地点を取得
    Ray Powerray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    if (Physics.Raycast(Powerray, out hit))
    {
      // 衝突地点とボールの位置から距離と方向を計算
      Vector3 hitPosition = hit.point;
      shotDirection = (hitPosition - this.transform.position).normalized;
      shotPower = Mathf.Clamp(Vector3.Distance(this.transform.position, hitPosition), 0, maxShotPower);

      // UIに現在のショット強さを表示

      // powerText.text = $"Power: {shotPower * 2.5:F1}";
      globalScript.SwitchShotPower(shotPower);
    }
    // ボールが動いている時に速度をログに出力
    if (move && first && check)
    {
      check = false;
      Debug.Log("magnitude: " + rb.velocity.magnitude);
      if (94 > this.transform.position.x)
      {
        GameObject camera = GameObject.Find("Main Camera");
        camera.transform.position = new Vector3(106, 24, -61);
        first = false;
      }

    }
    if (!first && (94 <= this.transform.position.x))
    {
      GameObject camera = GameObject.Find("Main Camera");
      // camera.transform.position = new Vector3(148, 25, -62);
    }
    // 速度が一定以上になったら速度監視を開始（すぐ監視すると、動かす前に止まる）
    if (!move && rb.velocity.magnitude > 0)
    {
      Debug.Log("start move");
      move = true; // 速度監視を開始

    }

    // 速度監視が開始されている場合、速度を監視する
    if (move && rb.velocity.magnitude < startMonitoringSpeed && !isOnSlope && !Cupin)
    {
      Debug.Log("stop move");
      // ボールを完全に停止させる
      globalScript.ChangeCamera();//カメラの視点変更
      rb.velocity = Vector3.zero;
      rb.angularVelocity = Vector3.zero;
      globalScript.SwitchImage();

      // Rigidbodyの物理演算を停止して完全に静止させる
      rb.isKinematic = true;
      move = false;
      addForce = false;
      shot = false;

    }
    //打つ
    if (!move && Input.GetKeyDown(KeyCode.Space) && !addForce)
    {
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
      globalScript.SwitchImage();
      //その方向に力を加える
      Debug.Log("space key down");
      rb.isKinematic = false;
      rb.AddForce(transform.forward * (shotPower / 12), ForceMode.Impulse);
      Debug.Log(transform.forward * (shotPower / 12));

      //音
      addForce = true;
      Debug.Log("saaaa" + globalScript.shotcount);
      globalScript.shotcount = globalScript.shotcount + 1;

      globalScript.ChangeShotcount();

      shot = true;
    }
  }


  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.name == "Pond")
    {
      if (SceneManager.GetActiveScene().name == "Stage1")
      {
        SceneManager.LoadScene("Stage1");
      }
      else
      if (SceneManager.GetActiveScene().name == "Stage2")
      {
        SceneManager.LoadScene("Stage2");
      }
      else
      if (SceneManager.GetActiveScene().name == "Stage3")
      {
        SceneManager.LoadScene("Stage3");
      }
      else
      {
        SceneManager.LoadScene("Stage4");
      }
    }
    //穴に入った判定
    if (collision.gameObject.name == "HollDetection" && !Cupin)
    {
      if (SceneManager.GetActiveScene().name == "Stage4")
      {

        NextScenename = "Clear";
      }
      else if (SceneManager.GetActiveScene().name == "Stage1")
      {

        NextScenename = "Stage2";
        // NextScenename = "Clear";
      }
      else if (SceneManager.GetActiveScene().name == "Stage2")
      {

        // NextScenename = "Clear";
        NextScenename = "Stage3";
      }
      else if (SceneManager.GetActiveScene().name == "Stage3")
      {

        NextScenename = "Stage4";
      }
      globalScript.cameraPermission = false;
      globalScript.SwitchScene();
    }
  }
}
