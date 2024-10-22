using System;

namespace KotletaGames.RobbyGachaPetModule
{
    public interface IInventoryItemReceiver
    {
        event Action OnUpdate;

        int CountBy(int id);

        bool Reduce(int id, int count);
    }
}