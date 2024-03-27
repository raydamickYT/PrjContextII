using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class HomeScreenState : State
{
    private Button MailButton, mapButton, tasksButton;
    private ChoiceManager choiceManagerInstance;
    private bool isHoveringOverScreen = false, PopHasBeenShown = false;
    // private ComputerManager manager;

    public HomeScreenState(FSM<State> _fSM, Canvas _homeScrn) : base(_fSM)
    {
        base.ScreenCanvas = _homeScrn;
        // manager = _manager;
        ScreenCanvas.enabled = false;
        GameManager.instance.AdvanceTheDay += UpdateBool;
    }

    //TODO: De coupling met de choicemanager is niet correct. verbeter dit ajb

    public override void OnEnter()
    {
        // manager.SwitchScreenMaterial(manager.MapScreenMaterial);
        //IMPORTANT: zet hier je canvas aan.
        if (!ScreenCanvas.enabled)
            ScreenCanvas.enabled = true;

        GetButtons();
        computerManager.SwitchScreenMaterial(computerManager.GetMaterialByName("HomeScreen"));
        if (!PopHasBeenShown)
        {
            PlaySFX();
        }

    }

    public void PlaySFX()
    {
        if (GameManager.instance.currentDayIndex > 0)
        {
            // check hoe de speler er voor staat
            if (GameManager.instance.GoodOrBadMeter < -0.2)
            {
                MailAudioManager.Instance.PlayMail?.Invoke(false); //geef false mee omdat speler het slecht doet.
            }
            else
            {
                MailAudioManager.Instance.PlayMail?.Invoke(true); //geef true mee omdat speler het goed doet.
            }
        }
        else //tussen-0.2 en 0.2 als het goed is.
        {
            MailAudioManager.Instance.PlayMail?.Invoke(true); //geef true mee omdat op de eerste dag de mail sowieso goed is.
        }
        PopHasBeenShown = true;
    }
    private void UpdateBool()
    {
        PopHasBeenShown = false;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
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
            {
                if (isHoveringOverScreen)
                    Cursor.SetCursor(PS.CursorArrow, Vector2.zero, CursorMode.ForceSoftware);
                isHoveringOverScreen = false;
            }
        }
    }

    public override void OnExit()
    {
        // Opruimen van scherm
        ScreenCanvas.enabled = false;
        Cursor.SetCursor(PS.CursorArrow, Vector2.zero, CursorMode.ForceSoftware);

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

        //vind de popup 1 keer
        if (MailButton == null)
        {
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
}
