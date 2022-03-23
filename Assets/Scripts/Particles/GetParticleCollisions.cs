using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetParticleCollisions : MonoBehaviour
{
    [SerializeField] private ParticleSystem _targetSystem;
    public UnityEvent<ParticleCollisionEvent> onParticleCollision;

    private List<ParticleCollisionEvent> _collisionEvents = new List<ParticleCollisionEvent>();

    private void OnValidate()
    {
        if(_targetSystem == null)
        {
            _targetSystem = GetComponent<ParticleSystem>();
            if(_targetSystem == null)
            {
                Debug.LogError("Get Particle Collisions requires a particle system on the same object to work");
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        
        _targetSystem.GetCollisionEvents(other, _collisionEvents);

        foreach (ParticleCollisionEvent collision in _collisionEvents)
        {
            //   print( "Particle from " + name + " intersected " + other.name + " at: " + collision.intersection);
            if (collision.Equals(null)) return;
            onParticleCollision.Invoke(collision);
        }
    }
}
