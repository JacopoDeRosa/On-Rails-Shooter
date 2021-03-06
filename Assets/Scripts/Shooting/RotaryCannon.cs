using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class RotaryCannon : MonoBehaviour
{
    [SerializeField] private float _roundPerMinute;
    [SerializeField] private float _windUpTime;
    [SerializeField] private float _windDownTime;
    [SerializeField] private float _damage;
    [SerializeField] private int _numberOfBarrels;
    [SerializeField] private float _heatPerShot;
    [SerializeField] private float _termalShutdownPoint;
    [SerializeField] private float _coolingPerSecond;
    [SerializeField] private Transform _barrels;
    [SerializeField] private ParticleSystem _bullet;
    [SerializeField] private ParticleSystem _muzzleBlast;
    [SerializeField] private GameObject _hitFX;

    public UnityEvent onWindUp;
    public UnityEvent onStartedFiring;
    public UnityEvent onStoppedFiring;
    public UnityEvent onWindDown;
    public UnityEvent onOutOfAmmoClick;
    public UnityEvent<float> onHeatChange;

    private float _currentSpeed = 0;
    private bool _firing = false;
    [ShowInInspector]
    private float _currentHeat;

    private bool _startedWindingUp = false;
    private bool _startedShooting = false;
    private bool _startedWindingDown = false;

    private float RotationSpeed { get => (360 / _numberOfBarrels) * (_roundPerMinute / 60); }
    private bool BarrelAligned
    {
        get
        {
            float rot = _barrels.rotation.eulerAngles.z % (360 / _numberOfBarrels);

            return rot > (360 / _numberOfBarrels) - _currentSpeed * Time.fixedDeltaTime && rot < (360 / _numberOfBarrels);
        }
    }
    private float WindUpSpeed { get => RotationSpeed / _windUpTime; }
    private float WindDownSpeed { get => RotationSpeed / _windDownTime; }

    public float MaxHeat { get => _termalShutdownPoint; }
    public float RPM { get => _roundPerMinute; }
    public Transform Barrels { get => _barrels; }


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

        if (collision.colliderComponent != null)
        {
            var hitbox = collision.colliderComponent.GetComponent<HitboxArea>();
            if (hitbox != null)
            {
                hitbox.Hit(_damage);
            }
        }
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
                AddHeat();
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

            if (_currentHeat > 0)
            {
                _currentHeat -= _coolingPerSecond * Time.deltaTime;
                onHeatChange.Invoke(_currentHeat);
            }

        }
        if (_currentSpeed > 0)
        {
            _barrels.Rotate(new Vector3(0, 0, _currentSpeed * Time.fixedDeltaTime), Space.Self);
        }     
    }
    private void AddHeat()
    {
        _currentHeat += _heatPerShot;
        onHeatChange.Invoke(_currentHeat);
        if(_currentHeat > _termalShutdownPoint)
        {
            StopFiring();
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
    public void OutOfAmmoClick()
    {
        onOutOfAmmoClick.Invoke();
    }
}
