using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _startsAwake;

    private void Start()
    {
        if(_startsAwake)
        {
            WakeUp();
        }
    }
    public void WakeUp()
    {
        _animator.SetTrigger("Wake");
    }

    public void Die()
    {
        _animator.SetTrigger("Die " + Random.Range(1, 3));
    }
}
