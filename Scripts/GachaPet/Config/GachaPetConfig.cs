using UnityEngine;

namespace KotletaGames.RobbyGachaPetModule
{
    [CreateAssetMenu(fileName = "GachaPet", menuName = "Configs/GachaPet")]
    public class GachaPetConfig<TItem> : ScriptableObject
        where TItem : IItemInformation
    {
        [field: SerializeField] public PetRatio[] Pets { get; private set; }

        [field: SerializeField] public CostUnlockingData<TItem>[] CostUnlockingDatas { get; private set; }

        [field: SerializeField] public GameObject StorePrefab { get; private set; }
    }
}