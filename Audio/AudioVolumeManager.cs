using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeManager : MonoBehaviour
{
    [System.Serializable]
    public class AudioControl
    {
        public string label;
        // public Image iconImg;
        public Sprite iconOn;
        public Sprite iconOff;
        public AudioSource audioSource;
        public Animator iconAnimator;

        [Range(0f, 1f)]
        public float volume = 1f;
        public float step = 1f;

        public void IncreaseSFX()
        {
            // Debug.Log("Increasing SFX volume");
            AudioManager.Instance.ChangeSFXVolume(+0.1f);
            // Debug.Log("Current SFX volume: " + AudioManager.Instance.sfxAudioSource.volume);
        }
        public void DecreaseSFX()
        {
            // Debug.Log("Decreasing SFX volume");
            AudioManager.Instance.ChangeSFXVolume(-0.1f);
            // Debug.Log("Current SFX volume: " + AudioManager.Instance.sfxAudioSource.volume);
        }

        public void IncreaseMusic()
        {
            // Debug.Log("Increasing Music volume");
            AudioManager.Instance.ChangeMusicVolume(+0.1f);
            // Debug.Log("Current Music volume: " + AudioManager.Instance.musicAudioSource.volume);
        }
        public void DecreaseMusic()
        {
            // Debug.Log("Decreasing Music volume");
            AudioManager.Instance.ChangeMusicVolume(-0.1f);
            // Debug.Log("Current Music volume: " + AudioManager.Instance.musicAudioSource.volume);
        }

        public void Apply()
        {
            float saveVolume = PlayerPrefs.GetFloat(label, 1);
            volume = saveVolume;
            // Debug.Log($"[{label}] Apply() called. Volume = {volume}");

            if (audioSource != null)
            {
                // Debug.Log(label + " Apply volume: " + volume);
                audioSource.volume = volume;
                // Debug.Log($"[{label}] AudioSource volume set to: {audioSource.volume}");
            }
            if (iconAnimator != null)
            {
                bool isMuted = volume <= 0.001f;
                iconAnimator.SetBool("IsMuted", isMuted);
                // Debug.Log($"[{label}] Animator IsMuted set to: {isMuted}");

            }
        }
    }

    [Header("Music Controll")]
    public AudioControl music;

    [Header("SFV Controll")]
    public AudioControl sfx;

    private void Start()
    {
        music.Apply();
        sfx.Apply();
    }

    public void IncreaseMusic() => music.IncreaseMusic();
    public void DecreaseMusic() => music.DecreaseMusic();
    public void IncreaseSFX() => sfx.IncreaseSFX();
    public void DecreaseSFX() => sfx.DecreaseSFX();
}
