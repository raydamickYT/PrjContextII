using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Cinemachine.Editor;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerStateHandler : MonoBehaviour
{
    public static PlayerStateHandler Instance;
    public Rigidbody rb;
    public Animator anim;
    public Camera mainCam;
    private readonly FSM<State> playerFsm = new();

    public PlayerSettings PS;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        mainCam = GetComponentInChildren<Camera>();
        FillScripteableObject();
    }
    void Start()
    {
        if (mainCam == null)
        {
            Debug.LogWarning("camera is niet gevonden in speler");
        }

        if (ComputerManager.instance == null)
        {
            Debug.LogWarning("CompManager is niet assigned.");
        }

        SetupStates();
    }

    void FillScripteableObject()
    {
        PS.rb = rb;
        PS.MainCam = mainCam;
        PS.Anim = anim;
    }

    void Update()
    {
        playerFsm.OnUpdate();
    }

    void LateUpdate()
    {
        playerFsm.OnLateUpdate();
    }

    void SetupStates()
    {
        playerFsm.computerManager = ComputerManager.instance;
        playerFsm.playerSettings = this.PS;
        playerFsm.AddState(new PlayerMovement(this, playerFsm));
        playerFsm.AddState(new PlayerMovementFree(this, playerFsm));
        playerFsm.AddState(new ComputerInteract(this, playerFsm));
        playerFsm.AddState(new DisableMovementState(playerFsm));
        playerFsm.SwitchState(typeof(PlayerMovement));
    }

    public void SwitchPlayerState(System.Type newState)
    {
        playerFsm.SwitchState(newState);
    }
    void OnCollisionStay(Collision other)
    {
        if ((PS.BedLayerMask.value & (1 << other.gameObject.layer)) != 0)
        {
            if (!ChoiceManager.instance.ChoicesLeft)
            {
                Debug.Log("Press E to end Day");
                //hier einde turn.
                if (Input.GetKey(KeyCode.E))
                {
                    Debug.Log("day ended");
                    GameManager.instance.EndDay();
                }
            }
        }
    }
}
