using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HitboxEventHandler(HitboxEventData eventData);

public struct HitboxEventData
{
    private float _damage;
    private Bodyparts _bodyPart;

    public float Damage { get => _damage; }
    public Bodyparts Bodypart { get => _bodyPart; }

    public HitboxEventData(float damage, Bodyparts bodypart)
    {
        _damage = damage;
        _bodyPart = bodypart;
    }

}

public class HitboxArea : MonoBehaviour
{
    [SerializeField] private Bodyparts _bodyPart;
    public event HitboxEventHandler onHit;


    public void Hit(float damage)
    {
        onHit?.Invoke(new HitboxEventData(damage, _bodyPart));
    }
}
