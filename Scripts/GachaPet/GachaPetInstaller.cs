using KotletaGames.AdditionalModule;
using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace KotletaGames.RobbyGachaPetModule
{
    public abstract class GachaPetInstaller<ITtem> : MonoInstaller
        where ITtem : IItemInformation
    {
        [Header("Inventory View")]
        [SerializeField] private Transform _selectedContainer;
        [SerializeField] private Transform _otherContainer;
        [SerializeField] private DisplayedPet _displayedPetPrefab;

        [Header("Audio")]
        [SerializeField] private GachaAudio _gachaAudio;

        [Header("Ui Panel")]
        [SerializeField] private UiGachaPanel _uiGachaPanel;

        [FormerlySerializedAs("_unlockerSpawner")]
        [Header("Unlocker")]
        [SerializeField] private AutoRemovedSpawner<GameObject> _unlockerStorageSpawner;
        [SerializeField] private AutoRemovedSpawner<GameObject> _unlockerItemSpawner;
        [SerializeField] private GachaUnlockerView _gachaUnlockerView;
        [SerializeField] private GameObject _location;

        [Header("Config")]
        [SerializeField] private GachaPetSelectionConfig _config;

        public override void InstallBindings()
        {
            BindConfig();
            BindAudio();
            BindUiPanel();
            BindGachaApplyer();
            BindGachaInventory();
            BindGachaUnlocker();
            BindGachaAcquire();
        }

        private void BindConfig()
        {
            Container
                .BindInstance(_config)
                .AsCached();
        }

        private void BindAudio()
        {
            Container
                .BindInstance(_gachaAudio)
                .AsCached();
        }

        private void BindUiPanel()
        {
            Container
                .BindInterfacesTo<UiGachaPanel>()
                .FromInstance(_uiGachaPanel)
                .AsCached();
        }

        private void BindGachaApplyer()
        {
            Container
                .BindInterfacesAndSelfTo<GachaApplyer>()
                .AsCached()
                .WithArguments(new SetContainerSpawner<GameObject>());
        }

        private void BindGachaInventory()
        {
            Container
                .Bind<GachaInventoryView>()
                .AsCached()
                .WithArguments(_selectedContainer, _otherContainer, _displayedPetPrefab);

            Container
                .BindInterfacesAndSelfTo<GachaInventory>()
                .AsCached();
        }

        private void BindGachaUnlocker()
        {
            Container
                .BindInterfacesAndSelfTo<GachaUnlocker>()
                .AsCached()
                .WithArguments(_unlockerStorageSpawner, _unlockerItemSpawner, _gachaUnlockerView, _location);
        }

        private void BindGachaAcquire()
        {
            Container
                .Bind<GachaAcquire<ITtem>>()
                .AsCached();
        }
    }
}