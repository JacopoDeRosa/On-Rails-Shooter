using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtendedUI
{
    public class SelectorWheelTrigger : MonoBehaviour
    {
        [SerializeField] private SelectorWheel _wheel;
        [SerializeField] private KeyCode _key;
        [SerializeField] private float _openingSpeed;
        [SerializeField] private float _finalSize;
        [SerializeField] [Range(0, 1)] private float _size;

        private void Start()
        {
            UpdateSize();
        }

        private void UpdateSize()
        {
            _wheel.SetSize(Mathf.Lerp(0, _finalSize, _size));
        }

        private void Update()
        {
            if (Input.GetKeyDown(_key))
            {
                StopAllCoroutines();
                StartCoroutine(OpenWheel());
            }
            if (Input.GetKeyUp(_key))
            {
                StopAllCoroutines();
                StartCoroutine(CloseWheel());
            }
        }

        private IEnumerator OpenWheel()
        {
            _wheel.gameObject.SetActive(true);
            while (_size < 1)
            {
                _size += Time.fixedDeltaTime * _openingSpeed;
                UpdateSize();
                yield return new WaitForFixedUpdate();
            }
            _size = 1;
            UpdateSize();
        }

        private IEnumerator CloseWheel()
        {
            while (_size > 0)
            {
                _size -= Time.fixedDeltaTime * _openingSpeed;
                UpdateSize();
                yield return new WaitForFixedUpdate();
            }
            _size = 0;
            _wheel.gameObject.SetActive(false);
            UpdateSize();
        }



    }
}