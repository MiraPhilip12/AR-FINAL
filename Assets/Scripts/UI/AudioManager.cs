using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource voiceoverSource;
    public AudioSource engineSource;

    [Header("Sound Clips")]
    public AudioClip buttonClickSFX;
    public AudioClip engineStartSFX;
    public AudioClip engineIdleLoop;
    public AudioClip engineStopSFX;
    public AudioClip[] voiceoverClips;

    private bool isEngineRunning = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (musicSource != null && musicSource.clip != null)
            musicSource.Play();
    }

    public void PlayButtonClick()
    {
        if (buttonClickSFX != null && sfxSource != null)
            sfxSource.PlayOneShot(buttonClickSFX);
    }

    public void PlayVoiceover(int carIndex)
    {
        if (voiceoverSource == null) return;

        if (carIndex >= 0 && carIndex < voiceoverClips.Length && voiceoverClips[carIndex] != null)
        {
            voiceoverSource.Stop();
            voiceoverSource.clip = voiceoverClips[carIndex];
            voiceoverSource.Play();
        }
    }

    public void StopVoiceover()
    {
        if (voiceoverSource != null)
            voiceoverSource.Stop();
    }

    public void ToggleEngine()
    {
        if (engineSource == null) return;

        isEngineRunning = !isEngineRunning;

        if (isEngineRunning)
        {
            if (engineStartSFX != null)
                engineSource.PlayOneShot(engineStartSFX);

            if (engineIdleLoop != null)
            {
                engineSource.clip = engineIdleLoop;
                engineSource.loop = true;
                float delay = engineStartSFX != null ? engineStartSFX.length : 0;
                engineSource.PlayDelayed(delay);
            }
        }
        else
        {
            engineSource.Stop();
            if (engineStopSFX != null)
                engineSource.PlayOneShot(engineStopSFX);
        }
    }

    public bool IsEngineRunning()
    {
        return isEngineRunning;
    }
}