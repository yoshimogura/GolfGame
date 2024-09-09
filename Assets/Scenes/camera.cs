using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject boll;
    void Start()
    {
        this.boll=GameObject.Find("ボール (1)");
        
    }
    // Update is called once per frame
    void Update()
    {
        if(-14>this.boll.transform.position.x){

            Debug.Log("camera");
        }


    }
    }
