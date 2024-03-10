using UnityEngine;
using UnityEngine.UI;

public class ComputerManager : MonoBehaviour
{
    public static ComputerManager instance;
    [SerializeField]
    private Canvas loginScreen,
    HomeScreen, MailScreen, MapScreen, TasksScreen;

    public GameObject TaskScreenContent;

    public Material MapScreenMaterial, BackgroundScreenMaterial;
    public GameObject ComputerScreenObj;
    private Renderer ComputerScreenRenderer;

    //states
    public LoginState loginState;
    private readonly FSM<State> ComputerFsm = new();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(this.gameObject); // Als je wilt dat dit object persistent is over scenes
        }
        else
        {
            Destroy(gameObject); // Zorgt ervoor dat er geen duplicaten zijn
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ComputerScreenRenderer = ComputerScreenObj.GetComponent<Renderer>();
        SwitchScreenMaterial(BackgroundScreenMaterial);

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
        ComputerFsm?.AddState(new HomeScreenState(ComputerFsm, HomeScreen, this));
        ComputerFsm?.AddState(new MapScreenState(ComputerFsm, this, MapScreen));
        ComputerFsm?.AddState(new MailScreenState(ComputerFsm, MailScreen));
        ComputerFsm?.AddState(new TasksScreen(ComputerFsm, TasksScreen));
        ComputerFsm?.AddState(new TaskContentScreenState(ComputerFsm, TaskScreenContent));
        ComputerFsm?.SwitchState(typeof(TasksScreen));
    }


    public void SwitchComputerState(System.Type newState)
    {
        ComputerFsm.SwitchState(newState);
    }

    public void SwitchScreenMaterial(Material mat)
    {
        ComputerScreenRenderer.material = mat;
    }
}
