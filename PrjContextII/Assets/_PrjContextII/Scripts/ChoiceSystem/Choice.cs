using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice
{
    public string choiceText; // De tekst die de keuze beschrijft
    public GameObject[] Buildings; //de buildings die bij deze keuze horen
    public Material changeMaterial; // Het materiaal dat verandert als gevolg van de keuze
    public bool IsCorrectChoice;

    public Choice(string text, bool isCorrectChoice)
    {
        choiceText = text;
        IsCorrectChoice = isCorrectChoice;
    }
    // Voeg meer eigenschappen toe afhankelijk van wat er nodig is, zoals verandering in score, enz.
}

[System.Serializable]
public class Day
{
    public List<Choice> choices; // Een lijst met keuzes voor de dag
}

