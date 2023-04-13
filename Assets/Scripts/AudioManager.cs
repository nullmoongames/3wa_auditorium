using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource[] _audioSources;

    // Start is called before the first frame update
    void Start()
    {
        _audioSources = GetComponentsInChildren<AudioSource>();

        foreach(AudioSource source in _audioSources)
        {
            source.Play();
        }
    }
}
