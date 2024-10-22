using System;

namespace KotletaGames.RobbyGachaPetModule
{
    [Serializable]
    public class PetSave<K, V>
    {
        public K Key;
        public V Value;
    }
}