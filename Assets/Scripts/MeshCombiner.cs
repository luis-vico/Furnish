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
        public GameObject deleteButton;
        public Material collisionMaterial;
        public Material furnitureMaterial;

        bool CR_running = false;

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
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
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
            
            
            while(interactionAffordance == null || deleteButton == null || collisionMaterial == null || furnitureMaterial == null){
                Debug.Log("wait");
            }
            GameObject iAffordance = Instantiate(interactionAffordance, gameObject.transform);
            iAffordance.transform.GetChild(0).GetComponent<MaterialPropertyBlockHelper>().rendererTarget = gameObject.GetComponent<MeshRenderer>();
            iAffordance.SetActive(true);
            Debug.Log("InteractionAffordance added");

            GameObject dButton = Instantiate(deleteButton, gameObject.transform);
            Debug.Log("DeleteButton added");

            xrGrab.hoverEntered.AddListener((e) => StartCoroutine(dButtonFade(dButton)));
            //xrGrab.hoverExited.AddListener((e) => dButton.SetActive(false));

            gameObject.AddComponent<CollisionMaterialChange>();
            gameObject.GetComponent<CollisionMaterialChange>().collisionMaterial = collisionMaterial;
            gameObject.GetComponent<CollisionMaterialChange>().furnitureMaterial = furnitureMaterial;
            Debug.Log("CollisionMaterialChange added");

        }

        IEnumerator dButtonFade(GameObject button){
            CanvasGroup cgroup = button.GetComponent<CanvasGroup>();
            if(CR_running){
                yield break;
            }
            CR_running = true;
            button.SetActive(true);
            float fadeSpeed = 2;
            cgroup.alpha = 1;
            float alphaValue = cgroup.alpha;
            yield return new WaitForSeconds(2.5f);
            while (cgroup.alpha > 0f)
            {
                alphaValue -= Time.deltaTime / fadeSpeed;
                cgroup.alpha = alphaValue;
                yield return null;
            }
            button.SetActive(false);
            CR_running = false;
            yield return 0;
        }


    }
}
