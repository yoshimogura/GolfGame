using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject boll;
    void Start(){
        GameObject camera = GameObject.Find("Main Camera");
        camera.transform.position = new Vector3(158, 32, -57);
        
    }
    // Update is called once per frame
    void Update()
    {   
         this.boll=GameObject.Find("ボール (1)");
        if(90>this.boll.transform.position.x){
            GameObject camera = GameObject.Find("Main Camera");        
            Debug.Log("camera exist");
            camera.transform.position = new Vector3(100, 20, -59);
            
        }


    }
    }
