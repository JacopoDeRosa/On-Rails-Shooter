using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace ExtendedUI
{
    public class ConfirmationWindow : MonoBehaviour
    {
        [SerializeField] private Button _acceptButton;
        [SerializeField] private Button _declineButton;
        [SerializeField] private Text _message;

        public event Action onConfirm;
        public event Action onDecline;

        public void Accept()
        {
            onConfirm?.Invoke();
            Destroy(gameObject);
        }
        public void Decline()
        {
            onDecline?.Invoke();
            Destroy(gameObject);
        }

        protected virtual void Awake()
        {
            if (_acceptButton != null)
            {
                _acceptButton.onClick.AddListener(Accept);
            }
            if (_declineButton != null)
            {
                _declineButton.onClick.AddListener(Decline);
            }
        }

        public void SetMessage(string message)
        {
            _message.text = message;
        }

    }
}
