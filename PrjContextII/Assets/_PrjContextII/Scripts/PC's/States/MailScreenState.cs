using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailScreenState : State
{
    private Canvas MailScreen;
    public GameObject mailContentPanel; // Verwijs naar het paneel dat de mailinhoud toont
    private FSM<State> fSM;
    private Button BackButton;


    public MailScreenState(FSM<State> _fSM, Canvas _mailscreen) : base(_fSM)
    {
        MailScreen = _mailscreen;
        MailScreen.enabled = false;
        fSM = _fSM;
    }

    public override void OnEnter()
    {
        MailScreen.enabled = true;

        GetButtons();
    }

    public override void OnUpdate()
    {
        // Update inlogscherm logica, bijv. inlogpoging
    }

    public override void OnExit()
    {
        // Opruimen van inlogscherm
        MailScreen.enabled = false;

    }

    public void SwitchToHomeScreen()
    {
        fSM.SwitchState(typeof(HomeScreenState));
    }

    public void GetButtons()
    {
        // Initialiseer UI
        Button[] Buttons = MailScreen.GetComponentsInChildren<Button>(true);

        //check hier voor alle ui elementen die je verwacht
        foreach (var buttonInList in Buttons)
        {
            if (buttonInList.name == "BackButton")
            {
                BackButton = buttonInList;
                BackButton.onClick.AddListener(() => SwitchToHomeScreen());
            }
            else
            {
                Debug.LogWarning("Juiste Knop is niet gevonden voor: " + buttonInList.name + "Negeer dit als het klopt");
            }
        }
    }
}
