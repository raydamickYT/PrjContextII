using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class ComputerManager : MonoBehaviour
{
    public static ComputerManager instance;
    [SerializeField]
    private Canvas loginScreen,
    HomeScreen, MailScreen, MapScreen, TasksScreen;

    public GameObject TaskScreenContent;

    // public Material MapScreenMaterial, BackgroundScreenMaterial, HomeScreenMaterial;
    public AllMaterials allMaterials;
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
    // public List<Material> allMaterials; // Je moet je materialen hieraan toewijzen in de Inspector

    public Material GetMaterialByName(string materialName)
    {
        foreach (Material mat in allMaterials.materials)
        {
            if (mat.name == materialName)
            {
                return mat;
            }
        }
        Debug.Log("no material found by that name");
        return null; // Geen materiaal gevonden met die naam
    }
    void SetupStates()
    {
        ComputerFsm.computerManager = this;
        ComputerFsm.playerSettings = PlayerStateHandler.Instance.PS;
        loginState = new(ComputerFsm, loginScreen);
        ComputerFsm?.AddState(loginState);
        ComputerFsm?.AddState(new HomeScreenState(ComputerFsm, HomeScreen));
        ComputerFsm?.AddState(new MapScreenState(ComputerFsm, MapScreen));
        ComputerFsm?.AddState(new MailScreenState(ComputerFsm, MailScreen));
        ComputerFsm?.AddState(new TasksScreen(ComputerFsm, TasksScreen));
        ComputerFsm?.AddState(new TaskContentScreenState(ComputerFsm, TaskScreenContent));
        ComputerFsm?.SwitchState(typeof(LoginState));
    }


    public void SwitchComputerState(System.Type newState, object ChoiceInfo)
    {
        ComputerFsm.SwitchStateWithExtraInfo(newState, ChoiceInfo);
    }

    public void SwitchScreenMaterial(Material mat)
    {
        ComputerScreenRenderer.material = mat;
    }


}
