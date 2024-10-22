using TMPro;
using UnityEngine;

namespace KotletaGames.RobbyGachaPetModule
{
    public class GachaPetInfo : GachaPetSimpleInfo
    {
        [SerializeField] public TextMeshProUGUI _countText;

        public void SetCount(string count)
        {
            _countText.text = count;
        }
    }
}