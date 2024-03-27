using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
using UnityEngine.PlayerLoop;
using JetBrains.Annotations;
using System;
using Unity.VisualScripting;

public class MailAudioManager : MonoBehaviour
{
    public static MailAudioManager Instance;

    public FMODUnity.EventReference MailGood, MailBad;
    public Action<bool> PlayMail;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayMail += DecideMail;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DecideMail(bool isGoodMail)
    {
        switch (isGoodMail)
        {
            case true:
                StartCoroutine(PlayGoodMail());
                break;

            default:
                StartCoroutine(PlayBadMail()); //als true het niet is, dan is het sowieso false
                break;
        }
    }

    private IEnumerator PlayBadMail()
    {
        yield return new WaitForSeconds(0.4f);
        RuntimeManager.PlayOneShot(MailBad, transform.position);
    }
    private IEnumerator PlayGoodMail()
    {
        yield return new WaitForSeconds(0.4f);
        RuntimeManager.PlayOneShot(MailGood, transform.position);
    }

    void OnDestroy()
    {
        PlayMail = null;
    }
}
