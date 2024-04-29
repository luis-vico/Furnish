using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Rendering;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using UnityEngine.XR.Interaction.Toolkit.Samples.Hands;


namespace UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]


    public class MeshCombiner : MonoBehaviour
    {
        public GameObject interactionAffordance;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Combining Meshes");

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
            Debug.Log("Meshes Combined");

            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
            boxCollider.size = gameObject.GetComponent<MeshFilter>().mesh.bounds.size;
            boxCollider.center = new Vector3(0, boxCollider.size.y/2 ,0);
            Debug.Log("BoxCollider added");

            gameObject.AddComponent<Rigidbody>();
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            Debug.Log("Rigidbody added");

            gameObject.AddComponent<XRGrabInteractable>();
            var xrGrab = gameObject.GetComponent<XRGrabInteractable>();
            xrGrab.useDynamicAttach = true;
            xrGrab.trackRotation = true;
            xrGrab.trackScale = false;
            Debug.Log("XRGrabInteractable added");

            gameObject.AddComponent<XRGeneralGrabTransformer>();
            XRGeneralGrabTransformer.ManipulationAxes axes = XRGeneralGrabTransformer.ManipulationAxes.All;
            axes = XRGeneralGrabTransformer.ManipulationAxes.X | XRGeneralGrabTransformer.ManipulationAxes.Z;
            gameObject.GetComponent<XRGeneralGrabTransformer>().permittedDisplacementAxes = axes;
            Debug.Log("XRGeneralGrabTransformer added");

            gameObject.AddComponent<GrabTransformerRotationAxisLock>();
            gameObject.GetComponent<GrabTransformerRotationAxisLock>().SetPermittedRotationAxes(XRGeneralGrabTransformer.ManipulationAxes.Y);
            Debug.Log("GrabTransformerRotationAxisLock added");
            
            
            while(interactionAffordance == null){
                Debug.Log("wait");
            }
            GameObject iAffordance = Instantiate(interactionAffordance, gameObject.transform);
            iAffordance.transform.GetChild(0).GetComponent<MaterialPropertyBlockHelper>().rendererTarget = gameObject.GetComponent<MeshRenderer>();
            iAffordance.SetActive(true);
            Debug.Log("InteractionAffordance added");

        }


    }
}