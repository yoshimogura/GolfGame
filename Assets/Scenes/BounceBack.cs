// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class BounceBack : MonoBehaviour
// {
//     // Start is called before the first frame update
//     private void OnCollosionEnter(Collision collision){
//         if(collision.gameObject.CompareTag("ボール（1)")){
//             Rigidbody ball=collision.rigidbody;
//             if(ball!=null){
//                 ContactPoint contactPoint =collision.GetContact(0);
//                 ball.velocity=Vector3.Reflect(collision.relativeVelocity,contactPoint.normal);
//             }
//         }
//     }
// }
