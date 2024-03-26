using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMovementForCompState : State
{
    private FSM<State> fsm;

    public DisableMovementForCompState(FSM<State> _fsm) : base(_fsm)
    {
        fsm = _fsm;
    }

    public override void OnEnter()
    {
        PS.Anim.SetTrigger("ZoomIntoComp");
        if (computerManager.loginState.IsActive)
        {
            Cursor.visible = false;
        }
        Cursor.SetCursor(PS.ComputerArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!computerManager.loginState.IsActive)
            {
                ComputerManager.instance.SwitchComputerState(typeof(HomeScreenState), null);
            }
            fsm.SwitchState(typeof(PlayerMovement));
            PS.Anim.SetTrigger("ZomOutOfComp");
        }
    }

    public override void OnExit()
    {
        Cursor.SetCursor(PS.CursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        Debug.Log("exiting");
        Cursor.visible = true;
    }
}

public class DisableMovementState : State
{
    private FSM<State> fsm;

    public DisableMovementState(FSM<State> _fsm) : base(_fsm)
    {
        fsm = _fsm;
    }

    public override void OnEnter()
    {
        Cursor.visible = false;
    }

    public override void OnUpdate()
    {
    }

    public override void OnExit()
    {
        Debug.Log("exiting");
        Cursor.visible = true;
    }
}
