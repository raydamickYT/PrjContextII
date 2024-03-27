using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Image FadeImage;
    public float FadeTime = 2;
    public Button StartButton;
    // Start is called before the first frame update
    void Start()
    {
        if (StartButton != null)
        {
            StartButton.onClick.AddListener(LoadGame);
        }
        FadeImage.GetComponent<Transform>().gameObject.SetActive(false);
    }

    public void LoadGame()
    {
        FadeImage.GetComponent<Transform>().gameObject.SetActive(true);
        StartCoroutine(FadeToBlack(2));
        Debug.Log("hoi");
    }

    IEnumerator FadeToBlack(float WaitTime)
    {
        Color imageColor = FadeImage.color;
        float alphaValue = imageColor.a;
        while (alphaValue < 1)
        {
            alphaValue += Time.deltaTime * FadeTime;
            FadeImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            yield return null;
        }
        yield return new WaitForSeconds(WaitTime);
        SceneManager.LoadScene("Scene Kamer");
    }
}
