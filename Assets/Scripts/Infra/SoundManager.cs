using System;
using Kosta.Infra;
using UnityEngine;

namespace Infra
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [Space]
        [SerializeField] private AudioClip _pickCoin;
        [SerializeField] private AudioClip _pickEnergy;
        [SerializeField] private AudioClip _shoot;
        [SerializeField] private AudioClip _startMusic;
        [SerializeField] private AudioClip _pinataVoice;

        EventManager _eventManager;
        private void Start()
        {
            _eventManager = ServiceLocator.Resolve<EventManager>();

            _eventManager.AfterPickCoin += PlayPickCoin;
            _eventManager.AfterPickEnergy += PlayPickEnergy;
            _eventManager.AfterPlayerShot += PlayShot;
            _eventManager.OnRestartRound += PlayStartMusic;
            _eventManager.OnPinataExploded += PlayPinataVoice;
        }

        private void PlayStartMusic()
        {
            _audioSource.PlayOneShot(_startMusic);
        }
        private void PlayShot()
        {
            _audioSource.PlayOneShot(_shoot);
        }
        private void PlayPickEnergy()
        {
            _audioSource.PlayOneShot(_pickEnergy);
        }
        private void PlayPickCoin()
        {
            _audioSource.PlayOneShot(_pickCoin);
        }

        private void PlayPinataVoice()
        {
            _audioSource.PlayOneShot(_pinataVoice);
        }
        
        private void OnDestroy()
        {
            _eventManager.AfterPickCoin -= PlayPickCoin;
            _eventManager.AfterPickEnergy -= PlayPickEnergy;
            _eventManager.AfterPlayerShot -= PlayShot;
            _eventManager.OnRestartRound -= PlayStartMusic;
            _eventManager.OnPinataExploded -= PlayPinataVoice;
        }
    }
}