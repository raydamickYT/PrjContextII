using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreenState : State
{
    private Button YesButton, NoButton;
    private ChoiceManager choiceManagerInstance;
    // private ComputerManager manager;

    public HomeScreenState(FSM<State> _fSM, Canvas _homeScrn, ComputerManager _manager) : base(_fSM)
    {
        base.ScreenCanvas = _homeScrn;
        // manager = _manager;
        ScreenCanvas.enabled = false;
    }

    //TODO: De coupling met de choicemanager is niet correct. verbeter dit ajb

    public override void OnEnter()
    {
        // manager.SwitchScreenMaterial(manager.MapScreenMaterial);
        //IMPORTANT: zet hier je canvas aan.
        if (!ScreenCanvas.enabled)
            ScreenCanvas.enabled = true;

        GetButtons();

    }


    public override void OnUpdate()
    {

    }

    public void SwitchToMailScreen()
    {
        FSM.SwitchState(typeof(MailScreenState));
    }

    public void SwitchToMap()
    {
        FSM.SwitchState(typeof(MapScreenState));
    }

    public override void OnExit()
    {
        // Opruimen van scherm
        ScreenCanvas.enabled = false;
    }
    public void GetButtons()
    {
        // Initialiseer UI
        Button[] Buttons = ScreenCanvas.GetComponentsInChildren<Button>(true);

        if (ChoiceManager.instance != null)
        {
            choiceManagerInstance = ChoiceManager.instance;
        }
        else
        {
            Debug.LogWarning("ChoiceManager Instance bestaat niet");
        }


        //check hier voor alle ui elementen die je verwacht
        foreach (var buttonInList in Buttons)
        {
            if (buttonInList.name == "Mail")
            {
                YesButton = buttonInList;
                YesButton.onClick.AddListener(() => SwitchToMailScreen());
            }
            else if (buttonInList.name == "Map")
            {
                NoButton = buttonInList;
                NoButton.onClick.AddListener(() => SwitchToMap());
            }
            else
            {
                Debug.LogWarning("Juiste Knop is niet gevonden voor: " + buttonInList.name + "\nCheck of de namen nog kloppen");
            }
        }
    }
}
