using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CannonCard : MonoBehaviour
{
    [SerializeField] private RotaryCannonsCollection _allCannons;
    [SerializeField] private int _targetCannon;
    [SerializeField] private Image _cannonImage;
    [SerializeField] private TMP_Text _nameText, _rpmText, _heatText;

    private RotaryCannon _cannon;

    private void Start()
    {
        _cannon = _allCannons.GetCannon(_targetCannon);
        print(_cannon.RPM);
        _nameText.text = _cannon.name;
        _rpmText.text = _cannon.RPM.ToString();
        _heatText.text = _cannon.MaxHeat.ToString();
    }

    public void SetCannonActive()
    {
        GameData.Instance.SetActiveCannon(_targetCannon);
    }

}
