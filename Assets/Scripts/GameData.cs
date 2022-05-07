using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData: MonoBehaviour
{
    [SerializeField] RotaryCannonsCollection _cannons;
    private int _activeCannon;

    public static GameData Instance { get; private set; }
    public int ActiveCannon { get => _activeCannon; }

    private void Awake()
    {
        if(FindObjectsOfType<GameData>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    public void SetActiveCannon(int index)
    {
        _activeCannon = index;
    }
}
