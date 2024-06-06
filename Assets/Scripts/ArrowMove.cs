using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    public Transform targetObj;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(targetObj.transform.position.x, 0, targetObj.transform.position.z);
        transform.rotation = new Quaternion(transform.rotation.x, targetObj.transform.rotation.y, transform.rotation.z, targetObj.transform.rotation.w); 
    }
}
