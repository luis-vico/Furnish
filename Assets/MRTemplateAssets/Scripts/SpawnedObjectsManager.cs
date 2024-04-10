using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets;

[RequireComponent(typeof(ObjectSpawner))]
public class SpawnedObjectsManager : MonoBehaviour
{

    [SerializeField]
    Button m_DestroyObjectsButton;

    ObjectSpawner m_Spawner;

    void OnEnable()
    {
        m_Spawner = GetComponent<ObjectSpawner>();
        m_Spawner.spawnAsChildren = true;

        m_DestroyObjectsButton.onClick.AddListener(OnDestroyObjectsButtonClicked);
    }

    void OnDisable()
    {
        m_DestroyObjectsButton.onClick.RemoveListener(OnDestroyObjectsButtonClicked);
    }

    void OnDestroyObjectsButtonClicked()
    {
        foreach (Transform child in m_Spawner.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
