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

    public Action KeyboardClicks;
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
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void InvokeAudio()
    {
        RuntimeManager.PlayOneShot(KeyboardAction, transform.position);
    }
}
