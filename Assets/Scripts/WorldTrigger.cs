using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldTrigger : MonoBehaviour
{
    [SerializeField] private string _tag = "Player";
    [SerializeField] private bool _oneTimeUse;

    public UnityEvent onTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == _tag)
        {
            onTrigger.Invoke();
            if(_oneTimeUse)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
