using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace ExtendedUI
{

    public class HealthBar : UIElement
    {
        [Header("Components")]
        [SerializeField] private RectTransform _mainTransform;
        [SerializeField] private RectTransform _fillMaskTransform;
        [SerializeField] private RectTransform _fillerTransform;

        [Header("Size Settings")]
        [SerializeField] private int _length;


        [Header("Values")]
        [MinValue(1)]
        [SerializeField] private float _maxValue;
        [MinValue(0)]
        [SerializeField] private float _value;

        private void OnValidate()
        {
            _maxValue = Mathf.Clamp(_maxValue, 0, float.MaxValue);
            _value = Mathf.Clamp(_value, 0, _maxValue);
            _length = Mathf.Clamp(_length, 100, int.MaxValue);

            SetMainRects();
            SetFillAmount();
        }

        private void SetMainRects()
        {
            if (_mainTransform != null) _mainTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _length);
            if (_fillerTransform != null) _fillerTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _length);
        }
        private void SetFillAmount()
        {
            if (_fillMaskTransform != null) _fillMaskTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _length * (_value / _maxValue));
        }


        public void SetValue(float value)
        {
            _value = value;
            _value = Mathf.Clamp(_value, 0, _maxValue);
            SetFillAmount();
        }
        public void SetLenght(int length)
        {
            _length = length;
            _length = Mathf.Clamp(_length, 100, int.MaxValue);
            SetMainRects();
        }
        public void SetMaxValue(float maxValue)
        {
            _maxValue = maxValue;
            _maxValue = Mathf.Clamp(_maxValue, 0, float.MaxValue);
            _value = Mathf.Clamp(_value, 0, _maxValue);
        }
    }

}