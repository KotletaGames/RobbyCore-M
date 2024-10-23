using KotletaGames.AdditionalModule;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KotletaGames.RobbyGiftsModule
{
    public class IncreasingView : MonoBehaviour
    {
        [SerializeField] private Button _open;
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Image _rateImage;
        [SerializeField] private GameObject _fade;

        public event Action OnOpened;

        public string GettingText { get; set; } = "Получить";

        private void OnEnable()
        {
            _open.interactable = false;
            _open.onClick.AddListener(OnNotify);
        }

        private void OnDisable()
        {
            _open.onClick.RemoveAllListeners();
        }

        private void OnDestroy()
        {
            OnOpened = null;
        }

        public void Allow()
        {
            _open.interactable = true;
            SetButtonText(GettingText);
        }

        public void MarkAsOpened()
        {
            _open.interactable = false;
            _fade.ActiveSelf();
            _open.DisactiveSelf();
        }

        public void SetTimeText(string text)
        {
            SetButtonText(text);
        }

        public void SetDescription(string description)
        {
            _descriptionText.text = description;
        }

        public void SetRateColor(Color color)
        {
            _rateImage.color = color;
        }

        public void SetAllowColor(Color color)
        {
            SetButtonColor(color);
        }

        public void SetForbiddenColor(Color color)
        {
            SetButtonColor(color);
        }

        private void SetButtonText(string text)
        {
            _timeText.text = text;
        }
        private void SetButtonColor(Color color)
        {
            _open.image.color = color;
        }

        private void OnNotify()
        {
            OnOpened?.Invoke();
        }
    }
}