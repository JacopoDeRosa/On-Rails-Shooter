using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cannons Collection", menuName = "New Cannons Collection")]
public class RotaryCannonsCollection : ScriptableObject
{
    [SerializeField] private RotaryCannon[] _availableCannons;

    public RotaryCannon GetCannon(int index)
    {
        if(index >= _availableCannons.Length)
        {
            return null;
        }

        return _availableCannons[index];
    }


}
