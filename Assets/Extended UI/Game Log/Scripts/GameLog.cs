using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtendedUI
{
    public class GameLog : MonoBehaviour
    {
        private const float systemMessageLifetime = 5;

        [SerializeField] private GameObject _messageContainer;
        [SerializeField] private Message _messageTemplate;

        public void LogMessagePermanent(string message, string sender)
        {
           var buffer = Instantiate(_messageTemplate, _messageContainer.transform);
           buffer.Create(message, sender);
        }

        public void LogMessage(string message, string sender, float lifeTime)
        {
            var buffer = Instantiate(_messageTemplate, _messageContainer.transform);
            buffer.Create(message, sender);
            Destroy(buffer.gameObject, lifeTime);
        }

        public void LogMessage(string message, string sender, float lifeTime, Color color)
        {
            var buffer = Instantiate(_messageTemplate, _messageContainer.transform);
            buffer.Create(message, sender);
            buffer.SetMessageColor(color);
            Destroy(buffer.gameObject, lifeTime);
        }

        public static void SystemMessage(string message)
        {
            GameLog log = FindObjectOfType<GameLog>();
            if (log == null) return;
            log.LogMessage(message, "System", systemMessageLifetime);
        }

        public static void SystemWarningMessage(string message)
        {
            GameLog log = FindObjectOfType<GameLog>();
            if (log == null) return;
            log.LogMessage(message, "System", systemMessageLifetime, Color.yellow);
        }
    }
}
