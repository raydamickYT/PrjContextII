    using UnityEngine;
using UnityEngine.UI;

public class ComputerManager : MonoBehaviour
{
    [SerializeField]
    private Canvas loginScreen,
    HomeScreen;

    //states
    public LoginState loginState;
    private readonly FSM<State> ComputerFsm = new();

    // Start is called before the first frame update
    void Start()
    {
        SetupStates();
    }

    // Update is called once per frame
    void Update()
    {
        ComputerFsm?.OnUpdate();

    }

    void LateUpdate()
    {
        ComputerFsm?.OnLateUpdate();
    }

    void SetupStates()
    {
        loginState = new(ComputerFsm, loginScreen);
        ComputerFsm?.AddState(loginState);
        ComputerFsm?.AddState(new HomeScreenState(ComputerFsm, HomeScreen));
        ComputerFsm?.AddState(new MapState(ComputerFsm));
        ComputerFsm?.AddState(new TasksClass(ComputerFsm));
        ComputerFsm?.AddState(new MailState(ComputerFsm));
        ComputerFsm?.SwitchState(typeof(LoginState));
    }


    public void SwitchPlayerState(System.Type newState)
    {
        ComputerFsm.SwitchState(newState);
    }
}
