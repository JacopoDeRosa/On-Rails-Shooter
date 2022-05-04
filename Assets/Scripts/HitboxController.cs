using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    [SerializeField] private List<HitboxArea> _areas;
    [SerializeField] private List<LimbDamageMultiplier> _limbDamageMultipliers;
    [SerializeField] private Health _health;

    private void Awake()
    {
        foreach (var area in _areas)
        {
            area.onHit += OnAreaHit;
        }
        var damageMultipliersSet = new HashSet<LimbDamageMultiplier>(_limbDamageMultipliers);
        _limbDamageMultipliers = new List<LimbDamageMultiplier>(damageMultipliersSet);
    }
    private void OnValidate()
    {
        if(_areas == null || _areas.Count == 0)
        {
            FindAreas();
        }
        if (_limbDamageMultipliers != null)
        {
            var damageMultipliersSet = new HashSet<LimbDamageMultiplier>(_limbDamageMultipliers);
            if(damageMultipliersSet.Count != _limbDamageMultipliers.Count)
            {
                Debug.LogWarning("Warning: " + gameObject.name + "'s Hitbox component contains duplicate damage multipliers," +
                    " these will be scrubbed in awake");
            }
        }

    }

    private void FindAreas()
    {
        var areas = GetComponentsInChildren<HitboxArea>();
        _areas = new List<HitboxArea>(areas);
    }
    private void OnAreaHit(HitboxEventData data)
    {
       

        var damageMulti = _limbDamageMultipliers.Find(x => x.Equals(data.Bodypart));

        float multiplier = 1;

        if(damageMulti != null)
        {
            multiplier = damageMulti.Multiplier;
        }

        float damage = data.Damage * multiplier;
        _health.DealDamage(damage);
        print(damage + " Damage to " + data.Bodypart);
    }
}
