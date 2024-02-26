using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerInteract : State
{
    private PlayerStateHandler PlayerFSM;
    private string stateName = "Sit";
    private PlayerSettings PS;


    public ComputerInteract(PlayerSettings _ps, PlayerStateHandler _fsm)
    {
        PS = _ps;
        PlayerFSM = _fsm;
    }

    public override void OnEnter()
    {
        PS.anim.SetTrigger("AwayFromComp");
    }
    public override void OnUpdate()
    {
        AnimatorStateInfo stateInfo = PS.anim.GetCurrentAnimatorStateInfo(0); // 0 verwijst naar de eerste laag van de Animator

        if (stateInfo.IsName(stateName))
        {
            if (stateInfo.normalizedTime > 1)
            {
                PlayerFSM.SwitchPlayerState(typeof(PlayerMovementFree));
            }
            // Print de huidige tijd van de animatie naar de console
            Debug.Log($"Animatie {stateName} tijd: {stateInfo.normalizedTime}");
        }
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}

