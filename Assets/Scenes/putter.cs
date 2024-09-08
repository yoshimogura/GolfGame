using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class putter : MonoBehaviour
{
    // Start is called before the first frame update
    public float shotpower=-50; 
    
    void Start()
    {
      
    
    }

    // Update is called once per frame
    void Update()
    {
    if(Input.GetKeyDown(KeyCode.Space))
      {
         Rigidbody rb = GetComponent<Rigidbody>();   //取得してる
            rb.AddForce(shotpower,0,0,ForceMode.VelocityChange);
      }
      if(this.transform.position.y < 9.4)
      {
        this.transform.position = new Vector3(38, 14, -58);
      }

    }

}
