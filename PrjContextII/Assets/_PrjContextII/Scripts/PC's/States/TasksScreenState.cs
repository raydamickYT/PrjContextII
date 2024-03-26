using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TasksScreen : State
{
    private bool isShowingButtons = false;
    private Button BackButton;
    private List<Button> Tasks = new();
    private bool isHoveringOverScreen;

    public TasksScreen(FSM<State> _fSM, Canvas _tasks) : base(_fSM)
    {
        base.ScreenCanvas = _tasks;
        ScreenCanvas.enabled = false;

    }

    public override void OnEnter()
    {
        if (!ScreenCanvas.enabled)
            ScreenCanvas.enabled = true;

        //maak hier een reference aan de gamemanager
        if (!ChoiceManager.instance.ChoicesLeft)
        {
            Debug.Log("we zijn door de choices heen vandaag");

            isShowingButtons = GameManager.instance.Result; //verandert dus alleen als de result wordt geupdate in de GameManager.
            return;
            // isShowingButtons = true;

            //TIJDELIJK
            // MailManager.instance.IsShowingButtons = GameManager.instance.Result;
        }
        else if (!isShowingButtons)
        {
            Tasks = ChoiceManager.instance.DisplayTaskButtons();
            isShowingButtons = true;
        }
        computerManager.SwitchScreenMaterial(computerManager.GetMaterialByName("TaskScreen"));
        GetButtons();
    }
    public override void OnLateUpdate()
    {
        RayCastToUI();
    }

    void RayCastToUI()
    {
        Ray ray = PS.MainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, PS.ComputerLayerMask))
        {
            // Debug.Log("Geraakt object: " + hit.collider.gameObject.name);
            if (!isHoveringOverScreen)
            {
                Cursor.SetCursor(PS.ComputerArrow, Vector2.zero, CursorMode.ForceSoftware);
                isHoveringOverScreen = true;
            }
        }
        else
        {
            if (isHoveringOverScreen)
            {
                Cursor.SetCursor(PS.CursorArrow, Vector2.zero, CursorMode.ForceSoftware);
                isHoveringOverScreen = false;
            }
        }
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
    
    public override void OnExit()
    {
        // Opruimen van inlogscherm
        if (ScreenCanvas.enabled)
            ScreenCanvas.enabled = false;
    }
}
