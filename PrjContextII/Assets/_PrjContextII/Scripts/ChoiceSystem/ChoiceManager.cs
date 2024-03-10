using UnityEngine;
using UnityEngine.UI; // Zorg ervoor dat je de UI namespace gebruikt voor toegang tot UI componenten
using System.Collections.Generic;


public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager instance { get; private set; }
    public bool ChoicesLeft = false;
    private MaterialChanger materialChanger = new();
    public List<Day> Days; // Een lijst met alle dagen en hun keuzes
    public List<Choice> ChoicesOfToday { get; private set; } = new List<Choice>();

    public int currentDayIndex = 1, CurrentChoiceIndex = 0; // Houdt bij welke dag het is

    public Text ChoiceText; // Een UI Text component om de keuzetekst te tonen
    public float GoodOrBadMeter = 0, GoodBadBorder = 0.2f, GoodBadIncrement = 0.2f;
    // UI Buttons voor Ja en Nee keuzes
    // public Button YesButton;
    // public Button NoButton;
    private void Start()
    {
        DisplayChoices();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(this.gameObject); // Als je wilt dat dit object persistent is over scenes
        }
        else
        {
            Destroy(gameObject); // Zorgt ervoor dat er geen duplicaten zijn
        }
    }

    // Methode om de keuzes voor de huidige dag te tonen en toe te voegen aan ChoicesOfToday
    public void DisplayChoices()
    {
        ChoicesLeft = true;
        ChoicesOfToday.Clear(); // Zorg ervoor dat de lijst leeg is voordat je nieuwe keuzes toevoegt
        if (currentDayIndex < Days.Count)
        {
            Day currentDay = Days[currentDayIndex];

            // Voeg alle keuzes van de huidige dag toe aan ChoicesOfToday
            ChoicesOfToday.AddRange(currentDay.choices);
            Debug.Log(ChoicesOfToday.Count);

            // Voor dit voorbeeld, toon gewoon de eerste vraag van de dag
            if (currentDay.choices.Count > 0)
            {
                // Debug.Log(Days.Count);
                ChoiceText.text = currentDay.choices[0].choiceText;
                // Verbind de Yes/No knoppen met MakeChoice methode...
            }
        }
    }

    public void MakeChoice(bool choiceMade)
    {
        // Hier zou je logica implementeren om te bepalen wat er gebeurt op basis van de keuze
        // Bijvoorbeeld, controleer of choiceMade overeenkomt met isCorrectChoice van de huidige keuze

        // Voor nu, simpelweg loggen of de juiste keuze gemaakt is
        if (Days[currentDayIndex].choices.Count > currentDayIndex)//als de aantal totale keuzes van vandaag nog meer is dan het aantal keuzes gemaakt.
        {
            // Debug.Log("Juiste keuze gemaakt: " + choiceMade);
            if (choiceMade)
            {
                GoodOrBadMeter = Mathf.Clamp(GoodOrBadMeter + GoodBadIncrement, -1f, 1f);
            }
            else
            {
                GoodOrBadMeter = Mathf.Clamp(GoodOrBadMeter - GoodBadIncrement, -1f, 1f);
            }

            FloatRange range = DetermineRange(GoodOrBadMeter);
            Choice currentChoice;
            Debug.Log(CurrentChoiceIndex);
            if (currentDayIndex < Days.Count && CurrentChoiceIndex < Days[currentDayIndex].choices.Count)
            {
                currentChoice = Days[currentDayIndex].choices[CurrentChoiceIndex];
                CurrentChoiceIndex++;
            }
            else if (CurrentChoiceIndex >= Days[currentDayIndex].choices.Count)
            {
                AdvanceToNextDay();
                Debug.LogWarning("Geen choices meer voor vandaag. voor logic uit(die is er nog niet)");
                Debug.LogWarning("Choices left: " + (CurrentChoiceIndex - Days[currentDayIndex].choices.Count));
                currentChoice = null;
                ChoicesLeft = false;
            }

            switch (range)
            {
                //TODO: hier de materials veranderen van de gebouwen
                case FloatRange.BetweenZeroAndOne:
                    Debug.Log("De waarde ligt tussen 0.2 en 1.");
                    //hier apply je de texture op de building
                    // ApplyChoice(currentChoice);
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
        }
        // else //anders is de dag voorbij en eindigen we de turn.
        // {
        //     Debug.Log("hoi");
        //     currentDayIndex++;
        //     CurrentChoiceIndex = 0;
        // }

        // Ga naar de volgende vraag of dag, afhankelijk van je spellogica
    }
    public void ApplyChoice(Choice choice)
    {
        // Loop door elk GameObject in de Buildings array van de keuze
        foreach (GameObject building in choice.Buildings)
        {
            // Verkrijg de Renderer component van het GameObject
            Renderer renderer = building.GetComponent<Renderer>();

            // Controleer of de Renderer component bestaat
            if (renderer != null)

            {
                // Wijzig het materiaal van de Renderer naar het materiaal gedefinieerd in de keuze
                renderer.material = choice.changeMaterial;
            }
        }
    }

    FloatRange DetermineRange(float value)
    {
        var range = (value > 0 && value <= 1, value < 0 && value >= -1, value > 0 && value <= GoodBadBorder || value < 0 && value >= GoodBadBorder);

        switch (range)
        {
            case (true, _, _):
                return FloatRange.BetweenZeroAndOne;
            case (_, true, _):
                return FloatRange.BetweenZeroAndMinusOne;
            case (_, _, true):
                return FloatRange.Neutral;
            default:
                return FloatRange.Other;
        }
    }

    public void AdvanceToNextDay()
    {
        if (currentDayIndex < Days.Count - 1) // Controleer of er nog dagen over zijn
        {
            currentDayIndex++; // Ga naar de volgende dag
            CurrentChoiceIndex = 0; // Reset de keuze-index voor de nieuwe dag
            DisplayChoices(); // Toon de keuzes voor de nieuwe dag

            Debug.Log("Overgegaan naar dag: " + (currentDayIndex + 1)); // Houd er rekening mee dat currentDayIndex 0-gebaseerd is
        }
        else
        {
            Debug.Log("Alle dagen voltooid. Spel is afgelopen of ga naar een eindscherm.");
            // Hier kun je logica toevoegen om het spel te beëindigen of naar een eindscherm te gaan
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
