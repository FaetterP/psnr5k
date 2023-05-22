using System;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen.Bulges
{
    enum BulgeType
    {
        Sin, Triangle
    }

    [CreateAssetMenu(fileName = "Bulge", menuName = "ScriptableObjects/Bulge")]
    class BulgeScriptableObject : ScriptableObject
    {
        [SerializeField] private BulgeType _bulgeType;
        [SerializeField] [Range(0, 1)] private float _width;
        [SerializeField] [Range(0, 1)] private float _maxAmplitude;
        [SerializeField] [Range(0, 15000)] private float _range;
        [SerializeField] [Range(0, 200)] private float _azimuthRaw;
        [SerializeField] private AudioClip _audioClip;
        [Header("View Only")]
        [SerializeField] private float _azimuth;

        public BulgeType BulgeType => _bulgeType;
        public float Width => _width;
        public float MaxAmplitude => _maxAmplitude;
        public float Range => _range;
        public float Azimuth => _azimuthRaw;
        public AudioClip AudioClip => _audioClip;

        private void OnValidate()
        {
            float value = _azimuthRaw;
            value = value / 200 * 81;
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
