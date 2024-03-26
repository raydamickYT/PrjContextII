using System.Collections;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody))]
public class PlayerStateHandler : MonoBehaviour
{
    public static PlayerStateHandler Instance;
    public Rigidbody rb;
    public Animator anim;
    public Camera mainCam;
    private readonly FSM<State> playerFsm = new();
    public GameObject Screen;
    public PlayerSettings PS;
    public Image FadeImage;

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
        FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, 1);
        StartCoroutine(FadeFromBlack());
        SetupStates();
    }

    void FillScripteableObject()
    {
        PS.Screen = Screen;
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
        playerFsm.AddState(new DisableMovementForCompState(playerFsm));
        playerFsm.AddState(new DisableMovementState(playerFsm));
        playerFsm.SwitchState(typeof(PlayerMovement));
    }

    public void SwitchPlayerState(System.Type newState)
    {
        playerFsm.SwitchState(newState);
    }
    IEnumerator FadeToBlack(float WaitTime)
    {
        Color imageColor = FadeImage.color;
        float alphaValue = imageColor.a;
        playerFsm.SwitchState(typeof(DisableMovementState));
        while (alphaValue < 1)
        {
            alphaValue += Time.deltaTime * 1f; // 1f is de fade speed
            FadeImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            yield return null;
        }
        yield return new WaitForSeconds(WaitTime);
        StartCoroutine(FadeFromBlack());
        playerFsm.SwitchState(typeof(PlayerMovementFree));

    }
    IEnumerator FadeFromBlack()
    {
        Color imageColor = FadeImage.color;
        float alphaValue = imageColor.a;

        // Verminder de alpha-waarde geleidelijk tot 0
        while (alphaValue > 0)
        {
            alphaValue -= Time.deltaTime * 1f;
            FadeImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            yield return null;
        }
    }
    void OnCollisionStay(Collision other)
    {
        if ((PS.BedLayerMask.value & (1 << other.gameObject.layer)) != 0)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("day ended");
                GameManager.instance.EndDay();
                StartCoroutine(FadeToBlack(1));
            }
            if (!ChoiceManager.instance.ChoicesLeft)
            {
                Debug.Log("Press E to end Day");
                //hier einde turn.
            }
        }
    }
}
