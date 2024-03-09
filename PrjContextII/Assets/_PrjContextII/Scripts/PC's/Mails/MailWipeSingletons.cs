using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MailWipeSingletons : MonoBehaviour
{
    public static MailWipeSingletons Instance { get; private set; }

    public Action OnBackButtonTriggered;
    public event Action<bool> OnMailVisibilityChanged;

    private bool mailIsShowing = false;

    public bool MailIsShowing
    {
        get { return mailIsShowing; }
        set
        {
            if (mailIsShowing != value)
            {
                mailIsShowing = value;
                OnMailVisibilityChanged?.Invoke(mailIsShowing);
            }
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Vernietig duplicaat.
        }
        else
        {
            Instance = this;
        }
    }

    // Een methode die de delegate aanroept.
    public void TriggerAction()
    {
        OnBackButtonTriggered?.Invoke(); // Roep de action/delegate alleen aan als er subscribers zijn.
    }

    // Een methode om te abonneren op de action/delegate.
    public void SubscribeToAction(Action action)
    {
        OnBackButtonTriggered += action;
    }

    // Een methode om af te melden van de action/delegate.
    public void UnsubscribeFromAction(Action action)
    {
        OnBackButtonTriggered -= action;
    }
}
