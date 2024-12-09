using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class clone : MonoBehaviour
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
    public TextMeshProUGUI powerText;
    public float maxShotPower = 100f;
    private Vector3 shotDirection;
    private float shotPower;
    void Start()
    {

        SpanBall(spawnPosition);


    }
    void SpanBall(Vector3 position)
    {
        Instantiate(ball, position, Quaternion.identity);
        GameObject ballInstance = Instantiate(ball, new Vector3(0, 0, 0), Quaternion.identity);
    }
    void Update()
    {

        // Ray Powerray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // RaycastHit hit; if (Physics.Raycast(Powerray, out hit))
        // {
        //     Vector3 hitPosition = hit.point; // ボールを生成 
        //     GameObject ballInstance = Instantiate(ballPrefab, transform.position, Quaternion.identity);

        //     shotDirection = (hitPosition - ballInstance.transform.position).normalized;
        //     shotPower = Mathf.Clamp(Vector3.Distance(ballInstance.transform.position, hitPosition), 0, maxShotPower);
        //     // UIに現在のショット強さを表示 
        //     powerText.text = $"Power: {shotPower * 2.5:F1}"; // Rigidbodyを使って力を加える 
        //     putter ball = ballInstance.GetComponent<putter>();
        //     if (ball != null && ball.rb != null)
        //     {
        //         ball.rb.AddForce(shotDirection * shotPower, ForceMode.Impulse);
        //     }
        // }

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
            // Debug.Log("kitaze");

        }
        // ボールが動いている時に速度をログに出力

        void GenerateClones()
        {
            GetComponent<AudioSource>().Play();
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
}