using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskContent : MonoBehaviour
{
    private bool taskIsShowing;
    private Button btn;
    [SerializeField]
    [TextArea(3, 10)]
    [Tooltip("Dit is waar je je tekst invoert")]
    private string TaskTekst;
    [Tooltip("Geef hier de prefab op voor de email")]
    public GameObject TaskPrefab; // Verwijst naar het paneel dat de mailinhoud toont
    private GameObject TaskInstanceBackup;
    void Start()
    {
        btn = GetComponent<Button>();
        if (btn == null)
        {
            Debug.LogWarning("button not assigned" + gameObject);
        }
        else
        {
            btn.onClick.AddListener(() => OpenTask());
        }
    }
    private void OnEnable()
    {
        // MailWipeSingletons.Instance.SubscribeToAction(CloseMail);
    }

    private void OnDisable()
    {
        // MailWipeSingletons.Instance.UnsubscribeFromAction(CloseMail);
    }

    // Deze functie toont de inhoud van de mail
    public void OpenTask()
    {
        // Controleer of er al een instantie bestaat en vernietig deze indien nodig
        if (MailWipeSingletons.Instance.MailIsShowing)
        {
            MailWipeSingletons.Instance.TriggerAction();
            OpenTask();
        }
        else if (!taskIsShowing)
        {
            // Debug.Log("Mail geopend met ID: ");
            // Maak een instantie van de mail ScrollView prefab
            GameObject taskInstance = Instantiate(TaskPrefab, transform.parent.parent, false);
            TaskInstanceBackup = taskInstance;

            taskInstance.transform.position += new Vector3(0.06f, 0.08f, -0.03f);
            taskInstance.transform.localScale += new Vector3(500, 500);
            taskInstance.transform.SetParent(transform.parent.parent.parent); //niet de netste manier om de parent te veranderen...

            if (taskInstance.transform.Find("TaskContent").GetComponent<Text>() != null)
            {

                Text textComponent = taskInstance.transform.Find("TaskContent").GetComponent<Text>();

                textComponent.text = TaskTekst;

                // Pas de grootte van het Text-component aan op basis van de tekstlengte
                float preferredHeight = textComponent.preferredHeight;

                // Pas de grootte van de RectTransform aan op basis van de preferredWidth en preferredHeight
                RectTransform textRectTransform = textComponent.rectTransform;
                textRectTransform.sizeDelta = new Vector2(textRectTransform.sizeDelta.x, preferredHeight);

                // Pas de ankerpositie aan zodat de tekst aan de bovenkant blijft vastzitten
                textRectTransform.pivot = new Vector2(0.5f, 1f);
                textRectTransform.anchorMin = new Vector2(0.5f, 1f);
                textRectTransform.anchorMax = new Vector2(0.5f, 1f);

                //bool flag
                taskIsShowing = true;
                MailWipeSingletons.Instance.MailIsShowing = taskIsShowing;

            }
            else
            {
                Debug.LogWarning("geen tekst object gevonden in mail prefab");
            }

        }
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
