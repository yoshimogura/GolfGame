using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clone : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectToClone;
    public int numberOfClones = 10; // 生成するクローンの数
    public float yPosition = 19f; // Y座標の位置
    public float zPosition = -75f; // Z座標の位置

    void Start() {
       
        GenerateClones();
    
    }


    void GenerateClones()
    {
        for (int i = 1; i < numberOfClones; i++)
        {
            // X座標
            float xPosition = Random.Range(74f, 135f);
            float zPosition;
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
            Instantiate(objectToClone, clonePosition, Quaternion.identity);
            if(i>10){
            break;
            }
        }
    
}
}
//74 135