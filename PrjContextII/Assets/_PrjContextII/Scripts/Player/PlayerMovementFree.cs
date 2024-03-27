using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementFree : State
{
    // private Camera MainCam;
    private PlayerStateHandler PlayerFSM;
    private float speed;


    public PlayerMovementFree(PlayerStateHandler _fsm, FSM<State> _fSM) : base(_fSM)
    {
        PlayerFSM = _fsm;
    }

    public override void OnEnter()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Debug.Log("test");
        PS.Anim.enabled = false;
        speed = PS.Speed;
        PS.Screen.SetActive(false);
    }
    public override void OnUpdate()
    {
        //move
        movement();
    }
    public override void OnLateUpdate()
    {
        CheckSurroundsingsWithSphereCast();
    }
    public override void OnExit()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PS.Anim.enabled = true;
        PS.Screen.SetActive(true);
    }

    public void CheckSurroundsingsWithSphereCast()
    {
        float radius = 1f;
        float maxDist = 1f;
        Vector3 direction = Vector3.forward;

        RaycastHit hitInfo;
        bool hit = Physics.SphereCast(PS.rb.transform.position, radius, direction, out hitInfo, maxDist, PS.ComputerLayerMask);

        if (hit)
        {
            if (((1 << hitInfo.collider.gameObject.layer) & PS.ComputerLayerMask) != 0)
            {
                // Debug.Log("hij ziet de computer");
                PlayerFSM.PopUpText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PS.Anim.enabled = true;
                    PS.Anim.SetTrigger("AwayFromComp");

                    PlayerFSM.SwitchPlayerState(typeof(PlayerMovement));
                    PlayerFSM.PopUpText.SetActive(false);
                }
            }
        }
        else
        {
            PlayerFSM.PopUpText.SetActive(false);
        }
    }

    public void movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Krijg de richting vooruit en rechts gebaseerd op de camera's oriëntatie
        Vector3 forward = PS.MainCam.transform.forward;
        Vector3 right = PS.MainCam.transform.right;

        // Zorg ervoor dat de beweging niet beïnvloed wordt door de hoogte van de camera
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward * moveVertical + right * moveHorizontal) * speed;

        // Pas de snelheid van de Rigidbody aan op basis van de berekende richting
        PS.rb.velocity = new Vector3(movement.x, PS.rb.velocity.y, movement.z); // Behoud de huidige verticale snelheid (bijvoorbeeld voor zwaartekracht)
    }
}
