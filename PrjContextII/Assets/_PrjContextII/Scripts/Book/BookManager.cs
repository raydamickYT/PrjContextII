using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookManager : MonoBehaviour
{
    public GameObject Book;
    public List<Text> Days = new();
    public int Index = 0;
    public bool temp = false;
    void Awake()
    {
        Days = FindObjectsStartingWith();
        Debug.Log(Days.Count);
    }
    // Start is called before the first frame update
    void Start()
    {
        // Index = GameManager.instance.currentDayIndex;
        // Days[1].gameObject.SetActive(true);
        // Days[1].text = GameManager.instance.Days[Index].book[Index].Day + "\n" + GameManager.instance.Days[Index].book[Index].Day; //beetje lang, maar belangrijk is om de dag goed in te vullen.
        GameManager.instance.AdvanceTheDay += AdvanceNextDay;
    }

    // Update is called once per frame
    void Update()
    {
        if (temp)
        {
            DisplayDays();
            temp = false;
        }
    }

    List<Text> FindObjectsStartingWith()
    {
        Text[] allObjects = Book.GetComponentsInChildren<Text>(true);
        List<Text> matchingObjects = new List<Text>();

        foreach (var obj in allObjects)
        {
            // Debug.Log(obj.name);
            obj.gameObject.SetActive(false);
            matchingObjects.Add(obj);
        }

        return matchingObjects;
    }
    public void DisplayDays()
    {
        if (Index <= Days.Count)
        {
            Text Day = Days[Index];
            Day.text = GameManager.instance.Days[Index].book.Day + "\n" + GameManager.instance.Days[Index].book.DayText; //beetje lang, maar belangrijk is om de dag goed in te vullen.
 
            RectTransform textRectTransform = Day.rectTransform;
            textRectTransform.pivot = new Vector2(textRectTransform.pivot.x, 0.8f);

            // Pas de grootte van het Text-component aan op basis van de tekstlengte
            float preferredHeight = Day.preferredHeight;

            // Pas de grootte van de RectTransform aan op basis van de preferredWidth en preferredHeight
            textRectTransform.sizeDelta = new Vector2(textRectTransform.sizeDelta.x, preferredHeight);


            Day.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("geen dagen meer");
        }
    }

    public bool AdvanceNextDay()
    {
        Index = GameManager.instance.currentDayIndex;
        if (GameManager.instance.currentDayIndex < Days.Count) // Controleer of er nog dagen over zijn
        {
            DisplayDays();

            return false;

            // Debug.Log("Overgegaan naar dag: " + (currentDayIndex + 1)); // Houd er rekening mee dat currentDayIndex 0-gebaseerd is
        }
        else
        {
            Debug.Log("Alle dagen voltooid. Spel is afgelopen of ga naar een eindscherm.");
            return true;
            // Hier kun je logica toevoegen om het spel te beëindigen of naar een eindscherm te gaan
        }
    }
}
