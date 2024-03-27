using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finale : MonoBehaviour
{
    public FMODUnity.EventReference LoudBang, PoweringDown, FinaleSong;
    FMOD.Studio.EventInstance FinalSongInstance;
    public static Finale Instance;
    public GameObject FinalBarrier;
    public GameObject[] Screens, AudioSources;
    // Start is called before the first frame update
    void Start()
    {
        FinalBarrier.SetActive(false);
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StartFinale()
    {
        Debug.Log("einde");
        FinalBarrier.gameObject.SetActive(true);

        FinalSongInstance = FMODUnity.RuntimeManager.CreateInstance(FinaleSong);
        FinalSongInstance.setParameterByName("StopLoop", 0);
        FinalSongInstance.start();
        StartCoroutine(WaitForEventToFinish(FinalSongInstance));
    }

    public void DestroyScreens()
    {
        FinalBarrier.gameObject.SetActive(false);

        FinalSongInstance.setParameterByName("StopLoop", 1);
        FMODUnity.RuntimeManager.PlayOneShot(LoudBang);
        FMODUnity.RuntimeManager.PlayOneShot(PoweringDown);
        foreach (var item in Screens)
        {
            item.SetActive(false);
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
        
        //doe hier iets als het spel is afgesloten.
    }
}
