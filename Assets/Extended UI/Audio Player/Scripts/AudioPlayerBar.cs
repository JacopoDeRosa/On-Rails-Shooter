using UnityEngine;
using UnityEngine.UI;

namespace ExtendedUI
{
    public class AudioPlayerBar : AudioPlayerComponent
    {
        [SerializeField] private Slider _slider;

        protected override void Awake()
        {
            base.Awake();
            Player.onClipSet += UpdateSliderValues;
        }

        private void UpdateSliderValues(AudioClip clip)
        {
            _slider.maxValue = clip.length;
            _slider.value = 0;
        }

        protected override void OnTimeUpdate(float time)
        {
            _slider.value = time;
        }
    }
}