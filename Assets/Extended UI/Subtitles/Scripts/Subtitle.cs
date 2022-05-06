using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Subtitle
{
    [SerializeField] private Color _senderColor;
    [SerializeField] private string _senderName;
    [SerializeField] private string _content;
    [SerializeField] private int _duration;

    public Color SenderColor { get => _senderColor; }
    public string SenderName { get => _senderName; }
    public string Content { get => _content; }
    public int Duration { get => _duration; }

    public Subtitle(Color senderColor, string senderName, string content, int duration)
    {
        _senderColor = senderColor;
        _senderName = senderName;
        _content = content;
        _duration = duration;
    }
}
