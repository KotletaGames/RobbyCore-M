using DG.Tweening;
using KotletaGames.AdditionalModule;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace KotletaGames.RobbyTriggersModule
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class TriggerZone : MonoBehaviour
    {
        [SerializeField] protected GameObject _container;
        [SerializeField] private Image _progressBar;
        [SerializeField][Min(0)] private float _fillingDuration;
        [SerializeField] private bool _isUsingRepetition;
        [SerializeField] private bool _isInstantly;

        public event Action OnComplated;

        private PlayerTriggerZoneMarker _player;
        private Tween _tween;

        private const float _minimumDuration = 0.01f;

        [Inject]
        private void Construct(PlayerTriggerZoneMarker player)
        {
            _player = player;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<PlayerTriggerZoneMarker>() != _player)
                return;

            StartFillBar();
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.GetComponent<PlayerTriggerZoneMarker>() != _player)
                return;

            CancelFillBar();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerTriggerZoneMarker>() != _player)
                return;

            StartFillBar();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<PlayerTriggerZoneMarker>() != _player)
                return;

            CancelFillBar();
        }

        public void Hide()
        {
            _container.DisactiveSelf();
        }

        private void StartFillBar()
        {
            float remainingDuration = _isInstantly == true ? _minimumDuration : 1 - _progressBar.fillAmount * _fillingDuration;

            _tween?.Kill();
            _tween = _progressBar
                .DOFillAmount(1, remainingDuration)
                .SetEase(Ease.Linear)
                .OnComplete(OnComplate);
        }

        private Tween CancelFillBar()
        {
            float remainingDuration = _isInstantly == true ? _minimumDuration : _progressBar.fillAmount * _fillingDuration;

            _tween?.Kill();
            _tween = _progressBar
                .DOFillAmount(0, remainingDuration)
                .SetEase(Ease.Linear);

            return _tween;
        }

        private void OnComplate()
        {
            OnComplated?.Invoke();

            if (_isUsingRepetition == true)
                CancelFillBar().OnComplete(StartFillBar);
        }
    }
}