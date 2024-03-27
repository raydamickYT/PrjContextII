using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    public static Keyboard Instance;
    [SerializeField]
    private FMODUnity.EventReference KeyboardAction;
    [SerializeField]
    private FMODUnity.EventReference MouseAction;

    public Action KeyboardClicks;
    public Action MouseClicks;
    private void Awake()
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
        KeyboardClicks += InvokeAudio;
        MouseClicks += InvokeAudio2;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void InvokeAudio()
    {
        RuntimeManager.PlayOneShot(KeyboardAction, transform.position);
    }
    private void InvokeAudio2()
    {
        RuntimeManager.PlayOneShot(MouseAction, transform.position);
    }
}
