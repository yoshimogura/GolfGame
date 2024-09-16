using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class putter : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float shotpower=-50; 
    public Rigidbody rb;
    public string permit="false";
    float x=-1;
    float Memorization=0;
    string firstshot ="YES";

    void FixedUpdate()
    {
      
      
        
         
    }
    

    void Update()
    {
      
    // if(Input.GetKeyDown(KeyCode.Space))
    if(Input.GetKeyDown(KeyCode.Space)&&permit=="true")
      {
        permit="false";
        if(firstshot=="YES"){
         firstshot ="NO";
        }
         Rigidbody rb = GetComponent<Rigidbody>();   //取得してる
            rb.AddForce(shotpower,0,0,ForceMode.VelocityChange);
            
            
      }
      if(this.transform.position.y < 9.4)
      {
        this.transform.position = new Vector3(129, 14, -58);
      }





      Debug.Log(x-Memorization  );
      Debug.Log(permit);
      Debug.Log(firstshot);
        // permit = rb.IsSleeping() ? "true" : "false";
        if(firstshot=="NO"){
          x=this.transform.position.x;
        }
        if(x-Memorization<=0.001){
          permit="true";
        }
        if(firstshot=="NO"){
         Memorization=x;
        }
    }

}
