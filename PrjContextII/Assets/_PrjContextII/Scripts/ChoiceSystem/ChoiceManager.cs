using UnityEngine;
using UnityEngine.UI; // Zorg ervoor dat je de UI namespace gebruikt voor toegang tot UI componenten
using System.Collections.Generic;


public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager instance { get; private set; }
    public bool ChoicesLeft = false;
    public Transform ButtonsParent;
    public Button QuestionButtonPrefab;

    public List<Day> Days; // Een lijst met alle dagen en hun keuzes
    public List<Choice> ChoicesOfToday { get; private set; } = new List<Choice>();

    public int currentDayIndex = 0, CurrentChoiceIndex = 0; // Houdt bij welke dag het is

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

        // Debug.Log("days count " + Days.Count);
        // Debug.Log("current day " + currentDayIndex);

        if (currentDayIndex < Days.Count)
        {
            // Debug.Log(currentDayIndex);
            Day currentDay = Days[currentDayIndex];

            // Voeg alle keuzes van de huidige dag toe aan ChoicesOfToday
            ChoicesOfToday.AddRange(currentDay.choices);

            // Voor dit voorbeeld, toon gewoon de eerste vraag van de dag
            if (currentDay.choices.Count > 0)
            {
                // Debug.Log(Days.Count);
                ChoiceText.text = currentDay.choices[0].choiceText;
                // Verbind de Yes/No knoppen met MakeChoice methode...
            }
        }
    }

    public bool AdvanceNextDay()
    {
        // currentDayIndex += 1;
        if (currentDayIndex < Days.Count) // Controleer of er nog dagen over zijn
        {
            currentDayIndex++; // Ga naar de volgende dag
            CurrentChoiceIndex = 0; // Reset de keuze-index voor de nieuwe dag
            DisplayChoices(); // Toon de keuzes voor de nieuwe dag
            return false;

            // Debug.Log("Overgegaan naar dag: " + (currentDayIndex + 1)); // Houd er rekening mee dat currentDayIndex 0-gebaseerd is
        }
        else
        {
            Debug.Log("Alle dagen voltooid. Spel is afgelopen of ga naar een eindscherm.");
            return true;
            // Hier kun je logica toevoegen om het spel te beëindigen of naar een eindscherm te gaan
        }
    }
    public void MakeChoice(bool choiceMade)
    {
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

        if (currentDayIndex < Days.Count && CurrentChoiceIndex < Days[currentDayIndex].choices.Count)
        {
            // Debug.Log("pipo currentchoice index: " + CurrentChoiceIndex);
            // Debug.Log("pipo keuzes over vandaag: " + currentDayIndex);
            currentChoice = Days[currentDayIndex].choices[CurrentChoiceIndex];
            CurrentChoiceIndex++;
        }
        else if (currentDayIndex < Days.Count) //als de current choice index meer is dan de hoeveelheid keuzes die we hebben.
        {
            if (CurrentChoiceIndex >= Days[currentDayIndex].choices.Count) //als de current choice index meer is dan de hoeveelheid keuzes die we hebben.
            {
                currentChoice = null;
                ChoicesLeft = false;
            }
            // AdvanceToNextDay();
            // Debug.LogWarning("Geen choices meer voor vandaag. voor logic uit(die is er nog niet)");
            // Debug.LogWarning("Choices left: " + (CurrentChoiceIndex - Days[currentDayIndex].choices.Count));
        }
        else
        {
            //we zijn door onze dagen heen
            Debug.Log("Alle dagen voltooid. Spel is afgelopen of ga naar een eindscherm.");
        }

        switch (range)
        {
            //TODO: hier de materials veranderen van de gebouwen
            case FloatRange.BetweenZeroAndOne:
                // Debug.Log("De waarde ligt tussen 0.2 en 1.");
                //hier apply je de texture op de building
                // ApplyChoice(currentChoice);
                MaterialManager.Instance.TriggerAction(2);
                break;
            case FloatRange.BetweenZeroAndMinusOne:
                // Debug.Log("De waarde ligt tussen 0 en -1.");
                MaterialManager.Instance.TriggerAction(1);
                break;
            case FloatRange.Other:
                Debug.LogWarning("De waarde ligt niet binnen de gespecificeerde bereiken.");
                break;
            case FloatRange.Neutral:
                // Debug.Log("De waarde is neutraal");
                MaterialManager.Instance.TriggerAction(0);
                break;
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


    public List<Button> DisplayTaskButtons()
    {
        List<Button> createdButtons = new List<Button>();

        foreach (var question in ChoicesOfToday)
        {
            // Debug.Log("button spawned" + ButtonsParent.name);
            Button questionButton = Instantiate(QuestionButtonPrefab, ButtonsParent);
            questionButton.GetComponentInChildren<Text>().text = question.choiceName;
            questionButton.GetComponent<TaskContent>().choice = question;

            // Zorg ervoor dat je ook event listeners correct instelt

            createdButtons.Add(questionButton);
        }

        return createdButtons; // Retourneer de lijst met aangemaakte knoppen
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
