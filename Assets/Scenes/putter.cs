using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class putter : MonoBehaviour
{

    public float forceAmount = 3f; // ボールに与える力の量
  
　
    private Rigidbody rb;
    // Start is called before the first frame update
    


    public bool logging;
    public float startMonitoringSpeed = 2f;
  // スピードが停止したと判断する閾値
  public float stopSpeedThreshold = 0.5f;
  // 動いているフラグ
  public bool move = false;
  // 力を加えたフラグ（グッと押した１回のキー入力を１回として捉える）
  public bool addForce = false;

  //ショットの数
  public static int shotcount=0;

  public Text scoreText; // スコアを表示するText（Legacy）
  public Text shotText; // 打てるかどうかを表すText（Legacy）
  
  bool shot=false;
  public GameObject cameraObject;  // カメラオブジェクトをインスペクタで設定
  public Transform cup;            // カップのTransform

  public float distanceFromBall = 10f; // ボールからカメラをどれだけ離すか

Plane plane = new Plane();
	    float distance = 0;
    
    void Start(){
        rb = GetComponent<Rigidbody>();
        
        Debug.Log(transform.forward);
        UpdateScoreText();
        UpdateShotText();
        CameraControlle();
    }
    void CameraControlle(){
      
    }
    void Update()
    {
      // ボールが動いている時に速度をログに出力
    if (move && logging)
    {
      Debug.Log("magnitude: " + rb.velocity.magnitude);
      if(94>this.transform.position.x){
            GameObject camera = GameObject.Find("Main Camera"); 
            camera.transform.position = new Vector3(106, 20, -61);
      }else{
            GameObject camera = GameObject.Find("Main Camera");  
            camera.transform.position = new Vector3(148, 25, -61);
        }
    }

    // 速度が一定以上になったら速度監視を開始（すぐ監視すると、動かす前に止まる）
    if (!move && rb.velocity.magnitude >= startMonitoringSpeed)
    {
      Debug.Log("start move");
      move = true; // 速度監視を開始

    }
 
    // 速度監視が開始されている場合、速度を監視する
    if (move && rb.velocity.magnitude < stopSpeedThreshold&&(transform.position.y>=15.6||transform.position.y<=12.79))
    {
      Debug.Log("stop move");
      // ボールを完全に停止させる
      
      rb.velocity = Vector3.zero;
      rb.angularVelocity = Vector3.zero;

      // Rigidbodyの物理演算を停止して完全に静止させる
      rb.isKinematic = true;
      move = false;
      addForce = false;
      shot=false;
      UpdateShotText();
      // CameraControllerスクリプトを取得
            CameraController cameraController = cameraObject.GetComponent<CameraController>();

            if (cameraController != null)
            {
                // カメラの位置を設定し、カップを向く
                Vector3 direction = (cup.position - this.transform.position).normalized;
                Vector3 newCameraPosition =this.transform.position - direction * 16f; // 例としてカメラの新しい位置
                newCameraPosition.y +=12f; 
                cameraController.SetPosition(newCameraPosition, cup);
            }
            else
            {
                Debug.LogError("CameraControllerが見つかりません。");
            }
    }
      //打つ
    if (!move && Input.GetKeyDown(KeyCode.Space) && !addForce)
    {
      //マウスの位置で方向を決定
    // カメラとマウスの位置を元にRayを準備
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		// プレイヤーの高さにPlaneを更新して、カメラの情報を元に地面判定して距離を取得
		plane.SetNormalAndPosition (Vector3.up, transform.localPosition);
		if (plane.Raycast (ray, out distance)) {
			// 距離を元に交点を算出して、交点の方を向く
			var lookPoint = ray.GetPoint(distance);
			transform.LookAt (lookPoint);
    }
    //その方向に力を加える
      Debug.Log("space key down");
      rb.isKinematic = false;
      rb.AddForce(transform.forward * forceAmount, ForceMode.Impulse);
      addForce = true;
      shotcount=shotcount+1;
      UpdateScoreText();
      shot=true;
      UpdateShotText();
      
    }
    //穴に入った判定
      if(this.transform.position.y < 11)
      {
        if(SceneManager.GetActiveScene().name=="1ndStage"){
          SceneManager.LoadScene("2ndStage");
        }else　if(SceneManager.GetActiveScene().name=="2ndStage"){
          SceneManager.LoadScene("3ndStage");
        }else if(SceneManager.GetActiveScene().name=="3ndStage"){
          SceneManager.LoadScene("4ndStage");
        }else{
          SceneManager.LoadScene("Title");
        }
  
      }
    



     
    }
    void UpdateScoreText()
    {
        // スコアをテキストに反映
        scoreText.text = "打った回数: " + shotcount;
    }
    void UpdateShotText()
    {
      if(shot){
        shotText.text = "移動中...";
      }else{
        shotText.text = "停止中";
      }
        // スコアをテキストに反映
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="当たってはいけない壁"){
            if(SceneManager.GetActiveScene().name=="3ndStage"){
          SceneManager.LoadScene("3ndStage");
        }else{
          SceneManager.LoadScene("4ndStage");
        }
        }
    }
}
