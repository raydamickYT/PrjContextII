using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    private List<GameObject> Buildings = new();
    public GameObject[] BuildingVariants;

    // Start is called before the first frame update
    void Start()
    {
        if (MaterialManager.Instance != null)
        {
            MaterialManager.Instance.SubscribeToAction(ReplaceBuildingMeshes);
        }
        else
        {
            Debug.Log("materialmanager bestaat nog niet ");
        }

        // Buildings = gameObject.GetComponentsInChildren<GameObject>();
        GetAllBuildings();
    }
    private void GetAllBuildings()
    {
        Transform[] childTransforms = GetComponentsInChildren<Transform>(false); // 'true' om ook inactieve GameObjects op te nemen

        foreach (Transform childTransform in childTransforms)
        {
            Buildings.Add(childTransform.gameObject);
        }
    }

    void ReplaceBuildingMeshes(int PrefabIndex)
    {
        // Verkrijg de Renderer componenten van het huidige gebouw en het prefab
        Renderer[] currentRenderers = this.gameObject.GetComponentsInChildren<MeshRenderer>();
        if (PrefabIndex < BuildingVariants.Length)
        {
            Renderer[] prefabRenderers = BuildingVariants[PrefabIndex].GetComponentsInChildren<MeshRenderer>();

            // Loop door elke Renderer in het huidige gebouw
            for (int i = 0; i < currentRenderers.Length; i++)
            {
                // Zorg ervoor dat er een overeenkomende Renderer is in het prefab om de materialen van te kopiëren
                if (i < prefabRenderers.Length)
                {
                    // Vervang de materialen van de huidige Renderer door die van de overeenkomstige Renderer in het prefab
                    // Dit kopieert alle materialen (nuttig voor objecten met meerdere materialen)
                    currentRenderers[i].sharedMaterials = prefabRenderers[i].sharedMaterials;
                }
            }
        }
        else
        {
            Debug.LogWarning(this.gameObject.name + " GameObject is out of bounds. Index: " + PrefabIndex + "buildingvariants length" + BuildingVariants.Length);
        }
    }


}
