using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Sirenix.OdinInspector;

namespace ExtendedUI
{
    [Serializable]
    public class Menu
    {
        [TabGroup("Data")]
        [SerializeField] protected string _name = "Unnamed Menu";
        [TabGroup("Data")]
        [SerializeField] protected GameObject _target;

        [TabGroup("Events")]
        public UnityEvent onMenuOpen;

        [TabGroup("Events")]
        public UnityEvent onMenuClose;

        public bool Equals(string name)
        {
            if (name == _name) return true;
            return false;
        }

        public void Open()
        {
            OnOpen();
            if (_target != null) _target.SetActive(true);
            onMenuOpen.Invoke();
        }
        public void Close()
        {
            onMenuClose.Invoke();
            if (_target != null) _target.SetActive(false);
        }
        protected virtual void OnOpen()
        {

        }
        protected virtual void OnClose()
        {
            onMenuClose.Invoke();
        }
    }
}