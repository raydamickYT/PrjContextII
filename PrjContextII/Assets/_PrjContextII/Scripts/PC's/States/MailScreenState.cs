using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailScreenState : State
{
    private Canvas MailScreen;
    public GameObject mailContentPanel; // Verwijs naar het paneel dat de mailinhoud toont

    public MailScreenState(FSM<State> _fSM, Canvas _mailscreen) : base(_fSM)
    {
        MailScreen = _mailscreen;
        MailScreen.enabled = false;
    }

    public override void OnEnter()
    {
        MailScreen.enabled = true;

        // Initialiseer inlogscherm UI
        // InputField[] inputFields = MailScreen.GetComponentsInChildren<InputField>(true);

        // foreach (var inputField in inputFields)
        // {
        //     if (inputField.name == "Name")
        //     {
        //         // nameInputField = inputField;
        //     }
        //     else if (inputField.name == "Password")
        //     {
        //         // passwordInputField = inputField;
        //     }
        //     else
        //     {
        //         Debug.LogWarning("Juiste Knop is niet gevonden voor: " + inputField.name);
        //     }
        // }
    }

    public override void OnUpdate()
    {
        // Update inlogscherm logica, bijv. inlogpoging
    }

    // Deze functie toont de inhoud van de mail
    public void OpenMail(int mailId)
    {
        // Logica om de inhoud van de mail te laden gebaseerd op mailId
        // Bijvoorbeeld: mailContentPanel.SetActive(true);
        Debug.Log("Mail geopend met ID: " + mailId);
    }

    public override void OnExit()
    {
        // Opruimen van inlogscherm
        MailScreen.enabled = false;

    }
}
