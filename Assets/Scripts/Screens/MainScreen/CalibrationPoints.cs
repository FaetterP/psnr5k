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

        private void CalibrationK1ChangedHandler()
        {
            transform.localPosition = new Vector3(0, 0, _calibrationK1.Value * _maxOffset / 100);
        }
    }
}
