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
        base.OnEnter();
        // Cursor.visible = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            fsm.SwitchState(typeof(PlayerMovement));
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        Cursor.visible = true;
    }
}
