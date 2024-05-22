using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Transform targetObj; 
    void Update()
    {
        transform.LookAt(targetObj);
        transform.rotation = new Quaternion(transform.rotation.x - 90, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }
}
