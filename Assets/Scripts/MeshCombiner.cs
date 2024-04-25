using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class MeshCombiner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I got run");

        Vector3 oldPos = transform.position;
        Quaternion oldRot = transform.rotation;

        transform.position = new Vector3(0,0,0);
        transform.rotation = new Quaternion(0,0,0,0);

        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }

        for (int a = 0; a < transform.childCount; a++){
            Destroy(transform.GetChild(a).gameObject);
        }

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combine, true, true, false);
        transform.GetComponent<MeshFilter>().sharedMesh = mesh;

        transform.position = oldPos;
        transform.rotation = oldRot;


        transform.gameObject.SetActive(true);
    }


}
