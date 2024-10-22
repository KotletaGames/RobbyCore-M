using UnityEngine;
using UnityEngine.UI;

namespace KotletaGames.RobbyGachaPetModule
{
    public class GachaPetSimpleInfo : MonoBehaviour
    {
        [SerializeField] public Image _icon;

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }
    }
}