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
        // Debug.Log("made");
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
        currentDayIndex++; // Ga naar de volgende dag
        Result = AdvanceTheDay.Invoke(); //hier later iets mee doen. als de result true is dan einde game.
        if (Result) //pakt de laatste return (kan dus zijn dat je dit toch nog moet veranderen naar een action)
        {
            //einde game
        }
        else
        {
            //niks, de dagen gaan door
        }
    }
}
