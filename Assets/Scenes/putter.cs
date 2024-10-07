using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class putter : MonoBehaviour
{

    public float forceAmount = 8f; // ボールに与える力の量
    public float speedThreshold = 6f; // ボールを止めるための速度の閾値
　
    private Rigidbody rb;
    // Start is called before the first frame update
    
    public float shotpower=-1; 

    public bool logging;
    public bool move=false;

  


    void Start(){
        rb = GetComponent<Rigidbody>();

        // 最初に力を加える
        
        Debug.Log(transform.forward);
    }
    

    void Update()
    {
      if(move){
        if(logging){
          Debug.Log("magnitude:"+rb.velocity.magnitude);
        }
      }
      if(Input.GetKeyDown(KeyCode.Space))
      {
         rb.AddForce(transform.forward * forceAmount, ForceMode.Impulse);
         move=true;
      }
    // ボールの速度が閾値を下回ったら
        if (rb.velocity.magnitude < speedThreshold&&move)
        {
            // ボールを完全に止める
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Rigidbodyの物理計算を停止して完全に静止させる
            rb.isKinematic = true;
            Debug.Log("stop");
        }
      if(this.transform.position.y < 10)
      {
        this.transform.position = new Vector3(129, 15, -58);
      }
    
    
    }
}
