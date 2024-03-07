using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailContent : MonoBehaviour
{
    private Button btn;
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
            Destroy(transform.parent.parent.Find(mailScrollViewPrefab.name + "(Clone)").gameObject);
        }
        else
        {
            // Maak een instantie van de mail ScrollView prefab
            GameObject mailScrollViewInstance = Instantiate(mailScrollViewPrefab, transform.parent.parent, false);

            // Optioneel: Pas de positie, grootte of andere eigenschappen aan
            mailScrollViewInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.right; // Bijvoorbeeld, centreer het
            mailScrollViewInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 300); // Pas de grootte aan
            mailScrollViewInstance.transform.Find("Content/Text").GetComponent<Text>().text = "Hier komt de specifieke mailinhoud...";

        }


        // Stel de mailinhoud in (je zult logica moeten toevoegen om de specifieke inhoud te laden gebaseerd op de geklikte button)
        // Voorbeeld:
    }
}
