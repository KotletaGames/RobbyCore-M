using KotletaGames.AdditionalModule;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KotletaGames.RobbyGachaPetModule
{
    public class GachaApplyer : IGachaPetMultiplayer, IGachaApplyerObserver
    {
        private readonly GachaPetSelectionConfig _selectionConfig;
        private readonly SetContainerSpawner<GameObject> _setContainerSpawner;
        private readonly IApplyerPointsProvider _applyerPoints;

        public event Action<uint[]> OndIdsApplied;

        private readonly List<GameObject> _pets = new();

        public GachaApplyer(GachaPetSelectionConfig selectionConfig, SetContainerSpawner<GameObject> setContainerSpawner, IApplyerPointsProvider applyerPoints)
        {
            _selectionConfig = selectionConfig;
            _setContainerSpawner = setContainerSpawner;
            _applyerPoints = applyerPoints;
        }

        public float SumMultiplayer { get; private set; } = 0;

        public void Apply(GachaItem[] selecteds)
        {
            if (_pets.Count > 0)
                Clear();

            if (selecteds.Length > 0)
                SumMultiplayer = 0;

            List<uint> ids = new();

            uint counter = 0;
            foreach (var selected in selecteds)
            {
                for (int i = 0; i < selected.Count; i++)
                {
                    if (counter > _selectionConfig.Maximum)
                        break;

                    Transform point = _applyerPoints.Points[counter];
                    GameObject spawned = _setContainerSpawner.Spawn(selected.PetConfig.Prefab, point, point);
                    _pets.Add(spawned);

                    ids.Add(selected.PetConfig.Id);

                    SumMultiplayer += selected.PetConfig.Modifier;

                    counter++;
                }
            }

            OndIdsApplied?.Invoke(ids.ToArray());
        }

        private void Clear()
        {
            foreach (GameObject pet in _pets)
                UnityEngine.Object.Destroy(pet.gameObject);

            SumMultiplayer = 0;

            _pets.Clear();
        }
    }
}