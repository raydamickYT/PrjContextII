using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapScreenState : State
{
    private Canvas MapScreen;
    private FSM<State> fSM;
    private ComputerManager computerManager;

    public Button BackButton { get; private set; }

    public MapScreenState(FSM<State> _fSM, ComputerManager _man, Canvas _map) : base(_fSM)
    {
        MapScreen = _map;
        fSM = _fSM;
        computerManager = _man;
    }

    public override void OnEnter()
    {
        MapScreen.enabled = true;
        computerManager.SwitchScreenMaterial(computerManager.MapScreenMaterial);
        GetButtons();
    }

    public override void OnUpdate()
    {
        // Update inlogscherm logica, bijv. inlogpoging
    }

    public override void OnExit()
    {
        // Opruimen van inlogscherm
        computerManager.SwitchScreenMaterial(computerManager.BackgroundScreenMaterial);
        MapScreen.enabled = false;
    }

    public void SwitchToHomeScreen()
    {
        fSM.SwitchState(typeof(HomeScreenState));
    }

    public void GetButtons()
    {
        // Initialiseer UI
        Button[] Buttons = MapScreen.GetComponentsInChildren<Button>(true);

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
