using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finale : MonoBehaviour
{
    public FMODUnity.EventReference LoudBang, PoweringDown, FinaleSong;
    FMOD.Studio.EventInstance FinalSongInstance;
    public static Finale Instance;
    public GameObject FinalBarrier;
    public GameObject[] Screens, AudioSources;

    public Light DirLight1, DirLight2;
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
        //verander de belichting
        DirLight1.gameObject.SetActive(false);
        DirLight2.gameObject.SetActive(true);

        foreach (var item in AudioSources)
        {
            item.SetActive(false);
        }
        Debug.Log("einde");
        FinalBarrier.gameObject.SetActive(true);

        FinalSongInstance = FMODUnity.RuntimeManager.CreateInstance(FinaleSong);
        FinalSongInstance.setParameterByName("StopLoop", 0);
        FinalSongInstance.start();
        StartCoroutine(WaitForEventToFinish(FinalSongInstance));
    }

    public void DestroyScreens()
    {
        foreach (var item in AudioSources)
        {
            if (item.name == "2D Ambience")
            {
                item.SetActive(true);
            }
        }

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

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("StartScreen");

    }
}
