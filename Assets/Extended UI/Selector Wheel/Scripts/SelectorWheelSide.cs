using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

namespace ExtendedUI
{
    public class SelectorWheelSide : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private SelectorWheel _parent;
        [SerializeField] private int _index;
        [SerializeField] private Image _background;

        [SerializeField] private Image _image;
        [SerializeField] private Text _text;


        private float _size;

        private bool _selected;
        private bool _engaged;
        private float BaseSize { get => _parent.Size / 2; }

        public RectTransform RectTransform { get => (RectTransform)transform; }
        public Image Image { get => _image; }
        public Text Text { get => _text; }
        public int Index { get => _index; }

        public void SetImage(Sprite sprite)
        {
            if (_image == null) return;
            _image.sprite = sprite;
        }

        public void SetText(string text)
        {
            if (_text == null) return;
            _text.text = text;
        }

        public void SetParent(SelectorWheel parent)
        {
            _parent = parent;
        }
        public void SetIndex(int index)
        {
            _index = index;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_engaged == true) return;
            if (_selected == true) return;
            _selected = true;
            Select();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_selected == false) return;

            _selected = false;

            if (_engaged == true) return;

            DeSelect();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_selected && IsLeftClick(eventData))
            {
                _engaged = true;
                SetColor(_parent.ClickedColor);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_engaged && IsLeftClick(eventData))
            {
                if (_selected)
                {
                    SetColor(_parent.SelectedColor);
                }
                else
                {
                    DeSelect();
                }

                _engaged = false;
                _parent.onSideSelected.Invoke(this);
            }
        }

        private bool IsLeftClick(PointerEventData eventData)
        {
            return eventData.button == PointerEventData.InputButton.Left;
        }


        public void SetColor(Color color)
        {
            if (_background != null)
            {
                _background.color = color;
            }
        }

        private void Select()
        {
            StopAllCoroutines();
            SetColor(_parent.SelectedColor);
            StartCoroutine(ChangeSize(_parent.SelectedSizeShift));
        }

        private void DeSelect()
        {
            StopAllCoroutines();
            SetColor(_parent.NormalColor);
            StartCoroutine(ChangeSize(0));
        }

        private void UpdateSize()
        {
            RectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, BaseSize + _size);
        }

        private IEnumerator ChangeSize(float desiredSize)
        {
            if (_size < desiredSize)
            {
                while (_size < desiredSize)
                {
                    _size += 1 * _parent.ShiftSpeed * Time.fixedDeltaTime;
                    UpdateSize();
                    yield return new WaitForFixedUpdate();
                }
            }
            else
            {
                while (_size > desiredSize)
                {
                    _size -= 1 * _parent.ShiftSpeed * Time.fixedDeltaTime;
                    UpdateSize();
                    yield return new WaitForFixedUpdate();
                }
            }

            _size = desiredSize;
            UpdateSize();
        }

        private void OnValidate()
        {
            if (_background == null) _background = GetComponent<Image>();
        }

    }
}