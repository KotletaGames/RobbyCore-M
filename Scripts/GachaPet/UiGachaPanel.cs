using KotletaGames.AdditionalModule;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace KotletaGames.RobbyGachaPetModule
{
    [Serializable]
    public class UiGachaPanel : IInitializable, IDisposable
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;

        public void Initialize()
        {
            _openButton.onClick.AddListener(OnOpen);
            _closeButton.onClick.AddListener(OnClose);
        }

        public void Dispose()
        {
            _openButton.onClick.RemoveListener(OnOpen);
            _closeButton.onClick.RemoveListener(OnClose);
        }

        private void OnOpen()
        {
            _panel.ActiveSelf();
        }

        private void OnClose()
        {
            _panel.DisactiveSelf();
        }

    }
}