using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using FMODUnityResonance;
using UnityEngine;

public class VoiceOvers : MonoBehaviour
{
    public static VoiceOvers Instance;
    public FMODUnity.EventReference VoiceOverIntro;
    public FMODUnity.EventReference VoiceOverBad, VoiceOverGood;
    public bool IntroHasBeenPlayed = false, VoiceOverHasBeenPlayed = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayIntro();
    }

    public void PlayIntro()
    {
        if (!IntroHasBeenPlayed)
        {
            RuntimeManager.PlayOneShot(VoiceOverIntro, transform.position);
        }
        IntroHasBeenPlayed = true;
    }

    public void PlayGoodBad()
    {
        if (GameManager.instance.GoodOrBadMeter < 0)
        {
            RuntimeManager.PlayOneShot(VoiceOverGood, transform.position);
        }
        else
        {
            RuntimeManager.PlayOneShot(VoiceOverGood, transform.position);
        }
        VoiceOverHasBeenPlayed = true;
    }
}
