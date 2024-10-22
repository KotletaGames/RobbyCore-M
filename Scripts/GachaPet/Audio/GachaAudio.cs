using System;
using UnityEngine;

namespace KotletaGames.RobbyGachaPetModule
{
    [Serializable]
    public class GachaAudio
    {
        [SerializeField] private AudioSource _audioSource;

        [Header("Clips")]
        [SerializeField] private AudioClip _unlockNewPet;
        [SerializeField] private AudioClip _cracklingEgg;

        public void PlayUnlockNewPet()
        {
            Play(_unlockNewPet);
        }

        public void PlayCracklingEgg()
        {
            Play(_cracklingEgg);
        }

        private void Play(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }
    }
}