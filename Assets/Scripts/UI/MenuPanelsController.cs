using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelsController : MonoBehaviour
{
    [SerializeField] private GameObject[] _menus;


    public void SetMenuActive(int index)
    {
        foreach (var menu in _menus)
        {
            menu.SetActive(false);
        }
        _menus[index].SetActive(true);
    }
}
