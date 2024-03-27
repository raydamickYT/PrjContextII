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
    public Action AdvanceTheDay;
    public bool GameEnded = false;

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
        currentDayIndex = Days.Count;
        Debug.Log(Days.Count);

        if (currentDayIndex >= Days.Count) //laatste dag
        {
            //einde spel
            GameEnded = true;
            Finale.Instance.StartFinale();
        }
        else
        {
            currentDayIndex++; // Ga naar de volgende dag
            AdvanceTheDay.Invoke(); //hier later iets mee doen. als de result true is dan einde game.

            if (VoiceOvers.Instance != null)
            {
                VoiceOvers.Instance.PlayGoodBad();
            }
        }

    }
}
