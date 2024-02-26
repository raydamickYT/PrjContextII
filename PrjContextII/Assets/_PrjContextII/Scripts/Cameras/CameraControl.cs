using UnityEngine;
using Cinemachine;

public class FreeLookCameraControl : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    private Camera myCamera;
    public float sensitivityX = 0.1f;
    public float sensitivityY = 0.1f;

    private void Awake()
    {
        myCamera = GetComponent<Camera>();

        if (freeLookCamera == null)
        {
            freeLookCamera = GetComponent<CinemachineFreeLook>();
        }
    }
    private void Update()
    {
        if (Input.GetMouseButton(1)) // Rechtermuisknop is ingedrukt
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

            // Pas de FreeLook camera assen aan met de muisinvoer
            freeLookCamera.m_XAxis.Value += mouseX;
            // Bereken de nieuwe potentiële Y-as waarde
            float newYValue = freeLookCamera.m_YAxis.Value + mouseY;

            // Zorg ervoor dat de nieuwe Y-as waarde binnen de grenzen van 0 en 1 blijft
            newYValue = Mathf.Clamp(newYValue, 0f, 1f);

            // Pas de Y-as van de FreeLook camera aan met de beperkte waarde
            freeLookCamera.m_YAxis.Value = newYValue;
        }
    }


    void LateUpdate()
    {
        if (freeLookCamera != null && myCamera != null)
        {
            // Synchroniseer de positie
            myCamera.transform.position = freeLookCamera.transform.position;

            // Synchroniseer de rotatie
            myCamera.transform.rotation = freeLookCamera.State.FinalOrientation;
        }
    }
}
