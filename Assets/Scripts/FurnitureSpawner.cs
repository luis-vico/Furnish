using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Utilities;

public class FurnitureSpawner : MonoBehaviour {

    [SerializeField] Camera m_CameraToFace;

    public Camera cameraToFace{
        get{
            EnsureFacingCamera();
            return m_CameraToFace;
        }
        set => m_CameraToFace = value;
    }

    /// Whether to only spawn an object if the spawn point is within view of the <see cref="cameraToFace"/>.

    [SerializeField] bool m_OnlySpawnInView = true;

    public bool onlySpawnInView {
        get => m_OnlySpawnInView;
        set => m_OnlySpawnInView = value;
    }

    /// The size, in viewport units, of the periphery inside the viewport that will not be considered in view.
    [SerializeField] float m_ViewportPeriphery = 0.15f;

    public float viewportPeriphery{
        get => m_ViewportPeriphery;
        set => m_ViewportPeriphery = value;
    }

    /// Whether to spawn each object as a child of this object.
    [SerializeField] bool m_SpawnAsChildren;

    public bool spawnAsChildren{
        get => m_SpawnAsChildren;
        set => m_SpawnAsChildren = value;
    }

    /// Event invoked after an object is spawned.
    public event Action<GameObject> objectSpawned;

    void Awake(){
        EnsureFacingCamera();
    }

    void EnsureFacingCamera(){
        if (m_CameraToFace == null)
            m_CameraToFace = Camera.main;
    }


        /// <param name="spawnPoint">The world space position at which to spawn the object.</param>
        /// <param name="spawnNormal">The world space normal of the spawn surface.</param>
        /// <returns>Returns <see langword="true"/> if the spawner successfully spawned an object. Otherwise returns
        /// <see langword="false"/>, for instance if the spawn point is out of view of the camera.</returns>
        /// <remarks>
        /// The object selected to spawn is based on <see cref="spawnOptionIndex"/>. If the index is outside
        /// the range of <see cref="objectPrefabs"/>, this method will select a random prefab from the list to spawn.
        /// Otherwise, it will spawn the prefab at the index.
        /// </remarks>
        /// <seealso cref="objectSpawned"/>
    public bool TrySpawnObject(Vector3 spawnPoint, Vector3 spawnNormal, GameObject furniture)
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

        var newObject = Instantiate(furniture);
        if (m_SpawnAsChildren)
            newObject.transform.parent = transform;

        newObject.transform.position = spawnPoint;
        EnsureFacingCamera();
                
        var facePosition = m_CameraToFace.transform.position;
        var forward = facePosition - spawnPoint;
        BurstMathUtility.ProjectOnPlane(forward, spawnNormal, out var projectedForward);
        newObject.transform.rotation = Quaternion.LookRotation(projectedForward, spawnNormal);

        objectSpawned?.Invoke(newObject);
        return true;
    }

    public void DeleteFurniture(){
        while (transform.childCount > 0) {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
    public void DeleteFurniture(GameObject furniture){
        DestroyImmediate(furniture);
    }

}
