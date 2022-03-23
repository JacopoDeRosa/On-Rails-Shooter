using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundFromTime : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _startingTime;


    void Start()
    {
        _audioSource.time = _startingTime;
    }
}
