using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static BoxCollider computerCollider;
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
