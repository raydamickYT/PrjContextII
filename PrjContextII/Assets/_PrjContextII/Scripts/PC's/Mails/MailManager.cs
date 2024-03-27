using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class MailManager : MonoBehaviour
{
    public static MailManager instance;
    public GameObject MailScrollViewPrefab;
    public Button MailButtonPrefab;
    public Transform ButtonsParent;
    public bool IsShowingButtonsForToday = false;
    // public List<ChoiceDay> Days; // Een lijst met alle dagen en hun keuzes
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
        DisplayMails();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.AdvanceTheDay += AdvanceNextDay;
    }

    // Methode om de keuzes voor de huidige dag te tonen en toe te voegen aan ChoicesOfToday
    public void DisplayMails()
    {
        // MailsOfToday.Clear(); // deze hoeven we niet te legen, want mail kan je de dag erna nog zien.

        if (GameManager.instance.currentDayIndex < GameManager.instance.Days.Count)
        {
            // Debug.Log(currentDayIndex);
            ChoiceDay currentDay = GameManager.instance.Days[GameManager.instance.currentDayIndex]; //alle mails zitten in de game manager
            // Debug.Log("current day" + GameManager.instance.currentDayIndex);
            if (GameManager.instance.currentDayIndex > 1) 
            {
                //hier nu logic voor de verschillende keuzes
                if (ChoiceManager.instance.FirstChoiceMade.Value) //vanaf dag 2 verschillende mails
                {
                    MailsOfToday.Add(currentDay.MailPerson1); //als ze ja hebben gezegd
                }
                else
                {
                    MailsOfToday.Add(currentDay.MailPerson2); //als ze ja hebben gezegd
                }
            }
            // Voeg alle keuzes van de huidige dag toe aan ChoicesOfToday
            MailsOfToday.AddRange(currentDay.mail);
        }
    }


    public List<Button> DisplayMailButtons()
    {
        List<Button> createdButtons = new List<Button>();
        foreach (var question in MailsOfToday)
        {
            Debug.Log("button");
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

    public void AdvanceNextDay() //call deze als de speler laat zien dat ze naar de volgende dag willen
    {
        // currentDayIndex += 1;
        if (GameManager.instance.currentDayIndex < GameManager.instance.Days.Count) // Controleer of er nog dagen over zijn
        {
            IsShowingButtonsForToday = false;
            // GameManager.instance.currentDayIndex++; // Ga naar de volgende dag
            ChoiceManager.instance.CurrentChoiceIndex = 0; // Reset de keuze-index voor de nieuwe dag
            DisplayMails();
            
            
             // Toon de keuzes voor de nieuwe dag
            // Debug.Log("Overgegaan naar dag: " + (currentDayIndex + 1)); // Houd er rekening mee dat currentDayIndex 0-gebaseerd is
        }

    }


}
