using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentDayIndex = 0;
    public float GoodOrBadMeter = 0, GoodBadBorder = 0.2f, GoodBadIncrement = 0.2f;
    public List<ChoiceDay> Days; // Een lijst met alle dagen en hun keuzes
    public Func<bool> AdvanceTheDay;
    public bool Result = false;

    public static BoxCollider computerCollider;
    private void Awake()
    {
        Debug.Log("made");
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void EndDay()
    {
        Result = AdvanceTheDay.Invoke();
    }

    public static void EnableComputer()
    {
        if (!computerCollider.enabled)
        {
            computerCollider.enabled = true;
            Debug.Log("computer collider is nu aan");
        }
    }
    public static void DisableComputer()
    {
        if (computerCollider.enabled)
        {
            computerCollider.enabled = false;
        }
    }
}
