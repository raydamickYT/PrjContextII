public abstract class State
{
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnLateUpdate() { }
    public virtual void OnExit() { }
}