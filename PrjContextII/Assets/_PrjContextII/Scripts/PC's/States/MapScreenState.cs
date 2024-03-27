using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MapScreenState : State
{
    public Button BackButton { get; private set; }
    public GameObject MapScreen;
    private bool HasPlayedSound = false;


    public MapScreenState(FSM<State> _fSM, Canvas _map) : base(_fSM)
    {
        base.ScreenCanvas = _map;
        ScreenCanvas.enabled = false;
        if (MapScreen == null)
        {
            MapScreen = ScreenCanvas.GetComponentInChildren<MapIdentifier>().gameObject;
            MapScreen.SetActive(false);
        }
    }

    public override void OnEnter()
    {
        MapScreen.SetActive(true);
        ScreenCanvas.enabled = true;
        // computerManager.SwitchScreenMaterial(computerManager.MapScreenMaterial);
        computerManager.SwitchScreenMaterial(computerManager.GetMaterialByName("MapScreen"));
      if (!HasPlayedSound && VoiceOvers.Instance != null)
        {
            VoiceOvers.Instance.Playmap();
        }
        GetButtons();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        // Opruimen van inlogscherm
        // computerManager.SwitchScreenMaterial(computerManager.BackgroundScreenMaterial);
        ScreenCanvas.enabled = false;
        MapScreen.SetActive(false);
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
