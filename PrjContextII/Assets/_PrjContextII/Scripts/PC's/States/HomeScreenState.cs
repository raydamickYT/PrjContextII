using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreenState : State
{
    private Canvas HomeScreen;

    public HomeScreenState(FSM<State> _fSM, Canvas _homeScrn) : base(_fSM)
    {
        HomeScreen = _homeScrn;
        HomeScreen.enabled = false;
    }

    public override void OnEnter()
    {
        //IMPORTANT: zet hier je canvas aan.
        if (!HomeScreen.enabled)
            HomeScreen.enabled = true;

        // Initialiseer inlogscherm UI
        Button[] Buttons = HomeScreen.GetComponentsInChildren<Button>(true);


        //check hier voor alle ui elementen die je verwacht
        foreach (var button in Buttons)
        {
            if (button.name == "Yes")
            {
                // nameInputField = inputField;
            }
            else if (button.name == "No")
            {
                // passwordInputField = inputField;
            }
            else
            {
                Debug.LogWarning("Juiste Knop is niet gevonden voor: " + button.name);
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
