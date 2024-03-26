using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMovementState : State
{
    private FSM<State> fsm;

    public DisableMovementState(FSM<State> _fsm) : base(_fsm)
    {
        fsm = _fsm;
    }

    public override void OnEnter()
    {
        PS.Anim.SetTrigger("ZoomIntoComp");
        // Cursor.visible = false;
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            fsm.SwitchState(typeof(PlayerMovement));
            PS.Anim.SetTrigger("ZomOutOfComp");
        }
    }

    public override void OnExit()
    {
        Debug.Log("exiting");
        Cursor.visible = true;
    }
}
