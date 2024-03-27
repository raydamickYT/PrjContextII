using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager Instance;
    public List<SubtitleInfo> subtitleInfos = new();
    public Text SubText;
    public Image TekstVak;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        StopSubtitles();
    }

    public void PlaySubtitle(string name)
    {

        foreach (var item in subtitleInfos)
        {
            if (item.SubName == name)
            {
                if (name == "Intro")
                {
                    StartCoroutine(IntroWaiting(item));
                }
                else
                {
                    Debug.Log(SubText.gameObject.activeSelf);
                    TekstVak.gameObject.SetActive(true);

                    DisplayText(item.Subtitles);
                }
            }
        }
    }

    public void StopSubtitles()
    {
        TekstVak.gameObject.SetActive(false);
    }
    public void DisplayText(string Text)
    {
        SubText.text = Text; //wann hij de juiste speelt
        float preferredHeight = SubText.preferredHeight;

        // Pas de grootte van de RectTransform aan op basis van de preferredWidth en preferredHeight
        RectTransform textRectTransform = SubText.rectTransform;
        textRectTransform.sizeDelta = new Vector2(textRectTransform.sizeDelta.x, preferredHeight);

        TekstVak.rectTransform.sizeDelta = new Vector2(textRectTransform.sizeDelta.x + 20, textRectTransform.sizeDelta.y + 20);
    }
    IEnumerator IntroWaiting(SubtitleInfo subtitleInfo)
    {
        yield return new WaitForSeconds(6.5f);
        TekstVak.gameObject.SetActive(true);
        DisplayText(subtitleInfo.Subtitles);
    }
}

[Serializable]
public class SubtitleInfo
{
    public string SubName;
    [TextArea]
    public string Subtitles;
}