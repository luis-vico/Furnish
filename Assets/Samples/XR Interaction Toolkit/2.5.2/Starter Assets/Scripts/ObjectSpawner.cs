using System;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Utilities;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets
{
    /// <summary>
    /// Behavior with an API for spawning objects from a given set of prefabs.
    /// </summary>
    public class ObjectSpawner : MonoBehaviour
   {
        [SerializeField]
        [Tooltip("The camera that objects will face when spawned. If not set, defaults to the main camera.")]
        Camera m_CameraToFace;

        /// <summary>
        /// The camera that objects will face when spawned. If not set, defaults to the <see cref="Camera.main"/> camera.
        /// </summary>
        public Camera cameraToFace
        {
            get
            {
                EnsureFacingCamera();
                return m_CameraToFace;
            }
            set => m_CameraToFace = value;
        }

        [SerializeField]
        [Tooltip("Whether to only spawn an object if the spawn point is within view of the camera.")]
        bool m_OnlySpawnInView = true;

        /// <summary>
        /// Whether to only spawn an object if the spawn point is within view of the <see cref="cameraToFace"/>.
        /// </summary>
        public bool onlySpawnInView
        {
            get => m_OnlySpawnInView;
            set => m_OnlySpawnInView = value;
        }

        [SerializeField]
        [Tooltip("The size, in viewport units, of the periphery inside the viewport that will not be considered in view.")]
        float m_ViewportPeriphery = 0.15f;

        /// <summary>
        /// The size, in viewport units, of the periphery inside the viewport that will not be considered in view.
        /// </summary>
        public float viewportPeriphery
        {
            get => m_ViewportPeriphery;
            set => m_ViewportPeriphery = value;
        }

        [SerializeField]
        [Tooltip("When enabled, the object will be rotated about the y-axis when spawned by Spawn Angle Range, " +
            "in relation to the direction of the spawn point to the camera.")]
        bool m_ApplyRandomAngleAtSpawn = true;

        /// <summary>
        /// When enabled, the object will be rotated about the y-axis when spawned by <see cref="spawnAngleRange"/>
        /// in relation to the direction of the spawn point to the camera.
        /// </summary>
        public bool applyRandomAngleAtSpawn
        {
            get => m_ApplyRandomAngleAtSpawn;
            set => m_ApplyRandomAngleAtSpawn = value;
        }

        [SerializeField]
        [Tooltip("The range in degrees that the object will randomly be rotated about the y axis when spawned, " +
            "in relation to the direction of the spawn point to the camera.")]
        float m_SpawnAngleRange = 45f;

        /// <summary>
        /// The range in degrees that the object will randomly be rotated about the y axis when spawned, in relation
        /// to the direction of the spawn point to the camera.
        /// </summary>
        public float spawnAngleRange
        {
            get => m_SpawnAngleRange;
            set => m_SpawnAngleRange = value;
        }

        /// <summary>
        /// Event invoked after an object is spawned.
        /// </summary>
        /// <seealso cref="TrySpawnObject"/>
        public event Action<GameObject> objectSpawned;

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        /// 

        [HideInInspector] public GameObject objToSpawn; 

        void Awake()
        {
            EnsureFacingCamera();
        }

        void EnsureFacingCamera()
        {
            if (m_CameraToFace == null)
                m_CameraToFace = Camera.main;
        }

        /// <summary>
        /// yaw rotation that faces <see cref="cameraToFace"/>, plus or minus a random angle within <see cref="spawnAngleRange"/>.
        /// </summary>
        /// <param name="spawnPoint">The world space position at which to spawn the object.</param>
        /// <param name="spawnNormal">The world space normal of the spawn surface.</param>
        /// <returns>Returns <see langword="true"/> if the spawner successfully spawned an object. Otherwise returns
        /// <see langword="false"/>, for instance if the spawn point is out of view of the camera.</returns>
        /// <remarks>
        /// Otherwise, it will spawn the prefab at the index.
        /// </remarks>
        /// <seealso cref="objectSpawned"/>
        public bool TrySpawnObject(Vector3 spawnPoint, Vector3 spawnNormal)
        {
            if (m_OnlySpawnInView)
            {
                var inViewMin = m_ViewportPeriphery;
                var inViewMax = 1f - m_ViewportPeriphery;
                var pointInViewportSpace = cameraToFace.WorldToViewportPoint(spawnPoint);
                if (pointInViewportSpace.z < 0f || pointInViewportSpace.x > inViewMax || pointInViewportSpace.x < inViewMin ||
                    pointInViewportSpace.y > inViewMax || pointInViewportSpace.y < inViewMin)
                {
                    return false;
                }
            }

            var newObject = Instantiate(objToSpawn);

            newObject.transform.SetParent(gameObject.transform);

            newObject.transform.position = spawnPoint;
            EnsureFacingCamera();
                
            var facePosition = m_CameraToFace.transform.position;
            var forward = facePosition - spawnPoint;
            BurstMathUtility.ProjectOnPlane(forward, spawnNormal, out var projectedForward);
            newObject.transform.rotation = Quaternion.LookRotation(projectedForward, spawnNormal);

            if (m_ApplyRandomAngleAtSpawn)
            {
                var randomRotation = Random.Range(-m_SpawnAngleRange, m_SpawnAngleRange);
                newObject.transform.Rotate(Vector3.up, randomRotation);
            }

            objectSpawned?.Invoke(newObject);
            return true;
        }
    }
}