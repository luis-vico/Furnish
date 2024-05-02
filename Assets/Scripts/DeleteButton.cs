using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject parentObj;
    BoxCollider boxCollider;
    void Awake()
    {
        parentObj = transform.parent.gameObject;
        boxCollider = parentObj.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(boxCollider != null){
            transform.position = new Vector3(transform.position.x, boxCollider.size.y + 0.02f, transform.position.z);
        }
        
    }

    public void deleteParent(){
        Destroy(parentObj);
    }
}
