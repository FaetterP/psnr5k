using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen.Bulges
{
    abstract class Bulge
    {
        public readonly float Width;
        public readonly float MaxAmplitude;
        public readonly float Range;
        public readonly float Azimuth;

        private readonly AudioSource _audioSource;
        private GameObject _leftLight;

        private static Transform _leftLine;
        private static GameObject _leftLightPrefab;

        public Bulge(float width, float maxAmplitude, float range, float azimuth, Transform leftLine, string sound, AudioSource audioSource)
        {
            Width = width;
            MaxAmplitude = maxAmplitude;
            Range = range;
            Azimuth = azimuth;
            _leftLine = leftLine;
            _leftLightPrefab = Resources.Load("LeftLight") as GameObject;
            _audioSource = audioSource;

            _audioSource.clip = Resources.Load<AudioClip>($"Sounds/{sound}");
        }

        public void SetActive(bool isActive, float yOffset, float height)
        {
            if (_leftLight == null)
            {
                _leftLight = GameObject.Instantiate(_leftLightPrefab, _leftLine);
            }

            if (isActive)
            {
                Vector3 scale = _leftLight.transform.localScale;
                scale.z = Width;
                scale.y = height / 3;
                _leftLight.transform.localScale = scale;

                Debug.Log(height * 8);
                _audioSource.volume = Mathf.Clamp01(height * 8);
            }

            if (_leftLight.active == isActive) return;
            _leftLight.transform.localPosition = new Vector3(0, 0, yOffset);
            _leftLight.SetActive(isActive);
            _audioSource.enabled = isActive;
        }

        public abstract void GenerateBulge(float[] vector);
    }
}
