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
        [SerializeField] GameObject testObj;



        public void SofaEdit(){
            var cPos = Camera.main.transform;
            //editPlatform.transform.position = cPos.position;
            editPlatform.transform.position = new Vector3(cPos.position.x, 0, cPos.position.z);
            editPlatform.transform.rotation = new Quaternion(0, cPos.rotation.y, 0, cPos.rotation.w);
            editPlatform.transform.position += editPlatform.transform.forward * 2;
            //editPlatform.transform.position = new Vector3(editPlatform.transform.position.x, 0, editPlatform.transform.position.z);
            Instantiate(editPlatform);
            SetObjectToSpawn(testObj);
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
