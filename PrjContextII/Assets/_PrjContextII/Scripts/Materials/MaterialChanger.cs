using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MaterialChanger : MonoBehaviour
{
    // Een array om de materialen te houden die je wilt kunnen toewijzen.
    public Material[] materials; //1 = goed, 2 = minder, 3= slecht
    List<GameObject> childGameObjects = new List<GameObject>();
    private Renderer test;


    private void Start()
    {
        // Debug.Log("test");
        if (MaterialManager.Instance != null)
        {
            MaterialManager.Instance.SubscribeToAction(ChangeMaterial);
        }
        else
        {
            Debug.Log("materialmanager bestaat no niet ");
        }


        GetAllBuildings();
    }

    private void OnDisable()
    {
        MaterialManager.Instance.SubscribeToAction(ChangeMaterial);
    }

    private void GetAllBuildings()
    {
        Transform[] childTransforms = GetComponentsInChildren<Transform>(false); // 'true' om ook inactieve GameObjects op te nemen

        foreach (Transform childTransform in childTransforms)
        {
            childGameObjects.Add(childTransform.gameObject);
        }
    }


    public void ChangeMaterial(int materialIndex)
    {
        // Controleert of de index geldig is.
        if (materialIndex < materials.Length && materialIndex >= 0)
        {
            foreach (GameObject child in childGameObjects)
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    child.GetComponent<Renderer>().material = materials[materialIndex];
                }
            }
            // Verandert het materiaal van het GameObject.
            Debug.Log(materialIndex);
        }
        else
        {
            Debug.Log("material index: " + materialIndex);

            Debug.LogError("Material index out of range!");
        }
    }
}
