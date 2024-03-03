using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    // Een array om de materialen te houden die je wilt kunnen toewijzen.
    public Material[] materials;

    public void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            // ChangeMaterial(Random(0,2));
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
        }
        else
        {
            Debug.LogError("Material index out of range!");
        }
    }
}
