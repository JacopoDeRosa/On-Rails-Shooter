using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace ExtendedUI
{
    public class MiniRadarItem : UIElement
    {
        [SerializeField] private Sprite _markerSprite;

        public Sprite MarkerSprite { get => _markerSprite; }

        public event Action onDisable;
        public event Action onMove;

        private void Start()
        {
            var miniRadar = FindObjectOfType<MiniRadar>();
            miniRadar.Register(this);
        }

        private void OnDisable()
        {
            onDisable?.Invoke();
        }

        private void Update()
        {
            if(transform.hasChanged)
            {
                onMove?.Invoke();
            }
        }
    }
}
