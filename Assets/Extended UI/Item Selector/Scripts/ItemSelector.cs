using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ExtendedUI
{
    public class ItemSelector : UIElement
    {
        private enum Direction
        {
            Left,
            Right
        }

        [Header("Components")]
        [SerializeField] private Image _image;
        [SerializeField] private Text _text;

        [Header("Buttons:")]
        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _leftButton;

        [Header("Options:")]
        [SerializeField] private List<SelectorOption> _options;

        public UnityEvent<SelectorOption> onOptionChanged;

        private int _position;

        public SelectorOption ActiveOption { get => _options[_position]; }

        private void Awake()
        {
            RedrawUI();
            if (_rightButton != null) _rightButton.onClick.AddListener(ScrollToRight);
            if (_leftButton != null) _leftButton.onClick.AddListener(ScrollToLeft);
        }

        public void ScrollToRight()
        {
            Scroll(Direction.Right);
        }
        public void ScrollToLeft()
        {
            Scroll(Direction.Left);
        }

        private void Scroll(Direction direction)
        {
            int delta = 0;
            if (direction == Direction.Left) delta = -1;
            if (direction == Direction.Right) delta = 1;

            _position += delta;

            if (_position >= _options.Count)
            {
                _position = 0;
            }
            else if (_position < 0)
            {
                _position = _options.Count - 1;
            }

            RedrawUI();

            onOptionChanged.Invoke(_options[_position]);
        }
        private void RedrawUI()
        {
            if (_image != null) _image.sprite = ActiveOption.Sprite;
            if (_text != null) _text.text = ActiveOption.Text;
        }
    }
}