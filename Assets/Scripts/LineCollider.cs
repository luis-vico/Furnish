using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation
{
    
    public class LineCollider : MonoBehaviour
    {
        GameObject[] planeArray;
        public GameObject BorderColliderPrefab;

        void Start(){

            StartCoroutine(transferMesh());
        }

        IEnumerator transferMesh(){
            yield return new WaitForSeconds(5);
            planeArray = GameObject.FindGameObjectsWithTag("ARPlane");
            foreach(GameObject obj in planeArray){
                GameObject BorderCollider = Instantiate(BorderColliderPrefab, obj.transform);
                BorderCollider.GetComponent<LineRenderer>().positionCount  = obj.GetComponent<LineRenderer>().positionCount;
                for(int i = 0; i < obj.GetComponent<LineRenderer>().positionCount; i++){
                    BorderCollider.GetComponent<LineRenderer>().SetPosition(i, obj.GetComponent<LineRenderer>().GetPosition(i));
                }
                Mesh lineMesh = new Mesh();
                BorderCollider.GetComponent<LineRenderer>().BakeMesh(lineMesh);
                //obj.GetComponent<LineRenderer>().BakeMesh(lineMesh);
                BorderCollider.GetComponent<MeshCollider>().sharedMesh = lineMesh;
            }
        } 

    }
}
