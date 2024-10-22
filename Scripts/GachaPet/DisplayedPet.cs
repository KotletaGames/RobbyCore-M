using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KotletaGames.RobbyGachaPetModule
{
    public class DisplayedPet : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _count;
        [SerializeField] private TextMeshProUGUI _modifier;

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetCount(string count)
        {
            _count.text = $"x{count}";
        }

        public void SetModifier(string modifier)
        {
            _modifier.text = modifier;
        }

        public void ClearCount()
        {
            _count.text = string.Empty;
        }
    }
}