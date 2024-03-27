using UnityEngine.UI;
using UnityEngine;

public abstract class State
{
    protected FSM<State> FSM;
    protected Canvas ScreenCanvas;
    public ComputerManager computerManager;
    public PlayerSettings PS;
    public State(FSM<State> _fSM)
    {
        FSM = _fSM;
    }
    public virtual void OnEnter() { }
    public virtual void OnUpdate()
    {
        if (PlayerStateHandler.Instance.disableMovementForCompState.IsActive)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Keyboard.Instance.MouseClicks?.Invoke();
            }
        }
    }
    public virtual void OnLateUpdate() { }
    public virtual void OnExit() { }
}