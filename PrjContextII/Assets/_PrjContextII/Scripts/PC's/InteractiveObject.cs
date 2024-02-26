using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractiveObject : MonoBehaviour
{
    public void Activate()
    {
        // Voer hier de gewenste actie uit, bijvoorbeeld het tonen van een bericht
        Debug.Log("Object geactiveerd: " + gameObject.name);
    }
}
