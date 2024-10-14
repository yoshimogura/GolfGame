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
    public float startMonitoringSpeed = 1.5f;
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



Plane plane = new Plane();
	    float distance = 0;

    void Start(){
        rb = GetComponent<Rigidbody>();


        
        Debug.Log(transform.forward);
        UpdateScoreText();
        UpdateShotText();

    }
    

    void Update()
    {
      // ボールが動いている時に速度をログに出力
    if (move && logging)
    {
      Debug.Log("magnitude: " + rb.velocity.magnitude);
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
    }
      //打つ
    if (!move && Input.GetKeyDown(KeyCode.Space) && !addForce)
    {
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
      if(this.transform.position.y < 10)
      {
        
        if(SceneManager.GetActiveScene().name=="1ndStage"){
          SceneManager.LoadScene("2ndStage");
        }else　if(SceneManager.GetActiveScene().name=="2ndStage"){
          SceneManager.LoadScene("3ndStage");
        }else{
          SceneManager.LoadScene("Title");
        }
  
      }
    
    


    // カメラとマウスの位置を元にRayを準備
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		// プレイヤーの高さにPlaneを更新して、カメラの情報を元に地面判定して距離を取得
		plane.SetNormalAndPosition (Vector3.up, transform.localPosition);
		if (plane.Raycast (ray, out distance)) {

			// 距離を元に交点を算出して、交点の方を向く
			var lookPoint = ray.GetPoint(distance);
			transform.LookAt (lookPoint);
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
}
