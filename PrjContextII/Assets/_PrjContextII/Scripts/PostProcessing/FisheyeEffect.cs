using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class FisheyeEffect : MonoBehaviour
{
    public Material fisheyeMaterial;
    public float bulgeAmount = 0.5f;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (fisheyeMaterial != null)
        {
            //update de shader parameters
            fisheyeMaterial.SetFloat("_BulgeAmount", bulgeAmount);

            // pas de shader toe
            Graphics.Blit(source, destination, fisheyeMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
