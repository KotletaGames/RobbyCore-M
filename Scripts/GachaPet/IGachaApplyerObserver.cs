using System;

namespace KotletaGames.RobbyGachaPetModule
{
    public interface IGachaApplyerObserver
    {
        event Action<uint[]> OndIdsApplied;
    }
}