using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _navmeshAgent;
    [SerializeField] private bool _startsAwake;
    [SerializeField] private bool _canGiveChase;
    [SerializeField] private Transform _target;

    private bool _giveChase = false;
    

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
       _navmeshAgent.isStopped = true;
    }
    public void Chase()
    {
        if (_canGiveChase == false) return;
        _giveChase = true;
    }

    private void Update()
    {
        if(_giveChase)
        {
            _navmeshAgent.SetDestination(_target.position);
        }
        if(_navmeshAgent != null)
        {
            _animator.SetFloat("Speed", _navmeshAgent.velocity.magnitude);
        }
    }
}
