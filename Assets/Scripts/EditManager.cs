using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Rendering;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets
{
    public class EditManager : MonoBehaviour{

        [SerializeField] ObjectSpawner m_ObjectSpawner;

        [SerializeField] Material furnitureMaterial;

        [SerializeField] GameObject FurniturePreset;

        public ObjectSpawner objectSpawner{
            get => m_ObjectSpawner;
            set => m_ObjectSpawner = value;
        }

        [SerializeField] GameObject editPlatform;
        [SerializeField] GameObject EditScreens;
        [SerializeField] GameObject testObj;

        void Awake(){
            editPlatform.SetActive(false);
        }

        void OnEnable(){
            SofaRuntimeController.spawnFurniture += transferToSpawner;
        }
        void OnDisable(){
            SofaRuntimeController.spawnFurniture -= transferToSpawner;
        }

        public void changePlatform(){
            var cPos = Camera.main.transform;
            editPlatform.transform.position = new Vector3(cPos.position.x, 0, cPos.position.z);
            editPlatform.transform.rotation = new Quaternion(0, cPos.rotation.y, 0, cPos.rotation.w);
            editPlatform.transform.position += editPlatform.transform.forward * 1; //1 Meter entfernt
            editPlatform.transform.RotateAround(editPlatform.transform.position, transform.up, 180f);

            EditScreens.transform.position = new Vector3(editPlatform.transform.position.x, cPos.position.y, editPlatform.transform.position.z);
            editPlatform.SetActive(true);
        }

        private void transferToSpawner(GameObject furniture){
            if (m_ObjectSpawner == null){
                Debug.LogWarning("Object Spawner not configured correctly: no ObjectSpawner set.");
            }
            else{
                furniture.AddComponent<MeshFilter>();
                furniture.AddComponent<MeshRenderer>();
                furniture.AddComponent<MeshCombiner>();
                furniture.GetComponent<MeshRenderer>().material = furnitureMaterial;
                //GameObject finishedFurniture = Instantiate(FurniturePreset, editPlatform.transform);

                //var FurnBoxCollider = finishedFurniture.GetComponent<BoxCollider>();
                //FurnBoxCollider.center = new Vector3(0,FurnBoxCollider.size.y / 2,0);
                //finishedFurniture.transform.GetChild(0).GetChild(0).GetComponent<MaterialPropertyBlockHelper>().rendererTarget = furniture.GetComponent<MeshRenderer>();

                //furniture.transform.parent = finishedFurniture.transform;
                //finishedFurniture.transform.parent = m_ObjectSpawner.transform;
                BoxCollider boxCollider = furniture.AddComponent<BoxCollider>();
                furniture.GetComponent<BoxCollider>().center = new Vector3(0,furniture.GetComponent<BoxCollider>().size.y / 2,0);
                furniture.GetComponent<BoxCollider>().size = furniture.GetComponent<Renderer>().bounds.size;

                furniture.AddComponent<Rigidbody>();
                furniture.GetComponent<Rigidbody>().isKinematic = true;

                furniture.transform.parent = m_ObjectSpawner.transform;
                m_ObjectSpawner.objToSpawn = furniture;
            }
            m_ObjectSpawner.TrySpawnObject(
                new Vector3(
                    editPlatform.transform.position.x,
                    0,
                    editPlatform.transform.position.z
                ),
                new Vector3(0,1,0)
            );
        }

    }
}
