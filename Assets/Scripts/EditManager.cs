using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets
{
    public class EditManager : MonoBehaviour{

        [SerializeField] ObjectSpawner m_ObjectSpawner;

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

        public void SofaEdit(){
            changePlatform(0); //Sofa
            //SetObjectToSpawn(testObj);
        }

        void changePlatform(int editNum){
            var cPos = Camera.main.transform;
            editPlatform.transform.position = new Vector3(cPos.position.x, 0, cPos.position.z);
            editPlatform.transform.rotation = new Quaternion(0, cPos.rotation.y, 0, cPos.rotation.w);
            editPlatform.transform.position += editPlatform.transform.forward * 1; //1 Meter entfernt

            EditScreens.transform.position = new Vector3(editPlatform.transform.position.x, cPos.position.y, editPlatform.transform.position.z);
            editPlatform.SetActive(true);

            switch(editNum){
                case 0: //Sofa
                    break;
                case 1: //Tisch
                    break;
                case 2: //Stuhl
                    break;
                case 3: //Regal
                    break;
            }
        }

        public void SetObjectToSpawn(GameObject furniture)
        {
            if (m_ObjectSpawner == null){
                Debug.LogWarning("Object Spawner not configured correctly: no ObjectSpawner set.");
            }
            else{
                m_ObjectSpawner.objToSpawn = furniture;
            }
        }

    }
}
