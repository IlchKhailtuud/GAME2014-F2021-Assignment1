using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    public static AudioEffect instance;

    [SerializeField]
    private AudioClip explosion, placeBomb;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayExplosion()
    {
        audioSource.clip = explosion;
        audioSource.Play();
    }

    public void PlayPlaceBomb()
    {
        audioSource.clip = placeBomb;
        audioSource.Play();
    }
}
