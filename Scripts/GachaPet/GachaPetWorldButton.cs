using KotletaGames.AdditionalModule;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KotletaGames.RobbyGachaPetModule
{
    [Serializable]
    public struct GachaPetWorldButton
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _keyText;
        [SerializeField] private TextMeshProUGUI _labelText;
        [SerializeField] private GameObject _desktopArt;
        [SerializeField] private GameObject _mobileArt;

        public void AddListener(Action action)
        {
            _button.onClick.AddListener(action.Invoke);
        }

        public void RemoveListener(Action action)
        {
            _button.onClick.RemoveListener(action.Invoke);
        }

        public void DisplayAsDesktop()
        {
            _desktopArt.ActiveSelf();
            _mobileArt.DisactiveSelf();
        }

        public void DisplayAsMobile()
        {
            _desktopArt.DisactiveSelf();
            _mobileArt.ActiveSelf();
        }

        public void SetInteractionText(string keyText, string labelText)
        {
            _keyText.text = keyText;
            _labelText.text = labelText;
        }
    }
}