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
                musicSource.PlayOneShot(gameMusic);
                break;
            case AvailableMusicClips.mainMenuMusic:
                musicSource.PlayOneShot(mainMenuMusic);
                break;
            default:
                break;
        }

    }

    public void playAudioClip(AvailableAudioClips AAC)
    {
        switch (AAC) {
            case AvailableAudioClips.attack:
                sfxSource.PlayOneShot(jumpSound);
                break;
            case AvailableAudioClips.jump:
                sfxSource.PlayOneShot(chargingAttackSound);
                break;
            case AvailableAudioClips.chargeAttack:
                sfxSource.PlayOneShot(attackSound);
                break;
            default:
                break;
        }
    }

    public enum AvailableAudioClips
    {
        jump,
        chargeAttack,
        attack
    }

    public enum AvailableMusicClips
    {
        ingameMusic,
        mainMenuMusic
    }
}
