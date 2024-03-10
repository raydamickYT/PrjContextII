using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

[System.Serializable]
public class Choice
{
    [Tooltip("Dit is de naam van de task knop")]
    public string choiceName; 

    [TextArea]
    [Tooltip("Dit is de tekst van de task")]
    public string choiceText; // De tekst die de keuze beschrijft
    public GameObject[] Buildings; //de buildings die bij deze keuze horen
    public Material changeMaterial; // Het materiaal dat verandert als gevolg van de keuze

    public Choice(string text)
    {
        choiceText = text;
    }
    // Voeg meer eigenschappen toe afhankelijk van wat er nodig is, zoals verandering in score, enz.
}

[System.Serializable]
public class Day
{
    public List<Choice> choices; // Een lijst met keuzes voor de dag
}

