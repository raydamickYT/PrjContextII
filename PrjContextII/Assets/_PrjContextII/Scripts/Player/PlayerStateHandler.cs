using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerStateHandler : MonoBehaviour
{
    public Rigidbody rb;
    public Animator anim;
    private readonly FSM<PlayerStateHandler> fsm = new();

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        SetupStates();
    }

    void Update()
    {
        fsm.OnUpdate();
    }

    void SetupStates()
    {
        fsm.AddState(new PlayerMovement(rb, anim, this));
        fsm.AddState(new PlayerMovementFree(rb, anim, this));
        fsm.AddState(new ComputerInteract(rb, anim, this));
        fsm.SwitchState(typeof(PlayerMovement));
    }

    public void SwitchPlayerState(System.Type newState)
    {
        fsm.SwitchState(newState);
    }
}
