using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerStateHandler : MonoBehaviour
{
    public Rigidbody rb;
    public Animator anim;
    public Camera mainCam;
    public LayerMask computerLayermask;
    private readonly FSM<PlayerStateHandler> fsm = new();

    public PlayerSettings PS;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        mainCam = GetComponentInChildren<Camera>();
        FillScripteableObject();
        if (mainCam == null)
        {
            Debug.LogWarning("camera is niet gevonden in speler");
        }

        SetupStates();
    }

    void FillScripteableObject(){
        PS.rb = rb;
        PS.MainCam = mainCam;
        PS.anim = anim;
    }

    void Update()
    {

        fsm.OnUpdate();
    }

    void LateUpdate()
    {
        fsm.OnLateUpdate();
    }

    void SetupStates()
    {
        fsm.AddState(new PlayerMovement(PS, this));
        fsm.AddState(new PlayerMovementFree(this, PS));
        fsm.AddState(new ComputerInteract(PS, this));
        fsm.SwitchState(typeof(PlayerMovement));
    }

    public void SwitchPlayerState(System.Type newState)
    {
        fsm.SwitchState(newState);
    }
}
