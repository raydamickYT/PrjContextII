using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreenState : State
{
    private Button YesButton, NoButton;
    private Canvas HomeScreen;
    private ChoiceManager choiceManagerInstance;
    private ComputerManager manager;

    public HomeScreenState(FSM<State> _fSM, Canvas _homeScrn, ComputerManager _manager) : base(_fSM)
    {
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

        // Initialiseer inlogscherm UI
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
            if (buttonInList.name == "Yes")
            {
                YesButton = buttonInList;
                YesButton.onClick.AddListener(() => choiceManagerInstance.MakeChoice(true));
            }
            else if (buttonInList.name == "No")
            {
                NoButton = buttonInList;
                NoButton.onClick.AddListener(() => choiceManagerInstance.MakeChoice(false));
            }
            else
            {
                Debug.LogWarning("Juiste Knop is niet gevonden voor: " + buttonInList.name);
            }
        }
    }

    public override void OnUpdate()
    {
        // Update inlogscherm logica, bijv. inlogpoging
    }

    public override void OnExit()
    {
        // Opruimen van inlogscherm
        HomeScreen.enabled = false;
    }
}
