using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class putter : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float shotpower=-1; 
    public Rigidbody rb;
    bool moving=false;



    void Start(){
      
      rb = GetComponent<Rigidbody>();   //取得してる
    }
    void FixedUpdate()
    {
      
     
        if(rb.IsSleeping()){
        moving=true;
      }else{
        moving=false;
      
      }
    }
    

    void Update()
    {
    if(Input.GetKeyDown(KeyCode.Space)&&moving==!false)
      {
        //  Rigidbody rb = GetComponent<Rigidbody>();   //取得してる
            rb.AddForce(shotpower,0,0,ForceMode.VelocityChange);
            
            
      }
      if(this.transform.position.y < 9.4)
      {
        this.transform.position = new Vector3(129, 14, -58);
      }





    
    }

}
