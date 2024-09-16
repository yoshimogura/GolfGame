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
        GameObject camera = GameObject.Find("Main Camera");
            if (camera)
            {
                
                Debug.Log("camera exist");
                camera.transform.position = new Vector3(81, 20, -57);
            }
            else
            {
                Debug.Log("camera not exist");
            }
        }else{
            GameObject camera = GameObject.Find("Main Camera");
            camera.transform.position = new Vector3(158, 32, -57);

        }


    }
    }
