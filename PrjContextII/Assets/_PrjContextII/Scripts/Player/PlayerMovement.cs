using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public Camera mainCamera;
    public Rigidbody rb;
    public Animator anim;
    private string stateName = "LookLeft";

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayBackwards();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayForwards();
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
