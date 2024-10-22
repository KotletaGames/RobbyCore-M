using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using KotletaGames.AdditionalModule;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace KotletaGames.RobbyGiftsModule
{
    [Serializable]
    public class IncreasingNotificationCounter : IInitializable
    {
        [Header("Color")] 
        [SerializeField] private Image _imageIndicator;
        [SerializeField] private Color _allowNewGiftColor;
        [SerializeField] private Color _normalColor;

        [Header("Remaining")]
        [SerializeField] private TextMeshProUGUI _remainingTimeText;

        [Header("Shaking")] 
        [SerializeField] private RectTransform _giftIcon;
        [SerializeField] private float _durationShaking;
        [SerializeField] private float _strenghtShaking;

        [Header("Counter")] 
        [SerializeField] private GameObject _container;
        [SerializeField] private TextMeshProUGUI _counterText;
        [field: SerializeField] public TextMeshProUGUI NextGiftText;

        [Header("Idle Animtion When gifts are available")] 
        [SerializeField] private float _delayIdle;

        [SerializeField] private float _durationIdle;
        [SerializeField] private float _strenghtIdle;

        private bool _isShaking = false;

        private int _totalCount;
        private int _freeCounter = 0;

        public void Initialize()
        {
            UpdateViewByCount();
            IdleAnimation().Forget();
        }

        public void SetTotalCount(int totalCount)
        {
            _totalCount = totalCount;
        }

        public void AddAllowed()
        {
            AnimateShaking(_durationShaking, _strenghtShaking);

            _freeCounter++;
            UpdateViewByCount();
        }

        public void AddForbidden()
        {
            _freeCounter--;
            UpdateViewByCount();
        }

        public void TrackTime(IncreasingSlot[] slots)
        {
            int delay = 500;

            UniTask.Create(async () =>
            {
                foreach (var slot in slots)
                {
                    while (slot.IsComplated == false)
                    {
                        _remainingTimeText.text = slot.FormattedTime;
                        await UniTask.Delay(delay);
                    }
                }
            });
        }

        private void UpdateViewByCount()
        {
            _counterText.text = _freeCounter.ToString();

            if (_freeCounter > 0)
            {
                _container.ActiveSelf();
                _counterText.enabled = true;
                _imageIndicator.color = _allowNewGiftColor;
            }
            else
            {
                _container.DisactiveSelf();
                _counterText.enabled = false;
                _imageIndicator.color = _normalColor;
            }
        }

        private async UniTaskVoid IdleAnimation()
        {
            while (this != null)
            {
                await UniTask.WaitWhile(() => _freeCounter <= 0);
                await UniTask.Delay(_delayIdle.ToDelayMillisecond());

                if (_freeCounter <= 0)
                    continue;

                AnimateShaking(_durationIdle, _strenghtIdle);
            }
        }

        private void AnimateShaking(float durationShaking, float strenghtShaking)
        {
            if (_isShaking == true)
                return;

            _isShaking = true;

            _giftIcon.localScale = Vector3.one;
            _giftIcon.rotation = Quaternion.identity;

            _giftIcon.DOShakeScale(durationShaking, strenghtShaking);
            _giftIcon.transform
                .DOShakeRotation(durationShaking, strenghtShaking)
                .OnComplete(() => _isShaking = false);
        }
    }
}