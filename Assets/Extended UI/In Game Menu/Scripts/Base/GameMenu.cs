using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Sirenix.OdinInspector;

namespace ExtendedUI
{
    public class GameMenu : MonoBehaviour
    {
        [Searchable]
        [Space(10)]
        [PropertyOrder(1)]
        [SerializeField] private List<MenuWindow> _menuWindows;


        private static char[] _separators = new char[] { '/', '-', '.' };

        private MenuWindow GetMenuWindow(string name)
        {
            return _menuWindows.Find(window => window.Equals(name));
        }
        private void OpenWindow(string name)
        {
            var window = GetMenuWindow(name);
            OpenWindow(window);
        }
        private void OpenWindow(MenuWindow window)
        {
            if (window == null) return;
            window.Open();
        }

        [PropertyOrder(0)]
        [BoxGroup("Controls")]
        [HorizontalGroup("Controls/Buttons")]
        [Button(Style = ButtonStyle.Box)]
        private void ResetMenus()
        {
            foreach (MenuWindow menu in _menuWindows)
            {
                menu.Close();
            }
        }

        [PropertyOrder(0)]
        [BoxGroup("Controls")]
        [HorizontalGroup("Controls/Buttons")]
        [Button(Style = ButtonStyle.FoldoutButton)]
        [LabelWidth(50)]
        public void OpenMenu(string path)
        {
            ResetMenus();

            if (string.IsNullOrEmpty(path)) return;

            string[] buffer = path.Split(_separators);

            var window = GetMenuWindow(buffer[0]);

            if (window == null) return;

            OpenWindow(window);

            if (buffer.Length <= 1) return;

            window.OpenSubmenu(buffer[1]);
        }
    }
}