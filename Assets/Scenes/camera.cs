using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    // Start is called before the first frame update
float x=10;
    void Start()
    {
       Vector3 tmp = GameObject.Find("ボール (1)").transform.position;
        x = tmp.x;
    }
    // Update is called once per frame
    void Update()
    {
        if(20>x){
            Debug.Log("kamera");
        }

    }
    }
