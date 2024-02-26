using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class PlayerMovement : State
{
    public Rigidbody rb;
    public Animator anim;
    private PlayerStateHandler PlayerFSM;
    private string stateName = "LookLeft";

    public PlayerMovement(Rigidbody _rb, Animator _anim, PlayerStateHandler _fsm)
    {
        anim = _anim;
        rb = _rb;
        PlayerFSM = _fsm;
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
            PlayerFSM.SwitchPlayerState(typeof(ComputerInteract));
        }
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0); // 0 verwijst naar de eerste laag van de Animator

        // Check of de huidige staat overeenkomt met de staat die je wilt monitoren
        if (stateInfo.IsName(stateName))
        {
            if (stateInfo.normalizedTime > 1)
            {
                anim.SetFloat("Speed", 0);
                anim.Play("LookLeft", 0, 1);
            }
            else if (stateInfo.normalizedTime < 0)
            {
                anim.SetFloat("Speed", 0);
                anim.Play("LookLeft", 0, 0);
            }
            // Print de huidige tijd van de animatie naar de console
            // Debug.Log($"Animatie {stateName} tijd: {stateInfo.normalizedTime}");
        }
    }

    void PlayBackwards()
    {
        anim.SetFloat("Speed", -1f);
        anim.SetTrigger("StartAnimation");
    }
    void PlayForwards()
    {
        anim.SetFloat("Speed", 1f);
        anim.SetTrigger("StartAnimation");
    }
}

public class PlayerMovementFree : State
{
    public Rigidbody rb;
    public Animator anim;
    private Camera MainCam;
    private PlayerStateHandler PlayerFSM;
    private float speed = 2f;

    //voor rond kijken
    private float sensX = 200, sensY = 200, xRotation = 1, yRotation = 1;
    // private Transform Orientation;

    public PlayerMovementFree(Rigidbody _rb, Animator _anim, PlayerStateHandler _fsm, Camera _mainCam)
    {
        anim = _anim;
        rb = _rb;
        PlayerFSM = _fsm;
        MainCam = _mainCam;
    }

    public override void OnEnter()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Debug.Log("test");
        anim.enabled = false;
    }
    public override void OnUpdate()
    {
        //move
        movement();

        //look
        ReadMouseInput();
    }
    public override void OnExit()
    {
        base.OnExit();
        anim.enabled = true;
    }

    public void ReadMouseInput()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        //roteer de camera
        MainCam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        // Orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    public void movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Krijg de richting vooruit en rechts gebaseerd op de camera's oriëntatie
        Vector3 forward = MainCam.transform.forward;
        Vector3 right = MainCam.transform.right;

        // Zorg ervoor dat de beweging niet beïnvloed wordt door de hoogte van de camera
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward * moveVertical + right * moveHorizontal) * speed;
        // Debug.Log(movement * speed);

        // Pas de snelheid van de Rigidbody aan op basis van de berekende richting
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z); // Behoud de huidige verticale snelheid (bijvoorbeeld voor zwaartekracht)
    }
}

public class ComputerInteract : State
{

    public Rigidbody rb;
    public Animator anim;
    private PlayerStateHandler PlayerFSM;
    private string stateName = "AwayFromComp";


    public ComputerInteract(Rigidbody _rb, Animator _anim, PlayerStateHandler _fsm)
    {
        anim = _anim;
        rb = _rb;
        PlayerFSM = _fsm;
    }

    public override void OnEnter()
    {
        Debug.Log("test");
        anim.SetTrigger("AwayFromComp");
    }
    public override void OnUpdate()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0); // 0 verwijst naar de eerste laag van de Animator

        if (stateInfo.IsName(stateName))
        {
            if (stateInfo.normalizedTime > 1)
            {
                PlayerFSM.SwitchPlayerState(typeof(PlayerMovementFree));
            }
            // Print de huidige tijd van de animatie naar de console
            Debug.Log($"Animatie {stateName} tijd: {stateInfo.normalizedTime}");
        }
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}

