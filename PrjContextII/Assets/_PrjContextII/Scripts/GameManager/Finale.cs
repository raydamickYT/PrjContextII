using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finale : MonoBehaviour
{
    FMODUnity.EventReference LoudBang, PoweringDown, FinaleSong;
    public static Finale Instance;
    public GameObject[] Screens;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestroyScreens()
    {
        foreach (var item in Screens)
        {
            item.SetActive(false);
        }
    }
}
