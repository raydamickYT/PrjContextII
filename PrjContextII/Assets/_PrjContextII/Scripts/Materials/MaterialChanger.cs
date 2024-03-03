using UnityEngine;

public class MaterialChanger 
{
    // Een array om de materialen te houden die je wilt kunnen toewijzen.
    public Material[] materials;

    // Functie om het materiaal van het GameObject te veranderen.
    public void ChangeMaterial(int materialIndex, Renderer Building)
    {
        // Controleert of de index geldig is.
        if (materialIndex < materials.Length && materialIndex >= 0)
        {
            // Verandert het materiaal van het GameObject.
            Building.material = materials[materialIndex];
            Debug.Log(materialIndex);
        }
        else
        {
            Debug.LogError("Material index out of range!");
        }
    }
}
