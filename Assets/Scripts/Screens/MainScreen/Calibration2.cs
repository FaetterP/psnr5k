using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    class Calibration2 : MonoBehaviour
    {
        [SerializeField] private HandleRotate _calibration;
        private float _maxSize;

        private void Awake()
        {
            _maxSize = transform.localScale.z;
        }

        private void OnEnable()
        {
            _calibration.AddListener(SetValue);
        }

        private void OnDisable()
        {
            _calibration.RemoveListener(SetValue);
        }

        private void SetValue(float value)
        {
            Vector3 scale = transform.localScale;
            scale.z = _maxSize * value / 100;
            transform.localScale = scale;
        }
    }
}
