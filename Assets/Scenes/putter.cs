using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class putter : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float shotpower=-50; 
    public Rigidbody rb;
    string permit="false";



    void Start(){
      
    }
    void FixedUpdate()
    {
      
      permit = rb.IsSleeping() ? "true" : "false";
        
         
    }
    

    void Update()
    {
    if(Input.GetKeyDown(KeyCode.Space)&&permit=="true")
      {
         Rigidbody rb = GetComponent<Rigidbody>();   //取得してる
            rb.AddForce(shotpower,0,0,ForceMode.VelocityChange);
            
            
      }
      if(this.transform.position.y < 9.4)
      {
        this.transform.position = new Vector3(129, 14, -58);
      }





    
    }

}
