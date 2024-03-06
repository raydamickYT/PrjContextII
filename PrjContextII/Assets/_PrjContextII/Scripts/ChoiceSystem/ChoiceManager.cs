using UnityEngine;
using UnityEngine.UI; // Zorg ervoor dat je de UI namespace gebruikt voor toegang tot UI componenten
using System.Collections.Generic;


public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager instance { get; private set; }
    private MaterialChanger materialChanger = new();
    public List<Day> Days; // Een lijst met alle dagen en hun keuzes
    private int currentDayIndex = 0, CurrentChoiceIndex = 0; // Houdt bij welke dag het is

    public Text ChoiceText; // Een UI Text component om de keuzetekst te tonen
    public float GoodOrBadMeter = 0, GoodBadBorder = 0.2f, GoodBadIncrement = 0.2f;
    // UI Buttons voor Ja en Nee keuzes
    // public Button YesButton;
    // public Button NoButton;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            //NOTE: als je wilt dat het object niet vernietigt wordt bij een nieuwe scene:
            // DontDestroyOnLoad(this.gameObject);
        }
    }

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
                // YesButton.onClick.AddListener(() => MakeChoice(true));
                // NoButton.onClick.AddListener(() => MakeChoice(false));
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
            else
            {
                Debug.LogWarning("currentchoice is NULL");
                currentChoice = null;
            }

            switch (range)
            {
                case FloatRange.BetweenZeroAndOne:
                    Debug.Log("De waarde ligt tussen 0.2 en 1.");
                    // materialChanger.ChangeMaterial(1, Building.GetComponent<Renderer>());
                    ApplyChoice(currentChoice);
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
        else //anders is de dag voorbij en eindigen we de turn.
        {
            currentDayIndex++;
            CurrentChoiceIndex = 0;
        }

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
