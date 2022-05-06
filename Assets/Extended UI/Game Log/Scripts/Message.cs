using UnityEngine;
using UnityEngine.UI;
using System;


namespace ExtendedUI
{
    public class Message : MonoBehaviour
    {
        [SerializeField] private Text _textObject;
       

        public void Create(string message, string sender)
        {
            string time = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0');
            string output = "[" +  time + "]" + " [" +  sender + "]" + ": " + message ;
            _textObject.text = output;
        }

        public void SetMessageColor(Color color)
        {
            _textObject.color = color;
        }
    }
}
