using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapScreenState : State
{

    public Button BackButton { get; private set; }

    public MapScreenState(FSM<State> _fSM, ComputerManager _man, Canvas _map) : base(_fSM)
    {
        base.ScreenCanvas = _map;
        ScreenCanvas.enabled = false;
        computerManager = _man;
    }

    public override void OnEnter()
    {
        ScreenCanvas.enabled = true;
        // computerManager.SwitchScreenMaterial(computerManager.MapScreenMaterial);
        computerManager.SwitchScreenMaterial(computerManager.GetMaterialByName("CameraView"));
        GetButtons();
    }

    public override void OnUpdate()
    {
        // Update inlogscherm logica, bijv. inlogpoging
    }

    public override void OnExit()
    {
        // Opruimen van inlogscherm
        // computerManager.SwitchScreenMaterial(computerManager.BackgroundScreenMaterial);
        computerManager.SwitchScreenMaterial(computerManager.GetMaterialByName("TempBackGround"));
        ScreenCanvas.enabled = false;
    }

    public void SwitchToHomeScreen()
    {
        FSM.SwitchState(typeof(HomeScreenState));
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
