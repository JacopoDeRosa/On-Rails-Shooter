using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandCannon : MonoBehaviour
{
    [SerializeField] private float _roundsPerMinute;
    [SerializeField] private float _windUpTime;
    [SerializeField] private float _windDownTime;
    [SerializeField] private float _damage;
    [SerializeField] private Transform _barrels;
    [SerializeField] private ParticleSystem _bullet;
    [SerializeField] private ParticleSystem _muzzleBlast;
    [SerializeField] private GameObject _hitFX;

    public UnityEvent onWindUp;
    public UnityEvent onStartedFiring;
    public UnityEvent onStoppedFiring;
    public UnityEvent onWindDown;

    private float _currentSpeed = 0;
    private bool _firing = false;

    private bool _startedWindingUp = false;
    private bool _startedShooting = false;
    private bool _startedWindingDown = false;

    private float RotationSpeed { get => _roundsPerMinute; }
    private bool BarrelAligned
    {
        get
        {
            float rot = _barrels.rotation.eulerAngles.z % 60;

            return rot > 60 - _currentSpeed * Time.fixedDeltaTime && rot < 60;
        }
    }
    private float WindUpSpeed { get => RotationSpeed / _windUpTime; }
    private float WindDownSpeed { get => RotationSpeed / _windDownTime; }

    public void StartFiring()
    {
        _firing = true;
    }
    public void StopFiring()
    {
        _firing = false;
    }

    public void OnParticleCollided(ParticleCollisionEvent collision)
    {
        var fx = Instantiate(_hitFX, collision.intersection, Quaternion.identity);
        fx.transform.LookAt(collision.normal);
        print(collision);
        var collisionTransform = collision.colliderComponent.transform;
        if (collisionTransform == null) return;
        var root = collisionTransform.root;
        if (root == null) return;
        var health = root.GetComponent<Health>();
        if (health == null) return;       
        health.DealDamage(_damage);      
    }

    private void FixedUpdate()
    {
        if (_firing)
        {
            if (_currentSpeed < RotationSpeed)
            {
                if (_startedWindingUp == false)
                {
                    onWindUp.Invoke();
                    _startedWindingUp = true;
                    TryResetWindDown();
                }
                _currentSpeed += WindUpSpeed * Time.fixedDeltaTime;
            }
            else
            {
                _currentSpeed = RotationSpeed;
                TryResetWindingUp();
            }
            if (_currentSpeed == RotationSpeed && BarrelAligned)
            {
                if (_startedShooting == false)
                {
                    onStartedFiring.Invoke();
                    _startedShooting = true;
                }
                _bullet.Play();
                _muzzleBlast.Play();
            }
        }
        else
        {
            TryResetWindingUp();
            TryResetStartedShooting();
            onStoppedFiring.Invoke();

            if (_currentSpeed > 0)
            {
                if (_startedWindingDown == false)
                {
                    onWindDown.Invoke();
                    _startedWindingDown = true;
                }
                _currentSpeed -= WindDownSpeed * Time.fixedDeltaTime;
            }
            else
            {
                TryResetWindDown();
                _currentSpeed = 0;
            }
        }
        if (_currentSpeed > 0)
        {
            _barrels.Rotate(new Vector3(0, 0, _currentSpeed * Time.fixedDeltaTime), Space.Self);
        }
    }

    private void TryResetWindingUp()
    {
        if (_startedWindingUp) _startedWindingUp = false;
    }
    private void TryResetStartedShooting()
    {
        if (_startedShooting) _startedShooting = false;
    }
    private void TryResetWindDown()
    {
        if (_startedWindingDown) _startedWindingDown = false;
    }
}
