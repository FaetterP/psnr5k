using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    class CalibrationPoints : MonoBehaviour
    {
        [SerializeField] private HandleRotate _handleK;
        [SerializeField] private float _maxOffset;

        private void OnEnable()
        {
            _handleK.AddListener(ChangeOffset);
        }

        private void OnDisable()
        {
            _handleK.RemoveListener(ChangeOffset);
        }

        private void ChangeOffset(float value)
        {
            transform.localPosition = new Vector3(0, 0, value * _maxOffset / 100);
        }
    }
}
