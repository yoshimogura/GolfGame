using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clone : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectToClone;
    public GameObject objectToClone2;
    public GameObject objectToClone3;
    int numberOfClones = 10; // 生成するクローンの数
    float yPosition = 17f; // Y座標の位置
    float zPosition = -75f; // Z座標の位置


    void Start()
    {

        GenerateClones();

    }
    void Update()
    {

    }

    void GenerateClones()
    {
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