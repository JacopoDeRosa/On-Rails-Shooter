using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ExtendedUI
{
    public class ConfirmationWindowTimed : ConfirmationWindow
    {
        [SerializeField] private float _autoDeclineTime;
        [SerializeField] private RadialSlider _timer;
        [SerializeField] [Range(0.01f, 1f)] private float _timerStep = 0.05f;

        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(AutoDecline());
        }

        private IEnumerator AutoDecline()
        {
            float time = _autoDeclineTime;
            _timer.SetMaxValue(_autoDeclineTime);
            while (time > 0)
            {
                _timer.SetValue(time);
                time -= _timerStep;
                yield return new WaitForSeconds(_timerStep);
            }

            Decline(); 
        }
    }
}
