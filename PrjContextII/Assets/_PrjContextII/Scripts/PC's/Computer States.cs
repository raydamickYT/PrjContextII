using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoginState : State
{
    public InputField NameInputField; //0
    public InputField PasswordInputField; //1
    public static int inputSelect = 0;

    public Button EnterButton;
    private Canvas loginScreen;
    private InputField[] inputFields;

    public LoginState(FSM<State> _fSM, Canvas _login) : base(_fSM)
    {
        loginScreen = _login;
    }

    public override void OnEnter()
    {
        //TODO: je was bezig met checken of je variabelen kan opslaan zodat je ze niet nog een keer hoeft te vinden
        // wanneer je dit scherm weer opent
        if (inputFields == null)
        {
            // Initialiseer inlogscherm UI
            inputFields = loginScreen.GetComponentsInChildren<InputField>(true);

            foreach (var inputField in inputFields)
            {
                if (inputField.name == "Name")
                {
                    NameInputField = inputField;
                }
                else if (inputField.name == "Password")
                {
                    PasswordInputField = inputField;
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

    public override void OnExit()
    {
        // Opruimen van inlogscherm
    }
    public static void UserSelectedPC(LoginState instance)
    {
        instance.NameInputField.Select();

    }
    public void SelectInputField()
    {
        switch (inputSelect)
        {
            case 0:
                NameInputField.Select();
                break;
            case 1:
                PasswordInputField.Select();
                break;
        }
    }

    private void OnEnterPressed()
    {
        string ComputerPassword = "123";
        string playerName = NameInputField.text;
        string password = PasswordInputField.text;

        if (password == ComputerPassword)
        {
            Debug.Log($"Inlogpoging met Naam: {playerName} en Wachtwoord: {password}, SUCCES");

        }
        else
        {
            Debug.Log($"Inlogpoging met Naam: {playerName} en Wachtwoord: {password}");
        }


        // Implementeer hier logica voor het verifiëren van de inloggegevens
        // en schakel over naar de volgende staat indien succesvol.
    }
}

public class MapState : State
{
    private Canvas MapScreen;

    public MapState(FSM<State> _fSM) : base(_fSM) { }

    public override void OnEnter()
    {

        // Initialiseer inlogscherm UI
        InputField[] inputFields = MapScreen.GetComponentsInChildren<InputField>(true);


        //check hier voor alle ui elementen die je verwacht
        foreach (var inputField in inputFields)
        {
            if (inputField.name == "Name")
            {
                // nameInputField = inputField;
            }
            else if (inputField.name == "Password")
            {
                // passwordInputField = inputField;
            }
            else
            {
                Debug.LogWarning("Juiste Knop is niet gevonden voor: " + inputField.name);
            }
        }
    }

    public override void OnUpdate()
    {
        // Update inlogscherm logica, bijv. inlogpoging
    }

    public override void OnExit()
    {
        // Opruimen van inlogscherm
    }
}

public class TasksClass : State
{
    private Canvas TasksScreen;

    public TasksClass(FSM<State> _fSM) : base(_fSM) { }

    public override void OnEnter()
    {

        // Initialiseer inlogscherm UI
        InputField[] inputFields = TasksScreen.GetComponentsInChildren<InputField>(true);

        foreach (var inputField in inputFields)
        {
            if (inputField.name == "Name")
            {
                // nameInputField = inputField;
            }
            else if (inputField.name == "Password")
            {
                // passwordInputField = inputField;
            }
            else
            {
                Debug.LogWarning("Juiste Knop is niet gevonden voor: " + inputField.name);
            }
        }
    }

    public override void OnUpdate()
    {
        // Update inlogscherm logica, bijv. inlogpoging

        FSM.SwitchState(typeof(MapState));
    }

    public override void OnExit()
    {
        // Opruimen van inlogscherm
    }
}

public class MailState : State
{
    private Canvas MailScreen;

    public MailState(FSM<State> _fSM) : base(_fSM) { }

    public override void OnEnter()
    {

        // Initialiseer inlogscherm UI
        InputField[] inputFields = MailScreen.GetComponentsInChildren<InputField>(true);

        foreach (var inputField in inputFields)
        {
            if (inputField.name == "Name")
            {
                // nameInputField = inputField;
            }
            else if (inputField.name == "Password")
            {
                // passwordInputField = inputField;
            }
            else
            {
                Debug.LogWarning("Juiste Knop is niet gevonden voor: " + inputField.name);
            }
        }
    }

    public override void OnUpdate()
    {
        // Update inlogscherm logica, bijv. inlogpoging
    }

    public override void OnExit()
    {
        // Opruimen van inlogscherm
    }
}