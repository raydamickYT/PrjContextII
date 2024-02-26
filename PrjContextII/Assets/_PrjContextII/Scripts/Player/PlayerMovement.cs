using Unity.VisualScripting;
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
    private PlayerStateHandler PlayerFSM;
    private float speed = 10f;

    public PlayerMovementFree(Rigidbody _rb, Animator _anim, PlayerStateHandler _fsm)
    {
        anim = _anim;
        rb = _rb;
        PlayerFSM = _fsm;
    }

    public override void OnEnter()
    {
        // Debug.Log("test");
        anim.enabled = false;
    }
    public override void OnUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Debug.Log(moveHorizontal);
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        Debug.Log(movement * speed);

        // rb.AddForce(movement * speed);
        rb.velocity = movement * speed;
    }
    public override void OnExit()
    {
        base.OnExit();
        anim.enabled = true;
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

