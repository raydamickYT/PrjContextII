using System.Collections.Generic;

public class FSM<T>
{
    private State currentState;
    private Dictionary<System.Type, State> allStates = new();

    public void AddState(State _state)
    {
        if (!allStates.ContainsKey(_state.GetType()))
        {
            allStates.Add(_state.GetType(), _state);
        }
    }

    public void OnUpdate()
    {
        currentState?.OnUpdate();
    }

    public void SwitchState(System.Type _type)
    {
        if (allStates.ContainsKey(_type))
        {
            currentState?.OnExit();
            currentState = allStates[_type];
            currentState?.OnEnter();
        }
    }
}