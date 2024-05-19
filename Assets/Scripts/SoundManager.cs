using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioClip GeneralSound;
    public AudioClip GunShotSound;
    public AudioClip AimSound;
    public AudioClip BackgroundSound;
    public AudioClip MissionTapeSound;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        PlaySound(BackgroundSound, transform.position, 0.2f);
        PlaySound(GeneralSound, transform.position, 1f);    
    }

    public void PlaySound(AudioClip clip, Vector3 position)
    {
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.position = position;

        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(soundGameObject, clip.length);
    }

    public void PlaySound(AudioClip clip)
    {
        GameObject soundGameObject = new GameObject("Sound");

        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(soundGameObject, clip.length);
    }

    public void PlaySound(AudioClip clip, float volume, float delay)
    {
        GameObject soundGameObject = new GameObject("Sound");

        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.PlayDelayed(delay);

        Destroy(soundGameObject, clip.length);
    }

    public void PlaySound(AudioClip clip, Vector3 position, float volume)
    {
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.position = position;

        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        Destroy(soundGameObject, clip.length);
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        GameObject soundGameObject = new GameObject("Sound");

        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        Destroy(soundGameObject, clip.length);
    }
}
