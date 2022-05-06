using System.Collections;
using UnityEngine;
using System;

namespace ExtendedUI
{
    public class AudioPlayer : UIElement
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _currentClip;
        [SerializeField] private bool _paused;


        public bool Paused { get => _paused; }
        public float CurrentTime { get => _audioSource.time; }
        public bool IsPlaying { get => _audioSource.isPlaying; }

        public delegate void ClipChangedHandler(AudioClip clip);
        public delegate void TimeChangedHandler(float time);


        public event Action onPlay;
        public event Action onPause;
        public event Action onUnPause;
        public event Action onReset;
        public event ClipChangedHandler onClipSet;
        public event TimeChangedHandler onTimeUpdated;

        private void Start()
        {
            onClipSet?.Invoke(_currentClip);
        }

        public void Play()
        {
            if (_audioSource == null || _currentClip == null) return;

            _audioSource.clip = _currentClip;
            _audioSource.Play();
            StartCoroutine(UpdateTime());

            onPlay?.Invoke();
        }

        public void Pause()
        {
            if (_audioSource == null) return;
            _audioSource.Pause();
            _paused = true;
            onPause?.Invoke();
        }

        public void UnPause()
        {
            if (_audioSource == null) return;
            _audioSource.UnPause();
            _paused = false;
            onUnPause?.Invoke();
        }

        public void ResetPlayer()
        {
            _audioSource.Stop();

            StopAllCoroutines();

            _audioSource.time = 0;
            onTimeUpdated?.Invoke(0);

            onReset?.Invoke();
        }

        public void SetAudioClip(AudioClip clip)
        {
            _currentClip = clip;
            _audioSource.clip = _currentClip;
            onClipSet?.Invoke(clip);
        }

        public void ChangeTime(float time)
        {
            if (_audioSource.isPlaying == false && _paused == false) return;

            if (_audioSource.time + time >= _currentClip.length)
            {
                ResetPlayer();
                return;
            }
            if (_audioSource.time + time < 0)
            {
                _audioSource.time = 0;
                return;
            }
            _audioSource.time += time;

        }


        private IEnumerator UpdateTime()
        {

            while (true)
            {

                if (_paused) yield return null;
                onTimeUpdated?.Invoke(_audioSource.time);
                if (_audioSource.isPlaying == false && _paused == false) break;
                yield return new WaitForFixedUpdate();
            }

            ResetPlayer();
        }
    }
}


