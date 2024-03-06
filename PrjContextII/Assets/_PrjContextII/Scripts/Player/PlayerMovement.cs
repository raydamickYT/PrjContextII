using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class PlayerMovement : State
{
    public LayerMask screenLayerMask;
    private Camera mainCam;
    private PlayerStateHandler PlayerFSM;
    public ComputerManager CompManager;
    private string stateName = "LookLeft";
    public PlayerSettings PS;
    public Texture2D cursorArrow, cursorHand;
    private FSM<State> fSM;


    public PlayerMovement(PlayerSettings _PS, PlayerStateHandler _fsm, FSM<State> _fSM, ComputerManager _com) : base(_fSM)
    {
        screenLayerMask = _PS.computerLayerMask;
        mainCam = _PS.MainCam;
        PS = _PS;
        PlayerFSM = _fsm;
        fSM = _fSM;
        cursorArrow = _PS.cursorArrow;
        cursorHand = _PS.cursorHand;
        CompManager = _com;
    }

    // Update is called once per frame
    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayBackwards();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayForwards();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // anim.SetTrigger("AwayFromComp");
            PlayerFSM.SwitchPlayerState(typeof(ComputerInteract));
        }
        AnimatorStateInfo stateInfo = PS.anim.GetCurrentAnimatorStateInfo(0); // 0 verwijst naar de eerste laag van de Animator

        // Check of de huidige staat overeenkomt met de staat die je wilt monitoren
        if (stateInfo.IsName(stateName))
        {
            if (stateInfo.normalizedTime > 1)
            {
                PS.anim.SetFloat("Speed", 0);
                PS.anim.Play("LookLeft", 0, 1);
            }
            else if (stateInfo.normalizedTime < 0)
            {
                PS.anim.SetFloat("Speed", 0);
                PS.anim.Play("LookLeft", 0, 0);
            }
            // Print de huidige tijd van de animatie naar de console
            // Debug.Log($"Animatie {stateName} tijd: {stateInfo.normalizedTime}");
        }
    }
    public override void OnLateUpdate()
    {
        RayCastToUI();
    }

    void RayCastToUI()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, screenLayerMask))
        {
            Debug.Log("Geraakt object: " + hit.collider.gameObject.name);
            Cursor.SetCursor(cursorHand, Vector2.zero, CursorMode.ForceSoftware);
            if (Input.GetMouseButtonDown(0))
            {
                // RaycastToScreen();
                fSM.SwitchState(typeof(DisableMovementState));
                CompManager.loginState.SelectInputField();
            }
        }
        else
        {
            Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    void PlayBackwards()
    {
        PS.anim.SetFloat("Speed", -1f);
        PS.anim.SetTrigger("StartAnimation");
    }
    void PlayForwards()
    {
        PS.anim.SetFloat("Speed", 1f);
        PS.anim.SetTrigger("StartAnimation");
    }
}

