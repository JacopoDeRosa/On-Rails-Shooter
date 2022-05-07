using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExtendedUI;

public class CannonHeatDisplay : MonoBehaviour
{
    [SerializeField] private RotaryCannon _cannon;
    [SerializeField] MeshRenderer _barrelsRenderer;
    [SerializeField] private RadialSliderTMP _radialSlider;
    [ColorUsage(true, true)]
    [SerializeField] private Color _coolColor;
    [ColorUsage(true, true)]
    [SerializeField] private Color _hotColor;

    [SerializeField] private AnimationCurve _effectCurve;

    private Material _targetMat = null;

    void Start()
    {
        if(_barrelsRenderer != null) _targetMat = _barrelsRenderer.material;
        if (_radialSlider != null) _radialSlider.SetMaxValue(_cannon.MaxHeat);
        _cannon.onHeatChange.AddListener(UpdateHeat);
    }

  
    private void UpdateHeat(float heat)
    { 
        Color colorToSet = Color.Lerp(_coolColor, _hotColor, _effectCurve.Evaluate(heat / _cannon.MaxHeat));
        if(_radialSlider != null) _radialSlider.SetValue(heat);
        if(_targetMat != null)   _targetMat.SetColor("_EmissiveColor", colorToSet);
    }
}
