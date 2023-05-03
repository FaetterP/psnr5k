using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen.Bulges
{
    abstract class Bulge
    {
        public readonly float Width;
        public readonly float MaxAmplitude;
        public readonly float Range;
        public readonly float Azimuth;
        private GameObject _leftLight;
        private static Transform _leftLine;
        private static GameObject _leftLightPrefab;

        public Bulge(float width, float maxAmplitude, float range, float azimuth, Transform leftLine)
        {
            Width = width;
            MaxAmplitude = maxAmplitude;
            Range = range;
            Azimuth = azimuth;
            _leftLine = leftLine;
            _leftLightPrefab = Resources.Load("LeftLight") as GameObject;
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
                scale.y = height/3;
                _leftLight.transform.localScale = scale;
            }

            if (_leftLight.active == isActive) return;
            _leftLight.transform.localPosition = new Vector3(0, 0, yOffset);
            _leftLight.SetActive(isActive);
        }

        public abstract void GenerateBulge(float[] vector);
    }
}
