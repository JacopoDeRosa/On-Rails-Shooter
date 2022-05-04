using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField] private List<EnemyController> _enemies;
    public UnityEvent onAllDead;

    private int _deadEnemies;

    private void Start()
    {
        foreach(var enemy in _enemies)
        {
          var health = enemy.GetComponent<Health>();
            if(health)
            {
                health.onDeath.AddListener(OnEnemyDeath);
            }
        }
    }

    public void WakeUpAll()
    {
        foreach (var enemy in _enemies)
        {
            enemy.WakeUp();
        }
    }

    private void OnEnemyDeath()
    {
        _deadEnemies++;
        if (_deadEnemies == _enemies.Count)
        {
            onAllDead.Invoke();
        }
    }
}
