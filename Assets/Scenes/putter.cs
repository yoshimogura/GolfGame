using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class putter : MonoBehaviour
{
    // Start is called before the first frame update
    float shotpower=90; 
    public Vector3 torque = new Vector3(5f, 0f, 0f);
    void Start()
    {
      
    
    }

    // Update is called once per frame
    void Update()
    {
    if(Input.GetKeyDown(KeyCode.Space))
      {
         Rigidbody rb = GetComponent<Rigidbody>();   //取得してる
            rb.AddForce(torque,ForceMode.Acceleration);
      }
    }

}
