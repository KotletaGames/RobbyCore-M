using UnityEngine;

namespace KotletaGames.RobbyGiftsModule
{
    [CreateAssetMenu(fileName = "TimeGif", menuName = "TimeGif/TimeGif")]
    public class IncreasingSetConfig : ScriptableObject
    {
        [field: SerializeField][field: Min(0)] public float TimeMinute { get; private set; }
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Color RateColor { get; private set; }
        [field: SerializeField] public Color AllowButtonColor { get; private set; }
        [field: SerializeField] public Color ForbiddenButtonColor { get; private set; }
        [field: SerializeReference][field: SubclassSelector] public IReceiveGift[] Gifts { get; private set; }
    }
}