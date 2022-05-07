using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelsController : MonoBehaviour
{
    [SerializeField] private FoldingBar[] _menus;

    FoldingBar _activeMenu;

    private void Awake()
    {
        foreach (var item in _menus)
        {
            item.Toggle(false);
        }
    }

    public void SetMenuActive(int index)
    {
        if(index >= _menus.Length)
        {
            return;
        }
        StartCoroutine(SwitchToMenu(index));
    }   

    private IEnumerator SwitchToMenu(int index)
    {
        var targetMenu = _menus[index];

        if(_activeMenu != null)
        {
            if (_activeMenu == targetMenu)
            {
                yield break;
            }
            else
            {
                yield return _activeMenu.Toggle();
            }
        }

        _activeMenu = targetMenu;
        yield return targetMenu.Toggle();

    }

    public void CloseActiveMenu()
    {
        if (_activeMenu == null) return;

        StartCoroutine(_activeMenu.Toggle());
    }
}
