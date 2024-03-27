using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using FMODUnityResonance;
using UnityEngine;

public class VoiceOvers : MonoBehaviour
{
    public static VoiceOvers Instance;
    private Queue<FMODUnity.EventReference> eventQueue = new Queue<FMODUnity.EventReference>();
    public FMODUnity.EventReference VoiceOverIntro;
    public FMODUnity.EventReference VoiceOverBad, VoiceOverGood, VoiceOverMail, VoiceOverTasks, VoiceOverDesktop, VoiceOverMap, EndingDay1;
    public bool IntroHasBeenPlayed = false, VoiceOverHasBeenPlayed = false, IsPlaying = false;
    private Queue<string> TempText = new();
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

    public void QueueVoiceOver(FMODUnity.EventReference eventReference, string EventName)
    {
        eventQueue.Enqueue(eventReference);
        TempText.Enqueue(EventName);

        if (!IsPlaying) //maar hij speelt niet de volgende als deze waar is.
        {
            PlayNextInQueue();
        }
    }

    private void PlayNextInQueue()
    {
        if (eventQueue.Count > 0) //dus alleen als hij op meer dan 0 staat speelt hij iets
        {
            IsPlaying = true;
            FMODUnity.EventReference nextEvent = eventQueue.Dequeue();

            FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(nextEvent);

            //set pos
            FMOD.ATTRIBUTES_3D attributes = RuntimeUtils.To3DAttributes(transform.position);
            eventInstance.set3DAttributes(attributes);

            eventInstance.start();
            eventInstance.release();

            string Text = TempText.Dequeue();
            SubtitleManager.Instance.PlaySubtitle(Text);

            StartCoroutine(WaitForEventToFinish(eventInstance));
        }
    }
    private IEnumerator WaitForEventToFinish(FMOD.Studio.EventInstance instance)
    {
        // SubtitleManager.Instance.PlaySubtitle(TempText);

        FMOD.Studio.PLAYBACK_STATE state;
        do
        {
            instance.getPlaybackState(out state);
            yield return null;
        } while (state != FMOD.Studio.PLAYBACK_STATE.STOPPED);

        SubtitleManager.Instance.StopSubtitles();
        IsPlaying = false;
        PlayNextInQueue();
    }
    public void PlayIntro()
    {
        if (!IntroHasBeenPlayed)
        {
            QueueVoiceOver(VoiceOverIntro, "Intro");
        }
        IntroHasBeenPlayed = true;
    }

    public void PlayGoodBad()
    {
        if (!VoiceOverHasBeenPlayed && !IsPlaying)
        {
            if (GameManager.instance.GoodOrBadMeter < 0)
            {
                QueueVoiceOver(VoiceOverBad, "Bad");
            }
            else
            {
                QueueVoiceOver(VoiceOverGood, "Good");
            }
            VoiceOverHasBeenPlayed = true;
        }
    }

    public void PlayDesktop()
    {
        QueueVoiceOver(VoiceOverDesktop, "Desktop");
    }

    public void PlayTasks()
    {
        QueueVoiceOver(VoiceOverTasks, "Tasks");
    }

    public void PlayMail()
    {
        QueueVoiceOver(VoiceOverMail, "Mail");
    }

    public void Playmap()
    {
        QueueVoiceOver(VoiceOverMap, "Map");
    }
    public void PlayEndingDay1()
    {
        QueueVoiceOver(EndingDay1, "EndingDay1");
    }
}
