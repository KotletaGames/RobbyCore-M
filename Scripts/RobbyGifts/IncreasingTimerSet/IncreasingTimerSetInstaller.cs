using KotletaGames.AdditionalModule;
using System;
using UnityEngine;
using Zenject;

namespace KotletaGames.RobbyGiftsModule
{
    public class IncreasingTimerSetInstaller : MonoInstaller
    {
        [Header("Configuration")]
        [SerializeField] private IncreasingSetConfig[] _configs;

        [Header("Spawner")]
        [SerializeField] private OnlyContianerPrefabSpawner<IncreasingView> _spawner;

        [Header("Notifier")]
        [SerializeField] private IncreasingNotificationCounter _notificationCounter;

        [Header("Shower")]
        [SerializeField] private IncreasingGiftShower _giftShower;

        [Header("Ui Panel")]
        [SerializeField] private UiIncreasingPanel _uiIncreasingPanel;

        public override void InstallBindings()
        {
            BindGiftShower();
            BindIncreasingNotificationCounter();
            BindSetTimeGift();
            BindUiPanel();
            InjectAllGifts();
        }

        private void BindGiftShower()
        {
            Container
                .BindInterfacesAndSelfTo<IncreasingGiftShower>()
                .FromInstance(_giftShower)
                .AsCached();
        }

        private void InjectAllGifts()
        {
            foreach (var config in _configs)
            {
                foreach (var gift in config.Gifts)
                {
                    Container
                        .QueueForInject(gift);
                }
            }
        }

        private void BindIncreasingNotificationCounter()
        {
            Container
                .BindInterfacesAndSelfTo<IncreasingNotificationCounter>()
                .FromInstance(_notificationCounter)
                .AsCached();
        }

        private void BindSetTimeGift()
        {
            Container
                .BindInterfacesTo<IncreasingTimerSet>()
                .AsCached()
                .WithArguments(_configs, _spawner);
        }

        private void BindUiPanel()
        {
            Container
                .BindInterfacesTo<UiIncreasingPanel>()
                .FromInstance(_uiIncreasingPanel)
                .AsCached();
        }
    }
}