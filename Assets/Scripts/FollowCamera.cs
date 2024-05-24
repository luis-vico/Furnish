using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform targetObj;
    public bool rotEnable;

    public Camera camera;

    void Update()
    {
        transform.position = new Vector3(targetObj.transform.position.x, 0, targetObj.transform.position.z);
        if(rotEnable){
            transform.rotation = new Quaternion(transform.rotation.x, targetObj.transform.rotation.y, transform.rotation.z, transform.rotation.w);
        }
        
    }

    public void changeRot(){
        if(rotEnable){
            rotEnable = false;
            transform.rotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);
        }else{
            rotEnable = true;
        }
    }

    public void sizePlus(){
        if(camera.orthographicSize <= 8){
            camera.orthographicSize = camera.orthographicSize + 0.5f;
        }    
    }
    public void sizeMinus(){
        if(camera.orthographicSize >= 1){
            camera.orthographicSize = camera.orthographicSize - 0.5f;
        } 

    }
}
