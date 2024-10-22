using System;
using System.Collections.Generic;

namespace KotletaGames.RobbyGachaPetModule
{
    [Serializable]
    public class GachaPetSave
    {
        public List<PetSave<uint, uint>> PetCollection = new();
    }
}