using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public void WakeUp()
    {
        _animator.SetTrigger("Wake");
    }

    public void Die()
    {
        _animator.SetTrigger("Die " + Random.Range(1, 3));
    }
}
