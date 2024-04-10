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

        [SerializeField] GameObject testObj;

        public void SofaEdit(){
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
