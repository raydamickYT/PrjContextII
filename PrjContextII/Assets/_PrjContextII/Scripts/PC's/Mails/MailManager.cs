using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class MailManager : MonoBehaviour
{
    public static MailManager instance;
    public GameObject MailScrollViewPrefab;
    public Button MailButtonPrefab;
    public Transform ButtonsParent;
    public bool IsShowingButtons = false;
    public List<ChoiceDay> Days; // Een lijst met alle dagen en hun keuzes
    public List<Mail> MailsOfToday { get; private set; } = new List<Mail>();
    public float GoodOrBadMeter = 0, GoodBadBorder = 0.2f, GoodBadIncrement = 0.2f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DisplayChoices();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Methode om de keuzes voor de huidige dag te tonen en toe te voegen aan ChoicesOfToday
    public void DisplayChoices()
    {
        MailsOfToday.Clear(); // Zorg ervoor dat de lijst leeg is voordat je nieuwe keuzes toevoegt

        // Debug.Log("days count " + Days.Count);
        // Debug.Log("current day " + currentDayIndex);

        if (GameManager.instance.currentDayIndex < GameManager.instance.Days.Count)
        {
            // Debug.Log(currentDayIndex);
            ChoiceDay currentDay = Days[GameManager.instance.currentDayIndex];

            // Voeg alle keuzes van de huidige dag toe aan ChoicesOfToday
            MailsOfToday.AddRange(currentDay.mail);

        }
    }


    public List<Button> DisplayMailButtons()
    {
        List<Button> createdButtons = new List<Button>();

        foreach (var question in MailsOfToday)
        {
            // Debug.Log("button spawned" + ButtonsParent.name);
            Button questionButton = Instantiate(MailButtonPrefab, ButtonsParent);
            questionButton.GetComponentInChildren<Text>().text = question.MailTitle;
            questionButton.GetComponent<MailContent>().mail = question;
            questionButton.GetComponent<MailContent>().MailScrollViewPrefab = MailScrollViewPrefab;
            
            // Zorg ervoor dat je ook event listeners correct instelt

            createdButtons.Add(questionButton);
        }

        return createdButtons; // Retourneer de lijst met aangemaakte knoppen
    }

    public bool AdvanceNextDay() //call deze als de speler laat zien dat ze naar de volgende dag willen
    {
        // currentDayIndex += 1;
        if (GameManager.instance.currentDayIndex < Days.Count) // Controleer of er nog dagen over zijn
        {
            GameManager.instance.currentDayIndex++; // Ga naar de volgende dag
            ChoiceManager.instance.CurrentChoiceIndex = 0; // Reset de keuze-index voor de nieuwe dag
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


}
