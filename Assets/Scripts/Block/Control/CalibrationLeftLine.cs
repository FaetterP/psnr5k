using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Block.Control
{
    class CalibrationLeftLine : MonoBehaviour
    {
        [SerializeField] private HandleRotate _handleK;

        private void OnEnable()
        {
            _handleK.AddListener(ChangeValue);
        }

        private void OnDisable()
        {
            _handleK.RemoveListener(ChangeValue);
        }

        private void ChangeValue(float value)
        {
            transform.localScale = new Vector3(1, 1, value / 100);
        }
    }
}
