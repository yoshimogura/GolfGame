using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject boll;
    void Start(){
        GameObject camera = GameObject.Find("Main Camera");
        camera.transform.position = new Vector3(151, 30, -62);
        
    }
    // Update is called once per frame
    void Update()
    {   
         this.boll=GameObject.Find("ボール（1)");
        if(95>this.boll.transform.position.x){
            GameObject camera = GameObject.Find("Main Camera");        
            // Debug.Log("camera exist");
            camera.transform.position = new Vector3(148, 25, -61);
            
        }


    }
    }
