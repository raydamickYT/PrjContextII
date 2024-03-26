using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TaskContent : MonoBehaviour
{
    private ChoiceManager choiceManager;
    public Choice choice;
    private bool taskIsShowing;
    private Button btn;

    [Tooltip("Dit is waar je je tekst invoert")]
    public string TaskTekst;
    [Tooltip("Geef hier de prefab op voor de email")]
    private GameObject TaskInstanceBackup;
    void Awake()
    {
        this.choiceManager = ChoiceManager.instance;


        btn = GetComponent<Button>();
        if (btn == null)
        {
            Debug.LogWarning("button not assigned" + gameObject);
        }
        else
        {
            btn.onClick.AddListener(() => SwitchToTaskContentState());
        }

    }

    private void SwitchToTaskContentState()
    {
        if (choice != null)
        {
            ComputerManager.instance.SwitchComputerState(typeof(TaskContentScreenState), choice);
        }

        Destroy(this.gameObject);
    }

    public void CloseMail()
    {
        if (taskIsShowing && TaskInstanceBackup != null)
        {
            Destroy(TaskInstanceBackup);
            taskIsShowing = false;
            MailWipeSingletons.Instance.MailIsShowing = taskIsShowing;
        }
    }
}
