using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }
    private static AudioManager _instance;

    public void PlayBGM()
    {
        _bgmSource.Play();
    }

    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    public void PlayCollideSound()
    {
        _soundSource.PlayOneShot(_soundSource.clip, 4);
    }

    [SerializeField]
    private AudioSource _bgmSource;
    [SerializeField]
    private AudioSource _soundSource;
}