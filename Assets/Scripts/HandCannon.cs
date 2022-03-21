using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandCannon : MonoBehaviour
{
    [SerializeField] private float _roundPerMinute;
    [SerializeField] private Transform _barrels;
    [SerializeField] private bool _firing = false;
    [SerializeField] private ParticleSystem _bullet;
    public UnityEvent onFire;


    private float _firingTimer = 0;
    private float FireInterval { get => 60 / _roundPerMinute; }

    public void StartFiring()
    {
        _firing = true;
    }

    public void StopFiring()
    {
        _firing = false;
    }

    private void Update()
    {
        if(_firing)
        {
            // Each revolution = 6 shots
            // For each shot it needs to rotate 60°
            // 60 RPM = 60 degrees per second;
            // 120 RPM = 120 degrees per second;
            _barrels.Rotate(new Vector3(0, 0, _roundPerMinute * Time.deltaTime), Space.Self);

            if (_firingTimer >= FireInterval)
            {
                _firingTimer = 0;
                _bullet.Play();
                onFire.Invoke();
            }
        }

        _firingTimer += Time.deltaTime;
    }
}
