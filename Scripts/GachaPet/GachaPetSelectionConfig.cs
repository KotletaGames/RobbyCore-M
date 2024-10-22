using UnityEngine;

namespace KotletaGames.RobbyGachaPetModule
{
    [CreateAssetMenu(fileName = "GachaSelection", menuName = "Configs/GachaSelection")]
    public class GachaPetSelectionConfig : ScriptableObject
    {
        [field: Header("Management")]
        [field: SerializeField] public uint Maximum { get; private set; }

        [field: SerializeField] public uint CountClicks { get; private set; }

        [field: Header("Opening")]
        [field: SerializeField][field: Min(0)] public float ShakeDuration { get; private set; }

        [field: SerializeField][field: Min(0)] public float ShakeStrenght { get; private set; }

        [field: SerializeField][field: Min(0)] public int ShakeVibrato { get; private set; }

        [field: Header("View")]
        [field: SerializeField][field: Range(0, 1)] public float ColorFadeWhenNoEnoughtMoney { get; private set; }
    }
}