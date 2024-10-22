using Cysharp.Threading.Tasks;
using DG.Tweening;
using KotletaGames.AdditionalModule;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace KotletaGames.RobbyGiftsModule
{
    [Serializable]
    public class IncreasingGiftShower : IIncreasingGiftShower
    {
        [Header("Info")]
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        [Header("Animation")]
        [SerializeField] private Transform _container;
        [SerializeField][Min(0)] private float _showingDuration;
        [SerializeField][Min(0)] private float _showingDelay;
        [SerializeField][Min(0)] private float _hiddingDuration;
        [SerializeField] private Ease _ease;

        public void Show()
        {
            _container.localScale = Vector3.zero;
            _container.ActiveSelf();

            UniTask.Create(async () =>
            {
                await _container
                    .DOScale(Vector3.one, _showingDuration)
                    .SetEase(_ease)
                    .AsyncWaitForCompletion();

                await UniTask.Delay(_showingDelay.ToDelayMillisecond());

                await _container
                    .DOScale(Vector3.zero, _hiddingDuration)
                    .SetEase(_ease)
                    .AsyncWaitForCompletion();

                _container.DisactiveSelf();
            });
        }

        public void SetIcon(Sprite icon)
        {
            _image.sprite = icon;
        }

        public void SetText(string text)
        { 
            _text.text = text;
        }
    }
}