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


    void playAudioClip(AvailableAudioClips AAC)
    {

        if (AAC.Equals(AvailableAudioClips.attack))
        {
            sfxSource.PlayOneShot(jumpSound);
        }


    }

    enum AvailableAudioClips
    {
        jump,
        chargeAttack,
        attack
    }

}
