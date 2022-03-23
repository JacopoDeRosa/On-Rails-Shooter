using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _startingHp;

    public UnityEvent<float> onHealthChanged;
    public UnityEvent<float> onDamageTaken;
    public UnityEvent onDeath;

    private float _currentHP;
    private bool _dead;

    private void Awake()
    {
        _currentHP = _startingHp;
    }

    public void DealDamage(float damage)
    {
        if (_dead) return;

        _currentHP -= damage;
        if(_currentHP <= 0)
        {
            _currentHP = 0;
            _dead = true;
            onDeath.Invoke();
        }

        onHealthChanged.Invoke(_currentHP);
        onDamageTaken.Invoke(damage);
    }
}
