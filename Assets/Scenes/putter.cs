using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class putter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    Transform myTransform = this.transform;
    Vector3 worldAngle = myTransform.eulerAngles;
         
    if(Input.GetKeyDown(KeyCode.Space))
      {
       transform.Rotate(new Vector3(100,0,0));
      }
    }

}
