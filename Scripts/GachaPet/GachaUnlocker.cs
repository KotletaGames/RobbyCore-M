using DG.Tweening;
using KotletaGames.AdditionalModule;
using System;
using UnityEngine;
using Zenject;

namespace KotletaGames.RobbyGachaPetModule
{
    public class GachaUnlocker : IInitializable, IDisposable
    {
        private readonly GachaPetSelectionConfig _selectionConfig;
        private readonly AutoRemovedSpawner<GameObject> _storageSpawner;
        private readonly AutoRemovedSpawner<GameObject> _itemSpawner;
        private readonly GachaUnlockerView _unlockerView;
        private readonly GachaInventory _inventory;
        private readonly GachaAudio _gachaAudio;
        private readonly GameObject _location;

        private Tween _tween = null;
        private PetRatio _petRatio;
        private Transform _spawedStore;
        private int _currentClicks = 0;
        private GameObject _item;
        
        public GachaUnlocker(GachaPetSelectionConfig selectionConfig, AutoRemovedSpawner<GameObject> storageSpawner, 
            AutoRemovedSpawner<GameObject> itemSpawner, GachaUnlockerView unlockerView, GachaInventory inventory, 
            GameObject location, GachaAudio gachaAudio)
        {
            _itemSpawner = itemSpawner;
            _selectionConfig = selectionConfig;
            _storageSpawner = storageSpawner;
            _unlockerView = unlockerView;
            _inventory = inventory;
            _location = location;
            _gachaAudio = gachaAudio;
        }

        public void Initialize()
        {
            _unlockerView.OpeningClicker.onClick.AddListener(OnClickToOpen);
            _unlockerView.CloseButton.onClick.AddListener(OnClose);
        }

        public void Dispose()
        {
            _unlockerView.OpeningClicker.onClick.RemoveListener(OnClickToOpen);
            _unlockerView.CloseButton.onClick.RemoveListener(OnClose);
        }

        public void Unlock(PetRatio petRatio, GameObject storePrefab)
        {
            _location.ActiveSelf();
            _unlockerView.Show();
            _spawedStore = _storageSpawner.Spawn(storePrefab).transform;
            _item = _itemSpawner.Spawn(petRatio.Pet.Prefab);
            _item.transform.parent = _unlockerView.PetContainer.transform;
            _petRatio = petRatio;

            _inventory.Add(_petRatio.Pet);

            GachaPetStatic.RegisterOpening();
        }

        private void OnClickToOpen()
        {
            if (_tween.IsActive() == true && _tween.IsPlaying() == true)
                return;

            if (_currentClicks >= _selectionConfig.CountClicks)
                return;

            _gachaAudio.PlayCracklingEgg();

            _tween = _spawedStore
                .DOShakePosition(_selectionConfig.ShakeDuration, _selectionConfig.ShakeStrenght, _selectionConfig.ShakeVibrato)
                .OnComplete(() =>
                {
                    _currentClicks++;
                    if (_currentClicks < _selectionConfig.CountClicks)
                        return;

                    _gachaAudio.PlayUnlockNewPet();
                    _unlockerView.ShowFinal();

                    GachaPetStatic.UnregisterOpening();
                });
        }

        private void OnClose()
        {
            _unlockerView.Reset();
            _unlockerView.Hide();
            _location.DisactiveSelf();

            _currentClicks = 0;
        }
    }
}