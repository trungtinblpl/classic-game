using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;

    [Header("General SFX")]
    public AudioClip itemsClip;
    public AudioClip kenClip;
    public AudioClip moonClip;
    public AudioClip hurtClip;
    public AudioClip dashClip;

    [Header("Enemies")]
    public AudioClip enemyAttackClip;
    public AudioClip enemyDeathClip;
    public AudioClip enemyRandClip;
    public AudioClip eagleAttackClip;

    [Header("Win and Lose")]
    public AudioClip winClip;
    public AudioClip loseClip;

    [Header("Button")]
    public AudioClip buttonClickClip;

    [Header("Menu Music")]
    public AudioClip menuMusic;

    [Header("Level Music Clips")]
    public AudioClip[] levelMusicClips;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            musicAudioSource = transform.Find("Music").GetComponent<AudioSource>();
            sfxAudioSource = transform.Find("SFX").GetComponent<AudioSource>();

            ApplySaveVolume();
        }
        else if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayerLoseMusic()
    {
        musicAudioSource.Stop();

        if (loseClip != null)
        {
            musicAudioSource.clip = loseClip;
            musicAudioSource.Play();
        }
    }

    public void PlaySFX(AudioClip audioClip)
    {
        if (audioClip != null)
        {
            sfxAudioSource.PlayOneShot(audioClip);
        }
    }

    public void PlayMusic(AudioClip audioClip)
    {
        if (audioClip == null)
        {
            return;
        }

        if (musicAudioSource.clip == audioClip && musicAudioSource.isPlaying)
        {
            return;
        }

        musicAudioSource.Stop();
        musicAudioSource.clip = audioClip;
        musicAudioSource.Play();
    }

    public void ChangeSFXVolume(float delta)
    {
        ChangeSourceVolume("SoundVolume", sfxAudioSource, delta);
    }

    public void ChangeMusicVolume(float delta)
    {
        ChangeSourceVolume("MusicVolume", musicAudioSource, delta);
    }

    private void ChangeSourceVolume(string key, AudioSource source, float delta)
    {
        float volume = PlayerPrefs.GetFloat(key, 1f);
        volume = Mathf.Clamp01(volume + delta);
        source.volume = volume;
        PlayerPrefs.SetFloat(key, volume);
    }

    private void ApplySaveVolume()
    {
        musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f); ;
        sfxAudioSource.volume = PlayerPrefs.GetFloat("SoundVolume", 1f); ;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Debug.Log("Scene Loaded: " + scene.name);

        if (scene.name == "Menu")
        {
            PlayMusic(menuMusic);
        }
        else
        {
            int sceneIndex = scene.buildIndex;

            if (sceneIndex - 1 >= 0 && sceneIndex - 1 < levelMusicClips.Length)
            {
                PlayMusic(levelMusicClips[sceneIndex - 1]);
            }
        }
    }
}


