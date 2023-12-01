using System;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen.Splashes
{
    enum SplashType
    {
        Sin, Triangle
    }

    [CreateAssetMenu(fileName = "Splash", menuName = "ScriptableObjects/Splash")]
    class SplashSettings : ScriptableObject
    {
        [SerializeField] private SplashType _splashType;
        [SerializeField][Range(0, 1)] private float _width;
        [SerializeField][Range(0, 1)] private float _maxAmplitude;
        [SerializeField][Range(50, 15000)] private float _range;
        [SerializeField][Range(0, 81)] private float _azimuthRaw;
        [SerializeField] private AudioClip _audioClip;
        [Header("View Only")]
        [SerializeField] private float _azimuth;

        public SplashType SplashType => _splashType;
        public float Width => _width;
        public float MaxAmplitude => _maxAmplitude;
        public float Range => _range;
        public float Azimuth => _azimuthRaw;
        public AudioClip AudioClip => _audioClip;

        private void OnValidate()
        {
            float value = _azimuthRaw;
            if (value > 40.5)
            {
                value -= 40.5f;
            }
            else
            {
                value += 19.5f;
            }
            _azimuth = (float)Math.Round(value, 2);
        }
    }
}
