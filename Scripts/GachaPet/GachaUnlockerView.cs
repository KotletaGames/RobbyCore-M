using KotletaGames.AdditionalModule;
using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace KotletaGames.RobbyGachaPetModule
{
    [Serializable]
    public struct GachaUnlockerView
    {
        [SerializeField] private GameObject _self;
        [SerializeField] private GameObject _closeButtonContainer;
        [SerializeField] private GameObject _rawImageContainer;
        [SerializeField] private GameObject _storageContainer;
        // [SerializeField] private Image _icon;

        [field: SerializeField] public Button OpeningClicker { get; private set; }
        [field: SerializeField] public GameObject PetContainer { get; private set; }

        [field: SerializeField] public Button CloseButton { get; private set; }

        public void Show()
        {
            _self.ActiveSelf();
        }

        public void Hide()
        {
            _self.DisactiveSelf();
        }

        public void ShowFinal()
        {
            _storageContainer.DisactiveSelf();
            PetContainer.ActiveSelf();
            _closeButtonContainer.ActiveSelf();
        }

        // public void SetPet(GameObject petPrefab)
        // {
        //     Instantiate
        //     // _pet = petPrefab.Insta 
        //     // _icon.sprite = 
        // }

        public void Reset()
        {
            PetContainer.DisactiveSelf();
            _closeButtonContainer.DisactiveSelf();
            _storageContainer.ActiveSelf();
            // _rawImageContainer.ActiveSelf();
        }
    }
}