using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEditor.PackageManager;

public class Global : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject objectToClone;
    public GameObject objectToClone2;
    public GameObject objectToClone3;
    public GameObject ball;

    int numberOfClones = 10; // 生成するクローンの数
    float yPosition = 17f; // Y座標の位置
    float zPosition = -75f; // Z座標の位置

    public Vector3 spawnPosition;
    public GameObject ballPrefab;
    public float maxShotPower = 100f;
    private Vector3 shotDirection;
    private float shotPower;
    public AudioClip sound1;//音
    AudioSource audioSource;//音
    public TextMeshProUGUI ShotCountText; // スコアを表示するTextMeshPro
    public TextMeshProUGUI nextStageText; // "Next Stage"テキスト用のUI Text 
    public TextMeshProUGUI powerText;
    public ChangeImage imageSwitcher;
    private BallMove ballMoveScript;
    public bool cameraPermission = true;
    public GameObject cameraObject;
    public Transform cup;
    float displayDuration = 3f; // テキスト表示時間 

    public Transform cameraTransform;
    public Vector3 cameraOffset = new Vector3(10, 20, 0);
    public string NextScenename = "";
    bool shot = false;
    int SceneNumber = 1;
    bool SceneChangeCheck = false;

    void Start()
    {
        Debug.Log("Start");
        GenerateClones();
        SpawnBall(spawnPosition);
        nextStageText.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();//音の取得をして
        GameObject camera = GameObject.Find("Main Camera");
        // camera.transform.position = new Vector3(148, 25, -62);
        GameObject ballMoveObject = GameObject.FindWithTag("BallMoveObject");
        ballMoveScript = ballMoveObject.GetComponent<BallMove>();
        SceneChangeCheck = false;
        Debug.Log(SceneChangeCheck);
        ChangeCamera();
        int currentScore = GameManager.Instance.GetShotCount();
        ShotCountText.text = "Score:" + currentScore;


    }
    void SpawnBall(Vector3 position)
    {
        GameObject ballObj = Instantiate(ball, position, Quaternion.identity);

        ballObj.AddComponent<BallMove>();
        // Instantiate(ball, new Vector3(0, 0, 0), Quaternion.identity);
    }
    public void ShotBall()
    {
        if (!shot)
        {
            GameManager.Instance.AddShotCount(1);
            int currentScore = GameManager.Instance.GetShotCount();
            ShotCountText.text = "Score:" + currentScore;
            shot = true;
            Debug.Log("PrepareToShotcount");
        }


    }
    public void PrepareToShot()
    {

        shot = false;
        ChangeCamera();
        imageSwitcher.Available();

        //a
    }
    public void ballMoveStart()
    {
        imageSwitcher.NotAvailable();

    }
    public void ChangeCamera()
    {
        Debug.Log("zChangecameraを呼んだ");
        Ray Powerray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(Powerray, out hit))
        {
            GameObject ball = GameObject.FindWithTag("BallMoveObject");
            Vector3 targetObject = ball.transform.position;
            // 衝突地点とボールの位置から距離と方向を計算
            Vector3 hitPosition = hit.point;
            shotDirection = (hitPosition - targetObject).normalized;
            shotPower = Mathf.Clamp(Vector3.Distance(targetObject, hitPosition), 0, maxShotPower);


            // CameraControllerスクリプトを取得
            CameraController cameraController = cameraObject.GetComponent<CameraController>();


            // カメラの位置を設定し、カップを向く
            Vector3 direction = (cup.position - targetObject).normalized;
            Vector3 newCameraPosition = targetObject - (direction * 16f); // 例としてカメラの新しい位置
            newCameraPosition.y += 12f;
            cameraController.SetPosition(newCameraPosition, cup);
        }
    }
    public void ballFalling()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    public void SwitchScene()
    {
        if (!SceneChangeCheck)
        {
            SceneNumber++;
            SceneChangeCheck = true;


            if (SceneManager.GetActiveScene().name == "Stage4")
            {

                NextScenename = "Clear";
            }
            else
            {
                NextScenename = "Stage" + SceneNumber;
            }
            WaitAndPrint();
            // audioSource.PlayOneShot(sound1);

            nextStageText.gameObject.SetActive(true); // "Next Stage"テキストを表示 
            ballMoveScript.Cupin = true;
            Debug.Log(NextScenename);
            SceneManager.LoadScene(NextScenename);// 次のシーンに移動 

            nextStageText.gameObject.SetActive(false); // テキストを非表示（新しいシーンで非表示にする） 
            cameraPermission = true;


            Debug.Log("OK");
        }
    }
    IEnumerator WaitAndPrint()
    { // 3秒待つ 
        yield return new WaitForSeconds(3); // 3秒待った後に実行する処理 
        Debug.Log("3秒経ちました！");
    }



    // ボールが動いている時に速度をログに出力
    public void UpdateShotPower(float shotPower)
    {
        powerText.text = $"Power: {shotPower * 5:F1}";

    }


    void GenerateClones()
    {
        // GetComponent<AudioSource>().Play();
        for (int i = 0; i < numberOfClones; i++)
        {
            // X座標
            float xPosition = Random.Range(74f, 135f);
            if (Random.value < 0.5f)
            {
                zPosition = Random.Range(-75f, -73f);
            }
            else
            {
                zPosition = Random.Range(-45f, -48f);
            }
            // クローンを生成
            Vector3 clonePosition = new Vector3(xPosition, yPosition, zPosition);
            float random = Random.value;
            if (random < 0.333f)
            {
                Instantiate(objectToClone, clonePosition, Quaternion.identity);
            }
            else if (random < 0.666f)
            {
                Instantiate(objectToClone2, clonePosition, Quaternion.identity);
            }
            else
            {
                Instantiate(objectToClone3, clonePosition, Quaternion.identity);
            }
        }

    }

}
