using UnityEngine;

namespace KotletaGames.RobbyGiftsModule
{
    public interface IGiftShower
    {
        void Show();

        void SetIcon(Sprite icon);

        void SetText(string text);
    }
}