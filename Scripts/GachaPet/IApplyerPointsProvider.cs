using UnityEngine;

namespace KotletaGames.RobbyGachaPetModule
{
    public interface IApplyerPointsProvider 
    {
        Transform[] Points { get; }
    }
}