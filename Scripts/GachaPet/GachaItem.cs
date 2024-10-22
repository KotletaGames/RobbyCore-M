using System;

namespace KotletaGames.RobbyGachaPetModule
{
    public class GachaItem
    {
        public readonly PetConfig PetConfig;

        public GachaItem(PetConfig petConfig)
        {
            PetConfig = petConfig;
            Count = 0;
        }

        public uint Count { get; private set; }

        public void Add(uint value = 1)
        {
            Count = Math.Clamp(Count + value, 0, uint.MaxValue);
        }

        public void Reduce(uint value = 1)
        {
            Count = Math.Clamp(Count - value, 0, uint.MaxValue);
        }
    }
}