using UnityEngine;

namespace KotletaGames.RobbyGachaPetModule
{
    public interface IItemInformation 
    { 
        int Id { get; }

        Sprite Icon { get; }
    }
}