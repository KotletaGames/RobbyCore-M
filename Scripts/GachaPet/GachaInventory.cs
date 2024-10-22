using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace KotletaGames.RobbyGachaPetModule
{
    public class GachaInventory : IInitializable, IGachaCountProvider
    {
        private readonly IGachaPetLoader _gachaPetLoader;
        private readonly IGachaPetSaver _gachaPetSaver;
        private readonly GachaPetSelectionConfig _selectionConfig;
        private readonly IGachaCollectionProvider _gachaCollectionProvider;
        private readonly GachaInventoryView _view;
        private readonly GachaApplyer _applyer;

        private readonly Dictionary<uint, GachaItem> _selecteds = new();
        private readonly Dictionary<uint, GachaItem> _others = new();

        public GachaInventory(IGachaPetLoader gachaPetLoader, IGachaPetSaver gachaPetSaver, GachaPetSelectionConfig selectionConfig, IGachaCollectionProvider gachaCollectionProvider, GachaInventoryView view, GachaApplyer applyer)
        {
            _gachaPetLoader = gachaPetLoader;
            _gachaPetSaver = gachaPetSaver;
            _selectionConfig = selectionConfig;
            _gachaCollectionProvider = gachaCollectionProvider;
            _view = view;
            _applyer = applyer;
        }

        public void Initialize()
        {
            List<PetSave<uint, uint>> petCollection = _gachaPetLoader.Load.PetCollection;
            if (petCollection.Count == 0)
                return;

            Count = petCollection.Select(i => (int)i.Value).Sum();

            foreach (var data in petCollection)
            {
                PetConfig petConfig = _gachaCollectionProvider.Pets.FirstOrDefault(i => i.Id == data.Key);
                for (int i = 0; i < data.Value; i++)
                    AddWithoutRedisrtibute(petConfig);
            }

            Redistribute();
        }

        public int Count { get; private set; } = 0;

        public void Add(PetConfig petConfig)
        {
            AddWithoutRedisrtibute(petConfig);
            Save();
            Redistribute();
        }

        private void AddWithoutRedisrtibute(PetConfig petConfig)
        {
            uint id = petConfig.Id;
            if (_others.ContainsKey(id) == false)
            {
                GachaItem gachaItem = new(petConfig);
                _others.Add(id, gachaItem);
            }

            _others[id].Add();
            Count++;
        }

        private void Redistribute()
        {
            TransferFromSelectedToOther();
            TransferFromOtherToSelected();

            GachaItem[] selecteds = GetAsDescending(_selecteds);
            GachaItem[] others = GetAsDescending(_others);

            _view.Update(selecteds, others);
            _applyer.Apply(selecteds);
        }

        private GachaItem[] GetAsDescending(Dictionary<uint, GachaItem> pairs)
        {
            return pairs.Values
                .Where(i => i.Count > 0)
                .OrderByDescending(i => i.PetConfig.Modifier)
                .ToArray();
        }

        private void TransferFromSelectedToOther()
        {
            if (_selecteds.Count == 0)
                return;

            foreach (var key in _selecteds.Keys)
            {
                GachaItem gachaItem = _selecteds[key];
                _others[key].Add(gachaItem.Count);
                gachaItem.Reduce(gachaItem.Count);
            }
        }

        private void TransferFromOtherToSelected()
        {
            GachaItem[] items = GetAsDescending(_others);

            uint counter = 0;
            for (int i = 0; i < items.Length; i++)
            {
                GachaItem item = items[i];
                if (_selecteds.ContainsKey(item.PetConfig.Id) == false)
                {
                    GachaItem gachaItem = new(item.PetConfig);
                    _selecteds.Add(item.PetConfig.Id, gachaItem);
                }

                uint count = Math.Clamp(item.Count, 0, _selectionConfig.Maximum - counter);
                counter += count;

                item.Reduce(count);
                _selecteds[item.PetConfig.Id].Add(count);

                if (counter >= _selectionConfig.Maximum)
                    return;
            }
        }

        private void Save()
        {
            List<PetSave<uint, uint>> gachaCollection = new();
            foreach (var gacha in _others.Values)
            {
                gachaCollection.Add(new()
                {
                    Key = gacha.PetConfig.Id,
                    Value = gacha.Count
                });
            }

            foreach (var gacha in _selecteds.Values)
            {
                var found = gachaCollection.FirstOrDefault(i => i.Key == gacha.PetConfig.Id);
                if (found == default)
                {
                    gachaCollection.Add(new()
                    {
                        Key = gacha.PetConfig.Id,
                        Value = gacha.Count
                    });
                }
                else
                {
                    found.Value += gacha.Count;
                }
            }

            _gachaPetLoader.Load.PetCollection = gachaCollection;
            _gachaPetSaver.Save();
        }
    }
}