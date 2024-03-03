using UnityEngine;
using UnityEngine.UI; // Zorg ervoor dat je de UI namespace gebruikt voor toegang tot UI componenten
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class ChoiceManager : MonoBehaviour
{
    public List<Day> Days; // Een lijst met alle dagen en hun keuzes
    private int currentDayIndex = 0, choicesMade = 0; // Houdt bij welke dag het is

    public Text ChoiceText; // Een UI Text component om de keuzetekst te tonen
    public float GoodOrBadMeter = 0, GoodBadBorder = 0.2f, GoodBadIncrement = 0.2f;

    // UI Buttons voor Ja en Nee keuzes
    public Button YesButton;
    public Button NoButton;

    void Awake()
    {
        DisplayChoices();
    }

    // Methode om de keuzes voor de huidige dag te tonen
    public void DisplayChoices()
    {
        if (currentDayIndex < Days.Count)
        {
            Day currentDay = Days[currentDayIndex];

            // Voor dit voorbeeld, toon gewoon de eerste vraag van de dag
            if (currentDay.choices.Count > 0)
            {
                ChoiceText.text = currentDay.choices[0].choiceText;
                YesButton.onClick.AddListener(() => MakeChoice(true));
                NoButton.onClick.AddListener(() => MakeChoice(false));
            }
        }
    }

    public void MakeChoice(bool choiceMade)
    {
        // Hier zou je logica implementeren om te bepalen wat er gebeurt op basis van de keuze
        // Bijvoorbeeld, controleer of choiceMade overeenkomt met isCorrectChoice van de huidige keuze

        // Voor nu, simpelweg loggen of de juiste keuze gemaakt is
        Debug.Log("Juiste keuze gemaakt: " + choiceMade);
        if (choiceMade)
        {
            GoodOrBadMeter += GoodBadIncrement;
        }
        else
        {
            GoodOrBadMeter -= GoodBadIncrement;
        }

        FloatRange range = DetermineRange(GoodOrBadMeter);

        switch (range)
        {
            case FloatRange.BetweenZeroAndOne:
                Debug.Log("De waarde ligt tussen 0 en 1.");
                break;
            case FloatRange.BetweenZeroAndMinusOne:
                Debug.Log("De waarde ligt tussen 0 en -1.");
                break;
            case FloatRange.Other:
                Debug.Log("De waarde ligt niet binnen de gespecificeerde bereiken.");
                break;
            case FloatRange.Neutral:
                Debug.Log("De waarde is neutraal");
                break;
        }

        // Ga naar de volgende vraag of dag, afhankelijk van je spellogica
    }
    // Methode om te bepalen in welk bereik de float valt
    FloatRange DetermineRange(float value)
    {
        if (value > 0 && value <= 1)
        {
            return FloatRange.BetweenZeroAndOne;
        }
        else if (value < 0 && value >= -1)
        {
            return FloatRange.BetweenZeroAndMinusOne;
        }
        else if (value > 0 && value <= GoodBadBorder || value < 0 && value >= GoodBadBorder)
        {
            return FloatRange.Neutral;
        }
        else
        {
            return FloatRange.Other;
        }
    }

}
// Enum om de bereiken te definiëren
enum FloatRange
{
    BetweenZeroAndOne,
    BetweenZeroAndMinusOne,
    Neutral,
    Other
}
