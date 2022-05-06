using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Sirenix.OdinInspector;

namespace ExtendedUI
{
    [Serializable]
    public sealed class MenuWindow : Menu
    {
        [TabGroup("Data")]
        [Searchable]
        [SerializeField] private List<SubMenu> _subMenus;
        private SubMenu GetSubMenu(string name)
        {
            return _subMenus.Find(menu => menu.Equals(name));
        }

        public void OpenSubmenu(string name)
        {
            ResetSubMenus();
            SubMenu menu = GetSubMenu(name);
            if (menu == null) return;
            menu.Open();
        }
        private void ResetSubMenus()
        {
            foreach (SubMenu menu in _subMenus)
            {
                menu.Close();
            }
        }

        protected override void OnOpen()
        {
            ResetSubMenus();
        }
        protected override void OnClose()
        {
            ResetSubMenus();
        }
    }
}