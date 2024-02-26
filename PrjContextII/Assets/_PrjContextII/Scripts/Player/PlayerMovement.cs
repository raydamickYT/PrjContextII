using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class PlayerMovement : State
{
    private PlayerStateHandler PlayerFSM;
    private string stateName = "LookLeft";
    public PlayerSettings PS;

    public PlayerMovement(PlayerSettings _PS, PlayerStateHandler _fsm)
    {
        PS = _PS;
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

