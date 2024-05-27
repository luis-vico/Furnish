using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform targetObj;
    public bool rotEnable;

    public Camera camera;

    public GameObject arrow;

    void Update()
    {
        transform.position = new Vector3(targetObj.transform.position.x, 0, targetObj.transform.position.z);

        if(rotEnable){
            transform.rotation = new Quaternion(transform.rotation.x, targetObj.transform.rotation.y, transform.rotation.z, targetObj.transform.rotation.w);

        }
        
    }

    public void changeRot(){
        if(rotEnable){
            rotEnable = false;
        }else{
            rotEnable = true;
            //arrow.transform.rotation = new Quaternion(90, 0, 45, arrow.transform.rotation.w);
        }
    }

    public void sizePlus(){
        if(camera.orthographicSize <= 8){
            camera.orthographicSize = camera.orthographicSize + 0.5f;
            arrow.transform.localScale = new Vector3(camera.orthographicSize/28, camera.orthographicSize/28, camera.orthographicSize/28);
        }    
    }
    public void sizeMinus(){
        if(camera.orthographicSize >= 1){
            camera.orthographicSize = camera.orthographicSize - 0.5f;
            arrow.transform.localScale = new Vector3(camera.orthographicSize/28, camera.orthographicSize/28, camera.orthographicSize/28);
        } 

    }
}
