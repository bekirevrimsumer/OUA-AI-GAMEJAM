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
    public AudioClip RewindSound;
    public AudioClip MissionSuccessSound;
    public AudioClip MissionFailSound;

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
        PlaySound(BackgroundSound, 0.2f, 5f);
        PlaySound(GeneralSound, 1f, 5f);    
        PlaySound(MissionTapeSound, 1f, 8f);
    }

    public void PlaySound(AudioClip clip, string name, float volume)
    {
        GameObject soundGameObject = new GameObject(name);

        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
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

        Destroy(soundGameObject, clip.length + delay);
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
}
