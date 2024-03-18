using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class MailContent : MonoBehaviour
{
    private bool mailIsShowing;
    private Button btn;
    [SerializeField]
    [TextArea(3, 10)]
    [Tooltip("Dit is waar je je tekst invoert")]
    private string MailText;
    [Tooltip("Geef hier de prefab op voor de email")]
    public GameObject MailScrollViewPrefab; // Verwijst naar het paneel dat de mailinhoud toont
    private GameObject mailScrollViewBackup;
    public Mail mail;
    void Start()
    {
        btn = GetComponent<Button>();
        if (btn == null)
        {
            Debug.LogWarning("button not assigned" + gameObject);
        }
        else
        {
            btn.onClick.AddListener(() => OpenMail());
        }

        if (MailWipeSingletons.Instance != null)
        {
            MailWipeSingletons.Instance.SubscribeToAction(CloseMail);
        }
        else
        {
            Debug.Log("mailwipesingletons bestaat nog niet");
        }
    }
    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        MailWipeSingletons.Instance.UnsubscribeFromAction(CloseMail);
    }

    // Deze functie toont de inhoud van de mail
    public void OpenMail()
    {
        // Logica om de inhoud van de mail te laden gebaseerd op mailId
        // Bijvoorbeeld: mailContentPanel.SetActive(true);


        //----------------------------
        // Controleer of er al een instantie bestaat en vernietig deze indien nodig
        if (MailWipeSingletons.Instance.MailIsShowing)
        {
            MailWipeSingletons.Instance.TriggerAction();
            OpenMail();
        }
        else if (!mailIsShowing)
        {
            // Debug.Log("Mail geopend met ID: ");
            // Maak een instantie van de mail ScrollView prefab
            GameObject mailScrollViewInstance = Instantiate(MailScrollViewPrefab, transform.parent.parent, false);
            mailScrollViewBackup = mailScrollViewInstance;

            mailScrollViewInstance.transform.position += new Vector3(0.18f, -0.08f, -0.03f); // Bijvoorbeeld, centreer het
            mailScrollViewInstance.transform.SetParent(transform.parent.parent.parent); //niet de netste manier om de parent te veranderen...

            if (mailScrollViewInstance.transform.Find("EmailContent").GetComponent<Text>() != null)
            {

                Text textComponent = mailScrollViewInstance.transform.Find("EmailContent").GetComponent<Text>();

                textComponent.text = MailText;

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
                mailIsShowing = true;
                MailWipeSingletons.Instance.MailIsShowing = mailIsShowing;

            }
            else
            {
                Debug.LogWarning("geen tekst object gevonden in mail prefab");
            }

        }
    }
    public void CloseMail()
    {
        if (mailIsShowing && mailScrollViewBackup != null)
        {
            Destroy(mailScrollViewBackup);
            mailIsShowing = false;
            MailWipeSingletons.Instance.MailIsShowing = mailIsShowing;
        }
    }
}
