using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class LoginState : State
{
    private InputField[] inputFields;
    public InputField NameInputField; //0
    public InputField PasswordInputField; //1
    public static int inputSelect = 0;

    public Button EnterButton;
    public bool IsActive = false;
    private Camera mainCam;
    private LayerMask screenLayerMask;
    public Texture2D cursorArrow, cursorHand;
    private bool isHoveringOverScreen;


    public LoginState(FSM<State> _fSM, Canvas _login) : base(_fSM)
    {
        base.ScreenCanvas = _login;
        ScreenCanvas.enabled = false;
    }
    public void Init()
    {
        if (PS != null)
        {
            mainCam = PS.MainCam;
            screenLayerMask = PS.ComputerLayerMask;
            cursorArrow = PS.CursorArrow;
            cursorHand = PS.CursorHand;
        }
        else
        {
            Debug.Log("kan geen ps vinden");
        }
    }

    public override void OnEnter()
    {
        if (mainCam == null)
        {
            Init();
        }
        //IMPORTANT: zet hier je canvas aan.
        if (!ScreenCanvas.enabled)
            ScreenCanvas.enabled = true;
        IsActive = true;

        //TODO: je was bezig met checken of je variabelen kan opslaan zodat je ze niet nog een keer hoeft te vinden
        // wanneer je dit scherm weer opent
        if (inputFields == null)
        {
            // Initialiseer inlogscherm UI
            inputFields = ScreenCanvas.GetComponentsInChildren<InputField>(true);

            foreach (var inputField in inputFields)
            {
                if (inputField.name == "Name")
                {
                    NameInputField = inputField;
                }
                else if (inputField.name == "Password")
                {
                    PasswordInputField = inputField;
                    PasswordInputField.contentType = InputField.ContentType.Password;
                }
                else
                {
                    Debug.LogWarning("Juiste Knop is niet gevonden voor: " + inputField.name);
                }
            }
        }
        else
        {
            Debug.Log("hij is niet leeg");
        }


        computerManager.SwitchScreenMaterial(computerManager.GetMaterialByName("LoginScreen"));

        //check even of dit scherm al niet eerder al deze elementen heeft gevonden.


    }

    public override void OnUpdate()
    {
        // Update inlogscherm logica, bijv. inlogpoging
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnEnterPressed();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inputSelect++;
            if (inputSelect > 1) inputSelect = 0;
            SelectInputField();
        }
    }
    public override void OnLateUpdate()
    {
        RayCastToUI();
    }

    void RayCastToUI()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, screenLayerMask))
        {
            // Debug.Log("Geraakt object: " + hit.collider.gameObject.name);
            if (!isHoveringOverScreen)
            {
                Cursor.SetCursor(cursorHand, Vector2.zero, CursorMode.ForceSoftware);
                isHoveringOverScreen = true;
            }
        }
        else
        {
            if (isHoveringOverScreen)
            {
                Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
                isHoveringOverScreen = false;
            }
        }
    }
    public static void UserSelectedPC(LoginState instance)
    {
        instance.NameInputField.Select();

    }
    public void SelectInputField()
    {
        PasswordInputField.Select();
        // switch (inputSelect)
        // {
        //     case 0:
        //         NameInputField.Select();
        //         break;
        //     case 1:
        //         break;
        // }
    }

    private void OnEnterPressed()
    {
        string ComputerPassword = "123";
        string playerName = NameInputField.text;
        string password = PasswordInputField.text;

        if (password == ComputerPassword)
        {
            Debug.Log($"Inlogpoging met Naam: {playerName} en Wachtwoord: {password}, SUCCES");
            FSM.SwitchState(typeof(HomeScreenState));

        }
        else
        {
            Debug.Log($"Inlogpoging met Naam: {playerName} en Wachtwoord: {password}");
        }


        // Implementeer hier logica voor het verifiëren van de inloggegevens
        // en schakel over naar de volgende staat indien succesvol.
    }
    
    public override void OnExit()
    {
        // Opruimen van inlogscherm
        // loginScreen.GetComponent<GameObject>().SetActive(false);

        //IMPORTANT: als je van scherm wisselt, dan zet je hier je canvas uit.
        ScreenCanvas.enabled = false;
        IsActive = false;
        Cursor.visible = true;


    }
}


