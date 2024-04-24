using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets
{
    public class EditManager : MonoBehaviour{

        [SerializeField] ObjectSpawner m_ObjectSpawner;

        [SerializeField] GameObject SpawnTrigger;

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
