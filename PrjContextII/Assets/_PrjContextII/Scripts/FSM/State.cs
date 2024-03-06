public abstract class State
{
    protected FSM<State> FSM;
    public State(FSM<State> _fSM)
    {
        FSM = _fSM;
    }
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnLateUpdate() { }
    public virtual void OnExit() { }
}