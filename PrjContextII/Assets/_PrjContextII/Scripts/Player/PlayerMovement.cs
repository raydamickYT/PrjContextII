using System.Runtime.CompilerServices;
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
    public Texture2D cursorArrow, cursorHand;
    private FSM<State> fSM;
    private bool hasMoved = false;


    public PlayerMovement(PlayerStateHandler _fsm, FSM<State> _fSM) : base(_fSM)
    {
        PlayerFSM = _fsm;
        fSM = _fSM;
    }

    public override void OnEnter()
    {
      if (!HasPlayedSound && VoiceOvers.Instance != null)
        {
            VoiceOvers.Instance.PlayDesktop();
            HasPlayedSound = true;
        }
        if (mainCam == null)
        {
            Init();
        }
    }

    public void Init()
    {
        if (PS != null)
        {
            screenLayerMask = PS.ComputerLayerMask;
            mainCam = PS.MainCam;
            cursorArrow = PS.CursorArrow;
            cursorHand = PS.CursorHand;
            CompManager = computerManager;
        }
        else
        {
            Debug.Log("geen ps gevonden");
        }
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (hasMoved)
            {
                PlayBackwards();
                hasMoved = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!hasMoved)
            {
                PlayForwards();
                hasMoved = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // anim.SetTrigger("AwayFromComp");
            PlayerFSM.SwitchPlayerState(typeof(ComputerInteract));
            ComputerManager.instance.SwitchComputerState(typeof(LoginState), null);
        }
        AnimatorStateInfo stateInfo = PS.Anim.GetCurrentAnimatorStateInfo(0); // 0 verwijst naar de eerste laag van de Animator

        // Check of de huidige staat overeenkomt met de staat die je wilt monitoren
        if (stateInfo.IsName(stateName))
        {
            if (stateInfo.normalizedTime > 1)
            {
                PS.Anim.SetFloat("Speed", 0);
                PS.Anim.Play("LookLeft", 0, 1);
            }
            else if (stateInfo.normalizedTime < 0)
            {
                PS.Anim.SetFloat("Speed", 0);
                PS.Anim.Play("LookLeft", 0, 0);
            }
            // Print de huidige tijd van de animatie naar de console
            // Debug.Log($"Animatie {stateName} tijd: {stateInfo.normalizedTime}");
        }
    }
    public override void OnLateUpdate()
    {
        RayCastToUI();
    }

    bool isHoveringOverScreen = false;

    public bool HasPlayedSound { get; private set; }

    void RayCastToUI()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, screenLayerMask))
        {
            // Debug.Log("Geraakt object: " + hit.collider.gameObject.name);
            if (!isHoveringOverScreen)
            {
                Cursor.SetCursor(cursorHand, Vector2.zero, CursorMode.ForceSoftware);
                isHoveringOverScreen = true;
            }
            if (Input.GetMouseButtonDown(0))
            {
                // RaycastToScreen();
                fSM.SwitchState(typeof(DisableMovementForCompState));
                if (CompManager.loginState.IsActive)
                {
                    CompManager.loginState.SelectInputField();
                }

            }
        }
        else
        {
            if (isHoveringOverScreen)
            {
                Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
                isHoveringOverScreen = false;
            }
        }
    }

    void PlayBackwards()
    {
        PS.Anim.SetFloat("Speed", -1f);
        PS.Anim.SetTrigger("StartAnimation");
    }
    void PlayForwards()
    {
        PS.Anim.SetFloat("Speed", 1f);
        PS.Anim.SetTrigger("StartAnimation");
    }

}

