using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ExtendedUI
{
    public class Crossair : UIElement
    {
        [System.Serializable]
        private class CrossairBit
        {     
            [SerializeField] private Vector3 _startPos;
            [SerializeField] private Vector3 _offset;
            [SerializeField] private Transform _target;

            public Transform transform { get => _target; }
            public Vector3 Offset { get => _offset; }
            public Vector3 StartPos { get => _startPos; }
        }

        [SerializeField] [Range(0, 1)] private float _size;
        [SerializeField] private Color _crossairColor = Color.white;
        [SerializeField] private List<CrossairBit> _crossairBits = new List<CrossairBit>();


        private void OnValidate()
        {
            ChangeSize();
            ChangeColors();
        }
     
        private void ChangeSize()
        {
            foreach (var bit in _crossairBits)
            {
                if (bit == null || bit.transform == null) continue;
                bit.transform.localPosition = Vector3.Lerp(bit.StartPos, bit.StartPos + bit.Offset, _size);

            }
        }
        private void ChangeColors()
        {
            foreach (Image image in GetComponentsInChildren<Image>())
            {
                image.color = _crossairColor;
            }
        }

        public void SetSize(float size)
        {
            size = Mathf.Clamp(size, 0, 1);
            _size = size;
            ChangeSize();
        }
        public void SetColor(Color color)
        {
            _crossairColor = color;
        }
    }
}



