namespace KotletaGames.RobbyGachaPetModule
{
    public class GachaAcquire<TItem>
        where TItem : IItemInformation
    {
        private readonly IInventoryItemReceiver _characterInputReceiver;
        private readonly GachaUnlocker _gachaUnlocker;

        public GachaAcquire(IInventoryItemReceiver characterInputReceiver, GachaUnlocker gachaUnlocker)
        {
            _characterInputReceiver = characterInputReceiver;
            _gachaUnlocker = gachaUnlocker;
        }

        public void Buy(GachaPetConfig<TItem> gachaPetConfig)
        {
            SpendResource(gachaPetConfig.CostUnlockingDatas);
            _gachaUnlocker.Unlock(GetRandomPetByRation(gachaPetConfig.Pets), gachaPetConfig.StorePrefab);
        }

        private void SpendResource(CostUnlockingData<TItem>[] costUnlockingDatas)
        {
            foreach (var costUnlockingData in costUnlockingDatas)
                _characterInputReceiver.Reduce(costUnlockingData.Item.Id, costUnlockingData.Count);
        }

        private PetRatio GetRandomPetByRation(PetRatio[] petRatios)
        {
            float randomNumber = UnityEngine.Random.Range(0f, 1.001f); // Генерируем случайное число от 0 до 1

            float cumulative = 0f;
            for (int i = 0; i < petRatios.Length; i++)
            {
                cumulative += petRatios[i].Ratio;
                if (randomNumber < cumulative)
                    return petRatios[i];
            }

            return petRatios[0];
        }
    }
}