using UnityEngine;

namespace ExtendedUI
{
    public class AudioPlayerComponent : UIElement
    {
        [SerializeField] private AudioPlayer _audioPlayer;

        protected AudioPlayer Player { get => _audioPlayer; }

        protected virtual void Awake()
        {
            _audioPlayer.onTimeUpdated += OnTimeUpdate;
        }
        protected virtual void OnValidate()
        {
            if (_audioPlayer == null)
            {
                _audioPlayer = GetComponentInParent<AudioPlayer>();
                if (_audioPlayer == null)
                {
                    Debug.LogWarning(gameObject + "AudioPlayerComponent couldn't find " +
                        "            an audio player please assing manually");
                }
            }
        }

        protected virtual void OnTimeUpdate(float time)
        {

        }
    }
}