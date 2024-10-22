using System;
using UnityEngine;

namespace KotletaGames.RobbyGachaPetModule
{
    [Serializable]
    public struct PetRatio
    {
        [field: SerializeField] public PetConfig Pet { get; private set; }

        [field: SerializeField][field: Range(0, 1)] public float Ratio { get; private set; }
    }
}