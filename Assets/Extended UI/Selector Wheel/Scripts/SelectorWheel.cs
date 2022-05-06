using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace ExtendedUI
{
    public class SelectorWheel : MonoBehaviour
    {
        [SerializeField] private List<SelectorWheelSide> _wheelSides;
        [SerializeField] private float _size = 400;
        [SerializeField] private int _selectedSizeShift = 25;
        [SerializeField] private float _selectedSizeShiftSpeed = 200;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _clickedColor;




        public UnityEvent<SelectorWheelSide> onSideSelected;

        public Color NormalColor { get => _normalColor; }
        public Color ClickedColor { get => _clickedColor; }
        public Color SelectedColor { get => _selectedColor; }
        public int SelectedSizeShift { get => _selectedSizeShift; set => _selectedSizeShift = value; }
        public float Size { get => _size; }
        public float ShiftSpeed { get => _selectedSizeShiftSpeed; }
        public RectTransform RectTransform { get => (RectTransform)transform; }

        private void OnValidate()
        {
            var axis = RectTransform.Axis.Horizontal;
            if (RectTransform != null)
            {
                RectTransform.SetSizeWithCurrentAnchors(axis, _size);
            }

            for (int i = 0; i < _wheelSides.Count; i++)
            {
                SelectorWheelSide side = _wheelSides[i];
                if (side == null) continue;
                side.SetParent(this);
                side.SetIndex(i);
                if (side.RectTransform)
                {
                    var rect = side.RectTransform;
                    var width = RectTransform.rect.width / 2;
                    rect.SetSizeWithCurrentAnchors(axis, width); ;
                }
                side.SetColor(NormalColor);
            }
        }

        public void SetSize(float size)
        {
            _size = size;
            UpdateSize();
        }

        private void UpdateSize()
        {
            var axis = RectTransform.Axis.Horizontal;

            if (RectTransform != null)
            {
                RectTransform.SetSizeWithCurrentAnchors(axis, _size);
            }

            foreach (var side in _wheelSides)
            {
                if (side.RectTransform)
                {
                    var rect = side.RectTransform;
                    var width = RectTransform.rect.width / 2;
                    rect.SetSizeWithCurrentAnchors(axis, width); ;
                }
            }
        }
    }
}