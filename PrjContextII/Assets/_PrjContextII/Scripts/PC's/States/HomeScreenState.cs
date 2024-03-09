using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreenState : State
{
    private Button YesButton, NoButton;
    private Canvas HomeScreen;
    private ChoiceManager choiceManagerInstance;
    private ComputerManager manager;
    private FSM<State> fSM;

    public HomeScreenState(FSM<State> _fSM, Canvas _homeScrn, ComputerManager _manager) : base(_fSM)
    {
        fSM = _fSM;
        HomeScreen = _homeScrn;
        HomeScreen.enabled = false;
        manager = _manager;
    }

    //TODO: De coupling met de choicemanager is niet correct. verbeter dit ajb

    public override void OnEnter()
    {
        // manager.SwitchScreenMaterial(manager.MapScreenMaterial);
        //IMPORTANT: zet hier je canvas aan.
        if (!HomeScreen.enabled)
            HomeScreen.enabled = true;

        GetButtons();
    }


    public override void OnUpdate()
    {
        
    }

    public void SwitchToMailScreen()
    {
        fSM.SwitchState(typeof(MailScreenState));
    }

    public void SwiwtchToTasks()
    {

    }

    public override void OnExit()
    {
        // Opruimen van scherm
        HomeScreen.enabled = false;
    }
    public void GetButtons()
    {
        // Initialiseer UI
        Button[] Buttons = HomeScreen.GetComponentsInChildren<Button>(true);

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
            else if (buttonInList.name == "Tasks")
            {
                NoButton = buttonInList;
                NoButton.onClick.AddListener(() => SwiwtchToTasks());
            }
            else
            {
                Debug.LogWarning("Juiste Knop is niet gevonden voor: " + buttonInList.name);
            }
        }
    }
}
