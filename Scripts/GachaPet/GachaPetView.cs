using KotletaGames.AdditionalModule;
using KotletaGames.Meta.Ui;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace KotletaGames.RobbyGachaPetModule
{
    [Serializable]
    public struct GachaPetView<TItem>
        where TItem : IItemInformation
    {
        [SerializeField] private GameObject _self;
        [SerializeField] private GachaPetInfo _infoPetPrefab;
        [SerializeField] private GachaPetInfo _infoCostPrefab;
        [SerializeField] private CanvasGroup _canvasGroupCost;
        [SerializeField] private Transform _containerPets;
        [SerializeField] private Transform _containerCosts;

        private ColorFader _colorFader;

        public void Init(GachaPetConfig<TItem> gachaPetConfig)
        {
            SpawnPets(gachaPetConfig.Pets);
            SpawnCosts(gachaPetConfig.CostUnlockingDatas);

            _colorFader = new ColorFader(_canvasGroupCost.GetComponentsInChildren<Graphic>(true));
        }

        public void Show()
        {
            _self.SetActive(true);
        }

        public void Hide()
        {
            _self.SetActive(false);
        }

        public void ShowAllowedBuy()
        {
            _canvasGroupCost.interactable = true;
            //_colorFader.Unfade();
        }

        public void ShowForbiddenBuy()
        {
            _canvasGroupCost.interactable = false;
            //_colorFader.Fade();
        }

        private void SpawnPets(PetRatio[] pets)
        {
            foreach (var pet in pets)
            {
                GachaPetInfo gachaPetInfo = Spawn(_infoPetPrefab, _containerPets);
                gachaPetInfo.SetIcon(pet.Pet.Icon);
                gachaPetInfo.SetCount($"{pet.Ratio * 100}%");

                gachaPetInfo.name = $"GachaPetInfo {pet.Pet.Prefab.name}";
            }
        }

        private void SpawnCosts(CostUnlockingData<TItem>[] pets)
        {
            foreach (var costUnlockingData in pets)
            {
                GachaPetInfo gachaPetInfo = Spawn(_infoCostPrefab, _containerCosts);
                gachaPetInfo.SetIcon(costUnlockingData.Item.Icon);
                gachaPetInfo.SetCount(Utility.FormatNumber(costUnlockingData.Count, 2));

                gachaPetInfo.name = $"GachaPetInfo {costUnlockingData.Item.Id}";
            }
        }

        private T Spawn<T>(T prefab, Transform container) where T : GachaPetSimpleInfo
        {
            return UnityEngine.Object.Instantiate(prefab, container);
        }
    }
}