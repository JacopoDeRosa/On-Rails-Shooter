using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCannonEquipper : MonoBehaviour
{
    [SerializeField] private RotaryCannonsCollection _cannons;
    [SerializeField] private ShootingSystem _shooting;
    private void Awake()
    {
        print("Enable Cannon");
        RotaryCannon cannon = _cannons.GetCannon(GameData.Instance.ActiveCannon);
        RotaryCannon instance = Instantiate(cannon, transform, false);
        _shooting.SetCannon(instance);
    }
}
