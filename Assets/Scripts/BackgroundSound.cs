using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    [SerializeField] AudioClip intro;
    [SerializeField] AudioClip game;
    public static BackgroundSound instance;
    private AudioSource audioSource;
    private bool restartGameSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void ReproduceIntro()
    {
        restartGameSound = true;
        audioSource.clip = intro;
        audioSource.volume = 1f;
        audioSource.Play();
    }

    public void ReproduceGame()
    {
        if(restartGameSound)
        {
            restartGameSound = false;
            audioSource.clip = game;
            audioSource.volume = 0.15f;
            audioSource.Play();
        }
    }
}
