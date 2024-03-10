using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TasksScreen : State
{
    private Canvas MapScreen;
    private bool isShowingButtons = false;
    private Button BackButton;
    private List<Button> Tasks = new();
    public TasksScreen(FSM<State> _fSM, Canvas _tasks) : base(_fSM)
    {
        base.ScreenCanvas = _tasks;
        ScreenCanvas.enabled = false;
    }

    public override void OnEnter()
    {
        if (!ScreenCanvas.enabled)
            ScreenCanvas.enabled = true;

        if (!ChoiceManager.instance.ChoicesLeft)
        {
            // Debug.Log("we zijn door de choices heen vandaag");
            isShowingButtons = false;
            // ChoiceManager.instance.currentDayIndex += 1;
        } 
        if (!isShowingButtons)
        {
            Tasks = ComputerManager.instance.DisplayTaskButtons();
            isShowingButtons = true;
        }
        // GetButtons();
    }

    public override void OnUpdate()
    {
        // Update inlogscherm logica, bijv. inlogpoging

        // FSM.SwitchState(typeof(MapScreenState));
    }

    public override void OnExit()
    {
        // Opruimen van inlogscherm
        if (ScreenCanvas.enabled)
            ScreenCanvas.enabled = false;
    }

    public void SwitchToHomeScreen()
    {
        FSM.SwitchState(typeof(HomeScreenState));

    }

    public void SwitchToTask()
    {

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
