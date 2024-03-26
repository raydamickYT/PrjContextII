using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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
    public PrefabChanger prefabChanger;
    public Choice(string text)
    {
        choiceText = text;
    }
    // Voeg meer eigenschappen toe afhankelijk van wat er nodig is, zoals verandering in score, enz.
}

[System.Serializable]
public class ChoiceDay
{
    public List<Choice> choices; // Een lijst met keuzes voor de dag
    public List<Mail> mail; //lijst met mails voor die dag
    public Book book;

}

[System.Serializable]
public class Mail
{
    public string MailTitle;
    public string MailSender, MailReceiver;
    [Tooltip("This is where you enter you mail's content"), TextArea]
    public string MailContent;
}

[System.Serializable]
public class Book
{
    public string Day = "Day ...: ";
    [TextArea]
    public string DayText;
}


