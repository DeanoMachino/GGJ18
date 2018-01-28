using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;
    public AudioSource musicSource;

    public AudioClip jumpSound;
    public AudioClip chargingAttackSound;
    public AudioClip attackSound;

    public AudioClip gameMusic;
    public AudioClip mainMenuMusic;

    //To create singleton class
    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playBackgroundMusic(AvailableMusicClips AMC) {
        switch (AMC)
        {
            case AvailableMusicClips.ingameMusic:
                musicSource.clip = gameMusic;
                musicSource.loop = true;
                musicSource.Play();
                break;
            case AvailableMusicClips.mainMenuMusic:
                musicSource.clip = mainMenuMusic;
                musicSource.loop = true;
                musicSource.Play();
                break;
            default:
                break;
        }
    }

    public void playAudioClip(AvailableAudioClips AAC)
    {
        switch (AAC) {
            case AvailableAudioClips.releaseAttack:
                sfxSource.PlayOneShot(attackSound,0.1f);
                break;
            case AvailableAudioClips.jump:
                sfxSource.PlayOneShot(jumpSound, 0.5f);
                break;
            case AvailableAudioClips.chargeAttack:
                sfxSource.PlayOneShot(chargingAttackSound, 0.1f);
                break;
            default:
                break;
        }
    }

    public enum AvailableAudioClips
    {
        jump,
        chargeAttack,
        releaseAttack
    }

    public enum AvailableMusicClips
    {
        ingameMusic,
        mainMenuMusic
    }
}
