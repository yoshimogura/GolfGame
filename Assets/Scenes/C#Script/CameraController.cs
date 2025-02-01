using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetPosition(Vector3 newPosition, Transform target)
    {
        // カメラの新しい位置を設定
        transform.position = newPosition;
        // カメラがターゲットを向くようにする
        transform.LookAt(target);
    }
}
