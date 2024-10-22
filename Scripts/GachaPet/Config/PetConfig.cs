using UnityEngine;

namespace KotletaGames.RobbyGachaPetModule
{
    [CreateAssetMenu(fileName = "Pet", menuName = "Configs/Pet")]
    public class PetConfig : ScriptableObject
    {
        [field: SerializeField] public uint Id { get; private set; }

        [field: SerializeField] public float Modifier { get; private set; }

        [field: SerializeField] public Sprite Icon { get; private set; }

        [field: SerializeField] public GameObject Prefab { get; private set; }

        /*
        [CustomEditor(typeof(PetConfig))]
        public class PetConfigEditor : Editor
        {
            private PetConfig _petConfig;
            private void OnEnable() => _petConfig = target as PetConfig;

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                if (_petConfig.Icon == null)
                    return;

                _petConfig.Icon = EditorGUILayout.ObjectField(_petConfig.Icon, typeof(Sprite), true,
                    GUILayout.Height(128), GUILayout.Width(128)) as Sprite;
            }
        }*/
    }
}