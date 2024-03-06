using Cinemachine;
using UnityEngine;

public class ScreenInteraction : MonoBehaviour
{
    public Camera mainCamera; // De camera die naar het scherm kijkt
    public Camera screenCamera;
    CinemachineBrain cinemachineBrain;
    public LayerMask screenLayerMask;
    public Texture2D cursorArrow, cursorHand;

    void Start()
    {
        cinemachineBrain = FindObjectOfType<CinemachineBrain>();
        // mainCamera = cinemachineBrain.OutputCamera;

        // Cursor.visible = false;
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    void FixedUpdate()
    {

        RayCastToUI();
    }
    void RayCastToUI()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, screenLayerMask))
        {
            Debug.Log("Geraakt object: " + hit.collider.gameObject.name);
            Cursor.SetCursor(cursorHand, Vector2.zero, CursorMode.ForceSoftware);
            if (Input.GetMouseButtonDown(0)) 
            {
                // RaycastToScreen();
            }
        }
        else
        {
            Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);

        }
    }

    //logic voor andere attempt om via scherm te interacteren
    void RaycastToScreen()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.green, 200.0f); // Teken voor 2 seconden

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, screenLayerMask))
        {
            Debug.Log("Geraakt object: " + hit.collider.gameObject.name);

            // Bereken de positie op de Render Texture gebaseerd op de hit positie
            Vector2 texturePos = CalculateTexturePosition(hit.point, hit.collider.gameObject);

            // Voer een tweede raycast uit vanuit de screenCamera
            RaycastFromScreenCamera(texturePos, screenCamera);
        }
    }
    Vector2 CalculateTexturePosition(Vector3 hitPoint, GameObject screenObject)
    {
        // krijg de afmetingen van het schermobject
        MeshRenderer screenMeshRenderer = screenObject.GetComponent<MeshRenderer>();
        if (screenMeshRenderer == null) return Vector2.zero;

        Vector3 screenBounds = new Vector3(100, 100, 0);
        Debug.Log(screenMeshRenderer.bounds.size);

        // Bereken de lokale positie van de hit binnen het schermobject
        Vector3 localHitPoint = screenObject.transform.InverseTransformPoint(hitPoint);
        float localX = (localHitPoint.x + screenBounds.x / 2) / screenBounds.x;
        float localY = (localHitPoint.y + screenBounds.y / 2) / screenBounds.y;

        // Zet deze lokale positie om naar een positie op de Render Texture
        return new Vector2(localX * screenCamera.pixelWidth, localY * screenCamera.pixelHeight);
    }
    void RaycastFromScreenCamera(Vector2 screenPosition, Camera screenCamera)
    {
        Ray ray = screenCamera.ScreenPointToRay(screenPosition);
        Debug.Log(screenPosition);
        Debug.DrawLine(ray.origin, ray.direction * 100, Color.red, 200.0f);

        // Log the start position of the ray
        // Debug.Log("Ray origin: " + ray.origin);
        // Debug.Log("Ray direction: " + ray.direction);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            // Log hit information
            Debug.Log("Raycast hit: " + hit.transform.position);
        }
        else
        {
            // Debug.Log("No hit");
        }
    }

}

