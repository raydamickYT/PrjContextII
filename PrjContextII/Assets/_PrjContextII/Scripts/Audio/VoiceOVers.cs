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
        if (!IsPlaying)
        {
            PlayNextInQueue();
            SubtitleManager.Instance.PlaySubtitle(EventName);
        }
    }

    private void PlayNextInQueue()
    {
        if (eventQueue.Count > 0)
        {
            IsPlaying = true;
            FMODUnity.EventReference nextEvent = eventQueue.Dequeue();

            FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(nextEvent);

            //set pos
            FMOD.ATTRIBUTES_3D attributes = RuntimeUtils.To3DAttributes(transform.position);
            eventInstance.set3DAttributes(attributes);

            eventInstance.start();
            eventInstance.release();
            StartCoroutine(WaitForEventToFinish(eventInstance));
        }
    }
    private IEnumerator WaitForEventToFinish(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        do
        {
            instance.getPlaybackState(out state);
            Debug.Log(state);
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
        Debug.Log("queued sound");
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
