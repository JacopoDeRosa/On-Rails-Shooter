using System;
using UnityEngine;

[System.Serializable]
public class LimbDamageMultiplier
{
    [SerializeField] private Bodyparts _targetPart;
    [SerializeField] private float _multiplier;

    public float Multiplier { get => _multiplier; }
    public Bodyparts TargetPart { get => _targetPart; }

    public override bool Equals(object obj)
    {
        LimbDamageMultiplier other = obj as LimbDamageMultiplier;
        if (other == null) return false;

        return other.TargetPart == _targetPart;
    }

    public bool Equals(Bodyparts bodypart)
    {
        return bodypart == _targetPart;
    }

    public override int GetHashCode()
    {
        return (int)_targetPart;
    }

}