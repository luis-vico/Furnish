using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionMaterialChange : MonoBehaviour
{
    public Material furnitureMaterial;
    public Material collisionMaterial;
    public List<GameObject> collisionEnterObj = new List<GameObject>();

    void OnCollisionExit(Collision collision) 
    {
        if(furnitureMaterial != null && collision.gameObject.tag == "CollisionDetection"){
            gameObject.GetComponent<MeshRenderer>().material = furnitureMaterial;
            collisionEnterObj.Remove(collision.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "CollisionDetection" && collision.gameObject.GetComponent<CollisionMaterialChange>() != null){
            collisionEnterObj.Add(collision.gameObject);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(collisionMaterial != null && collision.gameObject.tag == "CollisionDetection"){
            gameObject.GetComponent<MeshRenderer>().material = collisionMaterial;
        }
    }

}
