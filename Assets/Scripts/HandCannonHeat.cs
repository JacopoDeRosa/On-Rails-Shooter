using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCannonHeat : MonoBehaviour
{
    [SerializeField] MeshRenderer _barrelsRenderer;
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
            _heat += 0.1f * Time.deltaTime;
            UpdateColor();
        }
        else if(_heat > 0)
        {         
            _heat -= 0.25f * Time.deltaTime;
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
