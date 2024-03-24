using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TaskContentScreenState : State, IStateWithExtraInfo
{
    private Button BackButton, YesButton, NoButton;
    private Text contentText;
    private GameObject thisObject;
    private Choice currentChoice;
    public TaskContentScreenState(FSM<State> _fSM, GameObject gameObject) : base(_fSM)
    {
        thisObject = gameObject;
        base.ScreenCanvas = thisObject.GetComponent<Canvas>();
        thisObject.SetActive(false);
    }
    public override void OnEnter()
    {
        thisObject.SetActive(true);
        SetupCanvas();
        GetButtons();
    }

    public override void OnUpdate()
    {
        // Update inlogscherm logica, bijv. inlogpoging
    }

    public override void OnExit()
    {
        // Opruimen van inlogscherm
        thisObject.SetActive(false);
    }

    public void GetButtons()
    {
        // Initialiseer UI
        Button[] Buttons = ScreenCanvas.GetComponentsInChildren<Button>(true);

        //check hier voor alle ui elementen die je verwacht
        foreach (var buttonInList in Buttons)
        {
            switch (buttonInList.name)
            {
                case "BackButton":
                    BackButton = buttonInList;
                    BackButton.onClick.AddListener(() => SwitchToTaskScreen());
                    break;
                case "Yes":
                    YesButton = buttonInList;
                    YesButton.onClick.AddListener(() => ExecuteChoice(true));
                    break;
                case "No":
                    NoButton = buttonInList;
                    NoButton.onClick.AddListener(() => ExecuteChoice(false)); //verander de bool naar true of false afhankelijk van welke impact de keuze moet hebben.
                    break;

                default:
                    Debug.LogWarning("Juiste Knop is niet gevonden voor: " + buttonInList.name + ". Negeer dit als het klopt.");
                    break;
            }

        }
    }

    public void InitializeWithExtraInfo(object extraInfo)
    {
        if (extraInfo is Choice choice)
        {
            currentChoice = choice;
            Debug.Log("het werkt");
        }
        else
        {
            Debug.LogError("verkeerd type extra info is doorgegeven aan tasksscren");
        }
    }

    public void ExecuteChoice(bool Choice)
    {
        ChoiceManager.instance.MakeChoice(Choice); //true is yes, false is no.
        //omdat je na de choice niet meer deze choice kan uitvoeren, ga je terug naar de task screen.
        FSM.SwitchState(typeof(TasksScreen));
    }
    public void SwitchToTaskScreen()
    {
        FSM.SwitchState(typeof(TasksScreen));
    }


    private void SetupCanvas()
    {
        if (currentChoice != null)
        {
            contentText = thisObject.transform.Find("TaskContent").GetComponent<Text>();

            contentText.text = currentChoice.choiceText;
        }
    }
}
