using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailScreenState : State
{
    public GameObject mailContentPanel; // Verwijs naar het paneel dat de mailinhoud toont
    private Button BackButton;
    private bool ButtonsActive = false;
    private List<Button> mails = new();

    public MailScreenState(FSM<State> _fSM, Canvas _mailscreen) : base(_fSM)
    {
        base.ScreenCanvas = _mailscreen;
        ScreenCanvas.enabled = false;
    }

    public override void OnEnter()
    {
        ScreenCanvas.enabled = true;
        if (!MailManager.instance.IsShowingButtonsForToday)
        {
            mails = MailManager.instance.DisplayMailButtons();
            MailManager.instance.IsShowingButtonsForToday = true;
        }
        computerManager.SwitchScreenMaterial(computerManager.GetMaterialByName("MailScreen"));
        GetButtons();
    }

    public override void OnUpdate()
    {
        // Update inlogscherm logica, bijv. inlogpoging
    }

    public override void OnExit()
    {
        // Opruimen van inlogscherm
        ScreenCanvas.enabled = false;

    }

    public void SwitchToHomeScreen()
    {
        FSM.SwitchState(typeof(HomeScreenState));
        MailWipeSingletons.Instance.TriggerAction();
    }

    public void GetButtons()
    {
        // Initialiseer UI
        Button[] Buttons = ScreenCanvas.GetComponentsInChildren<Button>(true);

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
