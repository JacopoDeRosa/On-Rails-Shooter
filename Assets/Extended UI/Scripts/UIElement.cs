using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtendedUI
{
    public class UIElement : MonoBehaviour
    {
        public RectTransform rectTransform { get => (RectTransform)transform; }
    }
}
