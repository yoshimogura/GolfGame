using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetterMove : MonoBehaviour
{
  public float speed = 5.0f; // 振り子の速度
  public float maxAngle = 90.0f; // 振り子の最大角度 
  private ConfigurableJoint joint;
  void Start()
  {
    joint = GetComponent<ConfigurableJoint>();
    JointDrive drive = new JointDrive { positionSpring = 1000, positionDamper = 100, maximumForce = Mathf.Infinity };
    joint.angularXDrive = drive;
  }
  void Update()
  {
    float angle = maxAngle * Mathf.Sin(Time.time * speed);
    joint.targetRotation = Quaternion.Euler(angle, 0, 0);
  }
}