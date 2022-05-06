using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ExtendedUI
{
    public class AudioPlayerToggle : AudioPlayerComponent, IPointerDownHandler
    {
        [SerializeField] private Image _symbol;
        [SerializeField] private Sprite _playSprite;
        [SerializeField] private Sprite _pauseSprite;

        [SerializeField] private bool _started;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (IsLeftClick(eventData))
            {
                if (_started)
                {
                    if (Player.Paused)
                    {
                        Player.UnPause();
                        _symbol.sprite = _pauseSprite;
                    }
                    else
                    {
                        Player.Pause();
                        _symbol.sprite = _playSprite;
                    }
                }
                else
                {
                    Player.Play();
                    _symbol.sprite = _pauseSprite;
                    _started = true;
                }
            }
        }

        private bool IsLeftClick(PointerEventData eventData)
        {
            return eventData.button == PointerEventData.InputButton.Left;
        }

        protected override void Awake()
        {
            void OnReset()
            {
                _started = false;
                _symbol.sprite = _playSprite;
            }

            Player.onReset += OnReset;
        }
    }
}