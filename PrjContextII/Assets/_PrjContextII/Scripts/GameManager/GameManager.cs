using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentDayIndex = 0;
    public float GoodOrBadMeter = 0, GoodBadBorder = 0.2f, GoodBadIncrement = 0.2f;
    public List<ChoiceDay> Days; // Een lijst met alle dagen en hun keuzes

    public static BoxCollider computerCollider;
    private void Awake()
    {
        Debug.Log("made");
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void EnableComputer()
    {
        if (!computerCollider.enabled)
        {
            computerCollider.enabled = true;
            Debug.Log("computer collider is nu aan");
        }
    }
    public static void DisableComputer()
    {
        if (computerCollider.enabled)
        {
            computerCollider.enabled = false;
        }
    }
}
