using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreenState : State
{
    private Button MailButton, mapButton, tasksButton;
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
    public void SwitchToTasks()
    {
        FSM.SwitchState(typeof(TasksScreen));
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
            switch (buttonInList.name)
            {
                case "Mail":
                    MailButton = buttonInList;
                    MailButton.onClick.AddListener(() => SwitchToMailScreen());
                    break;
                case "Map":
                    mapButton = buttonInList;
                    mapButton.onClick.AddListener(() => SwitchToMap());
                    break;
                case "Tasks":
                    tasksButton = buttonInList;
                    tasksButton.onClick.AddListener(() => SwitchToTasks()); //verander de bool naar true of false afhankelijk van welke impact de keuze moet hebben.
                    break;

                default:
                    Debug.LogWarning("Juiste Knop is niet gevonden voor: " + buttonInList.name + ". Negeer dit als het klopt.");
                    break;
            }
        }
    }
}
