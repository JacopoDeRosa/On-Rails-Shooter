using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelsController : MonoBehaviour
{
    [SerializeField] private FoldingBar[] _menus;

    private FoldingBar _activeMenu;
    private bool _busy;

    public void SetMenuActive(int index)
    {
        if (_busy) return;

        if(index >= _menus.Length)
        {
            return;
        }
        StartCoroutine(SwitchToMenu(index));
    }   

    private IEnumerator SwitchToMenu(int index)
    {
        _busy = true;

        var targetMenu = _menus[index];

        if(_activeMenu != null)
        {
            if (_activeMenu == targetMenu)
            {
                _busy = false;
                yield break;
            }
            else
            {
                yield return _activeMenu.Toggle();
            }
        }

        _activeMenu = targetMenu;
        yield return targetMenu.Toggle();

        _busy = false;
    }
}
