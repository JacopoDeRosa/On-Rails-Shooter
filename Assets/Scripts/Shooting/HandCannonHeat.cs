using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCannonHeat : MonoBehaviour
{
    [SerializeField] MeshRenderer _barrelsRenderer;
    [SerializeField] private float _heatUpTime, _coolDownTime;
    [ColorUsage(true, true)]
    [SerializeField] private Color _coolColor;
    [ColorUsage(true, true)]
    [SerializeField] private Color _hotColor;
    [SerializeField] private AnimationCurve _effectCurve;

    private bool _firing = true;
    private Material _targetMat = null;

    private float _heat = 0;

    void Start()
    {
        _targetMat = _barrelsRenderer.material;
    }

    private void Update()
    {
       
        if(_firing && _heat < 1)
        {
            _heat += (1 / _heatUpTime) * Time.deltaTime;
            UpdateColor();
        }
        else if(_heat > 0)
        {         
            _heat -= (1 / _coolDownTime) * Time.deltaTime;
            UpdateColor();
        }

       
    }

    private void UpdateColor()
    {
        Color colorToSet = Color.Lerp(_coolColor, _hotColor, _effectCurve.Evaluate(_heat));
        _targetMat.SetColor("_EmissiveColor", colorToSet);
    }

    public void SetFiring(bool status)
    {
        _firing = status;
    }
}
