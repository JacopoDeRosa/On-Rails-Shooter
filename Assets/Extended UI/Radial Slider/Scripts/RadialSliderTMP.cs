using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ExtendedUI
{
    public class RadialSliderTMP : MonoBehaviour
    {
        [Header("Components:")]
        [SerializeField] private Image _fill;
        [SerializeField] private Image _mask;

        [Header("Sprites:")]
        [SerializeField] private Sprite _maskSprite;
        [SerializeField] private Sprite _fillSprite;

        [Header("Colors:")]
        [SerializeField] private Color _fillColor = Color.gray;
        [SerializeField] private Color _baseColor = Color.white;
        [SerializeField] private Color _textColor = Color.white;

        [Header("Dyamic Text Color:")]
        [SerializeField] private bool _dynamicTextColor;
        [SerializeField] private Color _fullTextColor;
        [SerializeField] private Color _emptyTextColor;

        [Header("Text:")]
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private int _textSize = 30;

        [Header("Values:")]
        [SerializeField] private float _maxValue;
        [SerializeField] private float _value;

        public float Value { get => _value; }

        private void OnValidate()
        {
            _value = Mathf.Clamp(_value, 0, _maxValue);
            _maxValue = Mathf.Clamp(_maxValue, 0, float.MaxValue);

            if (_mask != null)
            {
                if (_maskSprite != null) _mask.sprite = _maskSprite;
                _mask.color = _baseColor;
            }

            if (_fill != null)
            {
                _fill.fillAmount = _value / _maxValue;
                _fill.color = _fillColor;
                if (_fillSprite != null) _fill.sprite = _fillSprite;
            }

            if (_text != null)
            {
                _text.fontSize = _textSize;
                if (_dynamicTextColor)
                {
                    _text.color = Color.Lerp(_emptyTextColor, _fullTextColor, _value / _maxValue);
                }
                else
                {
                    _text.color = _textColor;
                }
                _text.text = Mathf.Ceil(_value).ToString();
            }
        }

        public void SetValue(float value)
        {
            _value = Mathf.Clamp(value, 0, _maxValue);
            if (_fill != null)
            {
                _fill.fillAmount = _value / _maxValue;
            }
            if (_text != null)
            {
                _text.text = Mathf.Ceil(_value).ToString();
                if (_dynamicTextColor)
                {
                    _text.color = Color.Lerp(_emptyTextColor, _fullTextColor, _value / _maxValue);
                }
            }
        }
        public void SetMaxValue(float value)
        {
            _maxValue = value;
            if (_value > _maxValue) _value = value;
        }
    }
}