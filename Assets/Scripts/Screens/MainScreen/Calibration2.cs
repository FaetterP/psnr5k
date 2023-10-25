using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    class Calibration2 : MonoBehaviour
    {
        [SerializeField] private HandleRotate _calibrationK2;
        private float _maxSize;

        private void Awake()
        {
            _maxSize = transform.localScale.z;
        }

        private void OnEnable()
        {
            _calibrationK2.AddListener(CalibrationK2ChangedHandler);
        }

        private void OnDisable()
        {
            _calibrationK2.RemoveListener(CalibrationK2ChangedHandler);
        }

        private void CalibrationK2ChangedHandler(float value)
        {
            Vector3 scale = transform.localScale;
            scale.z = _maxSize * value / 100;
            transform.localScale = scale;
        }
    }
}
