using UnityEngine;
using UnityEngine.UI;

namespace ExtendedUI
{
    public class AudioPlayerTimer : AudioPlayerComponent
    {
        [SerializeField] private Text _currentTimeText;
        [SerializeField] private Text _maxTimeText;


        protected override void Awake()
        {
            base.Awake();
            Player.onClipSet += SetMaxTime;
        }

        private void SetMaxTime(AudioClip clip)
        {
            _maxTimeText.text = EasyExtensions.ToClockFormat(clip.length);
        }

        protected override void OnTimeUpdate(float time)
        {
            _currentTimeText.text = EasyExtensions.ToClockFormat(time);
        }
    }
}