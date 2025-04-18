using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------- Audio Source ---------")]
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioSource SFXSource;

    [Header("--------- Audio Clip ---------")]

    public AudioClip background;

    public AudioClip playerDeath;
    public AudioClip playerAttack;
    public AudioClip playerWalking;

    public AudioClip coin;
    public AudioClip click;
    public AudioClip doorOpen;
    public AudioClip doorClose;


    public AudioClip orcAttack;
    public AudioClip bomerAttack;
    public AudioClip bossAttack;
    public AudioClip bossDeath;




    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);    
    }
}
