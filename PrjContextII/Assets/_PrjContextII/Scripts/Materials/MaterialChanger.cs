using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    // Een array om de materialen te houden die je wilt kunnen toewijzen.
    public Material[] materials;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeMaterial(Random.Range(0,2));
            Debug.Log("test");
        }
    }

    // Functie om het materiaal van het GameObject te veranderen.
    public void ChangeMaterial(int materialIndex)
    {
        // Controleert of de index geldig is.
        if (materialIndex < materials.Length && materialIndex >= 0)
        {
            // Verandert het materiaal van het GameObject.
            GetComponent<Renderer>().material = materials[materialIndex];
            Debug.Log(materialIndex);
        }
        else
        {
            Debug.LogError("Material index out of range!");
        }
    }
}
