using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour{

    [SerializeField] GameObject FurnitureSpawner;
    [SerializeField] GameObject TestObj;
    FurnitureSpawner fSpawnerScript;

    void Awake(){
        fSpawnerScript = FurnitureSpawner.GetComponent<FurnitureSpawner>();
    }

    public void SpawnTest(){
        fSpawnerScript.TrySpawnObject(GetSpawnPosition(), new Vector3(0,0,1), TestObj);
    }

    Vector3 GetSpawnPosition(){
        var cameraPos = Camera.main.transform.position;
        return cameraPos;
    }

}
