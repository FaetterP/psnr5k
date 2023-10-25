using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    class CalibrationPoints : MonoBehaviour
    {
        [SerializeField] private HandleRotate _calibrationK1;
        [SerializeField] private float _maxOffset;

        private void OnEnable()
        {
            _calibrationK1.AddListener(CalibrationK1ChangedHandler);
        }

        private void OnDisable()
        {
            _calibrationK1.RemoveListener(CalibrationK1ChangedHandler);
        }

        private void CalibrationK1ChangedHandler(float value)
        {
            transform.localPosition = new Vector3(0, 0, value * _maxOffset / 100);
        }
    }
}
