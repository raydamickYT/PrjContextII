using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteract : State
{
    private PlayerStateHandler PlayerFSM;
    private string stateName = "Sit";


    public ComputerInteract(PlayerStateHandler _fsm, FSM<State> _fSM) : base(_fSM)
    {
        PlayerFSM = _fsm;
    }

    public override void OnEnter()
    {
        PS.Anim.SetTrigger("AwayFromComp");
    }
    public override void OnUpdate()
    {
        //voor als de speler wilt opstaan.
        AnimatorStateInfo stateInfo = PS.Anim.GetCurrentAnimatorStateInfo(0); // 0 verwijst naar de eerste laag van de Animator

        if (stateInfo.IsName(stateName))
        {
            if (stateInfo.normalizedTime > 1)
            {
                PlayerFSM.SwitchPlayerState(typeof(PlayerMovementFree));
            }
            // Print de huidige tijd van de animatie naar de console
            // Debug.Log($"Animatie {stateName} tijd: {stateInfo.normalizedTime}");
        }
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}

