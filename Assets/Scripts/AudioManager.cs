using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private float _timeRequiredToWin = 3.0f;

    private AudioSource[] _audioSources;

    private MeshRenderer[] _meshRenderers = new MeshRenderer[] { };

    private bool _isVictory;
    private float _sourcesVolumeSum = 0;
    private float _victoryTimer;

    // Start is called before the first frame update
    void Start()
    {
        _audioSources = GetComponentsInChildren<AudioSource>();

        foreach (AudioSource source in _audioSources)
        {
            source.Play();
        }

        for(int i = 10; i >= 0; i-=2)
        {
            Debug.Log(i);
        }
    }

    private void Update()
    {
        // FOREACH method
        /*if (!_isVictory)
        {
            foreach(AudioSource source in _audioSources)
            {
                if(source.volume < 1)
                {
                    return;
                }
            }
            
            _isVictory = true;
        }


        Debug.Log("Win");*/

        // reset sum of all audiosources volume
        if (_isVictory) return;

        _sourcesVolumeSum = 0;

        // browse audio sources and add their volume to sum
        foreach (AudioSource source in _audioSources)
        {
            _sourcesVolumeSum += source.volume;
        }

        // If sum is equal to amount of audio sources, start incrementing timer
        if (_sourcesVolumeSum == _audioSources.Length && _victoryTimer < _timeRequiredToWin)
        {
            _victoryTimer += Time.deltaTime;
        }
        // else reset timer to zero
        else
        {
            _victoryTimer = 0;
        }

        // if timer is superior or equal to victory time, win
        if (_victoryTimer >= _timeRequiredToWin)
        {

            _isVictory = true;
            Debug.Log("Win");
            // Load Scene
        }
    }
}
