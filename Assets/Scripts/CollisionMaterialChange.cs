using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionMaterialChange : MonoBehaviour
{
    public Material furnitureMaterial;
    public Material collisionMaterial;

    private void OnCollisionExit(Collision collision) 
    {
        if(furnitureMaterial != null && collision.gameObject.tag == "CollisionDetection"){
            gameObject.GetComponent<MeshRenderer>().material = furnitureMaterial;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(collisionMaterial != null && collision.gameObject.tag == "CollisionDetection"){
            gameObject.GetComponent<MeshRenderer>().material = collisionMaterial;
        }
    }

}