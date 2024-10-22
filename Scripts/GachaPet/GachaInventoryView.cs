using KotletaGames.AdditionalModule;
using System.Collections.Generic;
using UnityEngine;

namespace KotletaGames.RobbyGachaPetModule
{
    public class GachaInventoryView
    {
        private readonly GachaPetSelectionConfig _selectionConfig;
        private readonly Transform _selectedContainer;
        private readonly Transform _otherContainer;
        private readonly DisplayedPet _prefab;

        private readonly SetContainerSpawner<DisplayedPet> _spawner = new();
        private readonly List<DisplayedPet> _displayedPets = new();

        public GachaInventoryView(GachaPetSelectionConfig selectionConfig, Transform selectedContainer, Transform otherContainer, DisplayedPet prefab)
        {
            _selectionConfig = selectionConfig;
            _selectedContainer = selectedContainer;
            _otherContainer = otherContainer;
            _prefab = prefab;
        }

        public void Update(GachaItem[] selecteds, GachaItem[] others)
        {
            if (_displayedPets.Count > 0)
                Clear();

            uint counter = 0;
            foreach (var selected in selecteds)
            {
                for (int i = 0; i < selected.Count; i++)
                {
                    if (counter > _selectionConfig.Maximum)
                        break;

                    DisplayedPet displayedPet = GetDisplayedPet(selected, _selectedContainer);
                    displayedPet.ClearCount();

                    counter++;
                }
            }

            foreach (var other in others)
            {
                DisplayedPet displayedPet = GetDisplayedPet(other, _otherContainer);
                displayedPet.SetCount(other.Count.ToString());
            }
        }

        private void Clear()
        {
            foreach (DisplayedPet displayedPet in _displayedPets)
                UnityEngine.Object.Destroy(displayedPet.gameObject);

            _displayedPets.Clear();
        }

        private DisplayedPet GetDisplayedPet(GachaItem other, Transform container)
        {
            DisplayedPet displayedPet = _spawner.Spawn(_prefab, container, container);
            displayedPet.SetIcon(other.PetConfig.Icon);
            displayedPet.SetModifier(other.PetConfig.Modifier.ToString());

            _displayedPets.Add(displayedPet);

            return displayedPet;
        }
    }
}