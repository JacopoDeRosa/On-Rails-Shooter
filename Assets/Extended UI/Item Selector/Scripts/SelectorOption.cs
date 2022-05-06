using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtendedUI
{
    [System.Serializable]
    public class SelectorOption
    {
        [SerializeField] private string _text;
        [SerializeField] private Sprite _sprite;

        public string Text { get => _text; }
        public Sprite Sprite { get => _sprite; }
    }

    public class SelectorOption<T>
    {
        [SerializeField] private string _text;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private T _data;

        public string Text { get => _text; }
        public Sprite Sprite { get => _sprite; }
        public T Data { get => _data; }
    }
}