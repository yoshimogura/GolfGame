using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;
public class putter : MonoBehaviour
{

  public float forceAmount = 3f; // ボールに与える力の量


  private Rigidbody rb;
  // Start is called before the first frame update



  public bool first = true;
  public float startMonitoringSpeed = 0.8f;
  // スピードが停止したと判断する閾値
  public float stopSpeedThreshold = 0.5f;
  // 動いているフラグ 
  public bool move = false;
  // 力を加えたフラグ（グッと押した１回のキー入力を１回として捉える）
  public bool addForce = false;
  float maxShotPower = 40f;     // 最大のショット強さ
  public TextMeshProUGUI powerText;
  private Vector3 shotDirection;       // ショットの方向
  private float shotPower;             // 現在のショット強さ
  //ショットの数
  public static int shotcount = 0;

  public Text scoreText; // スコアを表示するText（Legacy）
  public Text shotText; // 打てるかどうかを表すText（Legacy）

  bool shot = false;
  public GameObject cameraObject;  // カメラオブジェクトをインスペクタで設定
  public Transform cup;            // カップのTransform

  public float distanceFromBall = 10f; // ボールからカメラをどれだけ離すか


  string NextScenename = "";
  public TextMeshProUGUI nextStageText; // "Next Stage"テキスト用のUI Text 
  float displayDuration = 3f; // テキスト表示時間 
  private bool isSceneSwitching = true;
  bool cameraPermission = true;
  Plane plane = new Plane();
  float distance = 0;
  private bool isOnSlope = false;
  bool Cupin = false;
  public AudioClip sound1;//音
  AudioSource audioSource;//音
  public ChangeImage imageSwitcher;
  void Start()
  {
    rb = GetComponent<Rigidbody>();

    Debug.Log(transform.forward);
    UpdateScoreText();
    CameraControlle();
    nextStageText.gameObject.SetActive(false);
    audioSource = GetComponent<AudioSource>();//音の取得をしてる
  }
  void CameraControlle()
  {

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
      powerText.text = $"Power: {shotPower * 2.5:F1}";

    }
    // ボールが動いている時に速度をログに出力
    if (move && first)
    {
      Debug.Log("magnitude: " + rb.velocity.magnitude);
      if (94 > this.transform.position.x)
      {
        GameObject camera = GameObject.Find("Main Camera");
        camera.transform.position = new Vector3(106, 24, -61);
        first = false;
      }
      else
      {
        // GameObject camera = GameObject.Find("Main Camera");  
        // camera.transform.position = new Vector3(148, 25, -61);
      }
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

      rb.velocity = Vector3.zero;
      rb.angularVelocity = Vector3.zero;
      imageSwitcher.SwitchImage();

      // Rigidbodyの物理演算を停止して完全に静止させる
      rb.isKinematic = true;
      move = false;
      addForce = false;
      shot = false;
      // CameraControllerスクリプトを取得
      CameraController cameraController = cameraObject.GetComponent<CameraController>();

      if (cameraController != null && cameraPermission)
      {
        // カメラの位置を設定し、カップを向く
        Vector3 direction = (cup.position - this.transform.position).normalized;
        Vector3 newCameraPosition = this.transform.position - direction * 16f; // 例としてカメラの新しい位置
        newCameraPosition.y += 12f;

        cameraController.SetPosition(newCameraPosition, cup);
      }
      // else
      // {
      //     Debug.LogError("CameraControllerが見つかりません。もしくはcameraPermissionがfalseです");
      // }
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
      imageSwitcher.SwitchImage();
      //その方向に力を加える
      Debug.Log("space key down");
      rb.isKinematic = false;
      rb.AddForce(transform.forward * (shotPower / 12), ForceMode.Impulse);
      GetComponent<AudioSource>().Play();//音
      addForce = true;
      shotcount = shotcount + 1;
      UpdateScoreText();
      shot = true;

    }

  }
  IEnumerator SwitchScene()
  {
    audioSource.PlayOneShot(sound1);
    isSceneSwitching = true; nextStageText.gameObject.SetActive(true); // "Next Stage"テキストを表示 
    Cupin = true;
    yield return new WaitForSeconds(displayDuration); // 指定時間待機
    PlayerPrefs.SetInt("TotalShot", shotcount);
    PlayerPrefs.Save();
    SceneManager.LoadScene(NextScenename);// 次のシーンに移動 
    nextStageText.gameObject.SetActive(false); // テキストを非表示（新しいシーンで非表示にする） 
    isSceneSwitching = false;
    cameraPermission = true;
  }
  void UpdateScoreText()
  {
    // スコアをテキストに反映
    scoreText.text = shotcount.ToString();
  }
  void UpdateShotText()
  {
    if (shot)
    {
      shotText.text = "移動中...";
    }
    else
    {
      shotText.text = "停止中";
    }
    // スコアをテキストに反映

  }
  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.name == "池の判定")
    {
      if (SceneManager.GetActiveScene().name == "1ndStage")
      {
        SceneManager.LoadScene("1ndStage");
      }
      else
      if (SceneManager.GetActiveScene().name == "2ndStage")
      {
        SceneManager.LoadScene("2ndStage");
      }
      else
      if (SceneManager.GetActiveScene().name == "3ndStage")
      {
        SceneManager.LoadScene("3ndStage");
      }
      else
      {
        SceneManager.LoadScene("4ndStage");
      }
    }
    //穴に入った判定
    if (collision.gameObject.name == "HollDetection" && !Cupin)
    {
      if (SceneManager.GetActiveScene().name == "4ndStage")
      {
        NextScenename = "Clear";
      }
      else if (SceneManager.GetActiveScene().name == "1ndStage")
      {
        NextScenename = "2ndStage";
      }
      else if (SceneManager.GetActiveScene().name == "2ndStage")
      {
        NextScenename = "3ndStage";
      }
      else if (SceneManager.GetActiveScene().name == "3ndStage")
      {
        NextScenename = "4ndStage";
      }
      cameraPermission = false;
      StartCoroutine(SwitchScene());
    }
  }
}
