using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailScreenState : State
{
    public GameObject mailContentPanel; // Verwijs naar het paneel dat de mailinhoud toont
    private Button BackButton;
    private bool HasPlayedSound = false;
    private List<Button> mails = new();
    private bool isHoveringOverScreen;

    public MailScreenState(FSM<State> _fSM, Canvas _mailscreen) : base(_fSM)
    {
        base.ScreenCanvas = _mailscreen;
        ScreenCanvas.enabled = false;
    }

    public override void OnEnter()
    {
        ScreenCanvas.enabled = true;
        if (!MailManager.instance.IsShowingButtonsForToday)
        {
            mails = MailManager.instance.DisplayMailButtons();
            MailManager.instance.IsShowingButtonsForToday = true;
        }
        computerManager.SwitchScreenMaterial(computerManager.GetMaterialByName("MailScreen"));

        if (!HasPlayedSound && VoiceOvers.Instance != null)
        {
            VoiceOvers.Instance.PlayMail();
        }
        GetButtons();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
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
        MailWipeSingletons.Instance.TriggerAction();
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
        ScreenCanvas.enabled = false;

    }
}
