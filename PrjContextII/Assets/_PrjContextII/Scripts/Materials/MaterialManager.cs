using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public static MaterialManager Instance;
    public Action<int> HandleMaterialChange;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); //er mogen geen duplicates zijn
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Een methode die de delegate aanroept.
    public void TriggerAction(int type)
    {
        HandleMaterialChange?.Invoke(type); // Roep de action/delegate alleen aan als er subscribers zijn.
    }

    // Een methode om te abonneren op de action/delegate.
    public void SubscribeToAction(Action<int> action)
    {
        HandleMaterialChange += action;
    }

    // Een methode om af te melden van de action/delegate.
    public void UnsubscribeFromAction(Action<int> action)
    {
        HandleMaterialChange -= action;
    }
}
