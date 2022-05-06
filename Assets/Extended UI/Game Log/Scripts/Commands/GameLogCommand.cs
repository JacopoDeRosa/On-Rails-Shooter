using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace ExtendedUI
{
    public class GameLogCommand : ScriptableObject
    {
        [SerializeField] private string _name;

        public string Name { get => _name; }

        public bool Equals(string name)
        {
            return name == _name;
        }

        public virtual void Invoke(string[] args)
        {
#if UNITY_EDITOR
            Debug.Log("Command " + _name + " was called.");
#endif
        }
    }
}
