using System;
using UnityEngine;

namespace KotletaGames.RobbyGachaPetModule
{
    [Serializable]
    public struct CostUnlockingData<TItem>
        where TItem : IItemInformation
    {
        [field: SerializeField] public TItem Item { get; private set; }

        [field: SerializeField] public int Count { get; private set; }
    }
}