using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TaskContentScreenState : State, IStateWithExtraInfo
{
    private Button BackButton, YesButton, NoButton;
    private Text contentText, contentName;
    private GameObject thisObject;
    private Choice currentChoice;
    private bool isHoveringOverScreen;

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
                Cursor.SetCursor(PS.CursorArrow, Vector2.zero, CursorMode.ForceSoftware);
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
                    if (GameManager.instance.currentDayIndex > 6) //moet op dag 7 gebeuren
                    {
                        NoButton.GetComponentInChildren<Text>().text = "Yes";
                        NoButton.onClick.AddListener(() => ExecuteChoice(true)); //verander de bool naar true of false afhankelijk van welke impact de keuze moet hebben.

                    }
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
            Debug.LogError("verkeerd type extra info is doorgegeven aan tasksscreen");
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
            contentName = thisObject.transform.Find("TabName").GetComponent<Text>();

            // Text textComponent = mailScrollViewInstance.transform.Find("EmailContent").GetComponent<Text>();
            if (contentText != null)
            {
                contentText.text = currentChoice.choiceText;
                // Pas de grootte van het Text-component aan op basis van de tekstlengte
                float preferredHeight = contentText.preferredHeight;

                // Pas de grootte van de RectTransform aan op basis van de preferredWidth en preferredHeight
                RectTransform textRectTransform = contentText.rectTransform;
                textRectTransform.sizeDelta = new Vector2(textRectTransform.sizeDelta.x, preferredHeight);
                // Pas de ankerpositie aan zodat de tekst aan de bovenkant blijft vastzitten
                textRectTransform.pivot = new Vector2(0.5f, 1f);
                textRectTransform.anchorMin = new Vector2(0.5f, 1f);
                textRectTransform.anchorMax = new Vector2(0.5f, 1f);
            }

            if (contentName != null)
            {
                contentName.text = currentChoice.choiceName;
            }
        }
    }

    public override void OnExit()
    {
        // Opruimen van inlogscherm
        thisObject.SetActive(false);
    }
}
