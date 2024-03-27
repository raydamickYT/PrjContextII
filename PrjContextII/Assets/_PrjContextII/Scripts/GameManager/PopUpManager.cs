using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    private Animator animator;
    public Action PlayAnimation;
    public float WaitTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        if (MailAudioManager.Instance != null)
        {
            MailAudioManager.Instance.PlayMail += PlayAnim;
        }
    }

    private void PlayAnim(bool Choice)
    {
        //hier kan ik de tekst op de popup veranderen. voor nu niet
        if (Choice)
        {
            StartCoroutine(SetTriggers());
        }
        else
        {
            StartCoroutine(SetTriggers());
        }
    }

    IEnumerator SetTriggers()
    {
        animator.SetTrigger("Trigger1");
        yield return new WaitForSeconds(WaitTime);
        animator.SetTrigger("Trigger2");
    }

    void OnDestroy()
    {
        PlayAnimation = null;
    }
}
