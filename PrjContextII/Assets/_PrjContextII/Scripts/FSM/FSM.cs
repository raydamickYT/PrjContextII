using System.Collections.Generic;

public class FSM<T>
{
    private State currentState;
    public ComputerManager computerManager;
    private Dictionary<System.Type, State> allStates = new();

    public void AddState(State _state)
    {
        if (!allStates.ContainsKey(_state.GetType()))
        {
            allStates.Add(_state.GetType(), _state);
            _state.computerManager = computerManager;
        }
    }

    public void OnUpdate()
    {
        currentState?.OnUpdate();
    }

    public void OnLateUpdate()
    {
        currentState?.OnLateUpdate();
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

    public void SwitchStateWithExtraInfo(System.Type _type, object extraInfo)
    {
        if (allStates.ContainsKey(_type))
        {
            currentState?.OnExit();
            currentState = allStates[_type];

            // Als de nieuwe staat extra informatie verwacht, roep dan InitializeWithExtraInfo aan
            if (currentState is IStateWithExtraInfo stateWithExtraInfo)
            {
                stateWithExtraInfo.InitializeWithExtraInfo(extraInfo);
            }

            currentState?.OnEnter();
        }
    }

}

public interface IStateWithExtraInfo
{
    void InitializeWithExtraInfo(object extraInfo);
}
