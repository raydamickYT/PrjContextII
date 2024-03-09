using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class MailContent : MonoBehaviour
{
    private Button btn;
    [SerializeField]
    [TextArea(3, 10)]
    [Tooltip("Dit is waar je je tekst invoert")]
    private string MailText;
    [Tooltip("Geef hier de prefab op voor de email")]
    public GameObject mailScrollViewPrefab; // Verwijs naar het paneel dat de mailinhoud toont

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
    }

    // Deze functie toont de inhoud van de mail
    public void OpenMail()
    {
        // Logica om de inhoud van de mail te laden gebaseerd op mailId
        // Bijvoorbeeld: mailContentPanel.SetActive(true);
        Debug.Log("Mail geopend met ID: ");

        //----------------------------
        // Controleer of er al een instantie bestaat en vernietig deze indien nodig
        if (transform.parent.parent.Find(mailScrollViewPrefab.name + "(Clone)"))
        {
            Debug.Log("hij bestaat al");
            Destroy(transform.parent.parent.Find(mailScrollViewPrefab.name + "(Clone)").gameObject);
        }
        else
        {
            // Maak een instantie van de mail ScrollView prefab
            GameObject mailScrollViewInstance = Instantiate(mailScrollViewPrefab, transform.parent.parent, false);
            // mailScrollViewInstance.name = mailScrollViewInstance.name + "(Clone)";

            // Optioneel: Pas de positie, grootte of andere eigenschappen aan
            mailScrollViewInstance.transform.position += new Vector3(0.18f, -0.13f, -0.03f); // Bijvoorbeeld, centreer het
            mailScrollViewInstance.transform.SetParent(transform.parent.parent.parent); //niet de netste manier om de parent te veranderen...
            // mailScrollViewInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 300); // Pas de grootte aan
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
            }
            else
            {
                Debug.LogWarning("geen tekst object gevonden in mail prefab");
            }

        }


        // Stel de mailinhoud in (je zult logica moeten toevoegen om de specifieke inhoud te laden gebaseerd op de geklikte button)
        // Voorbeeld:
    }
}
