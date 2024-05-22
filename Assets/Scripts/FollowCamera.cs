using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform targetObj; 
    
    void Update()
    {
        transform.position = new Vector3(targetObj.transform.position.x, 0, targetObj.transform.position.z);
    }
}
