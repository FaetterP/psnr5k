using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    class RangeStrobe : MonoBehaviour
    {
        [SerializeField] private HandleRotate _range;
        [SerializeField] private HandleRotate _handleN;
        [SerializeField] private HandleRotate _handleK;
        [SerializeField] private HandleStep _delay;
        [SerializeField] private float _minHeight, _maxHeight;

        private void OnEnable()
        {
            _range.AddListener(UpdateValues);
            _delay.AddListener(UpdateValues);
            _handleN.AddListener(UpdateValues);
            _handleK.AddListener(UpdateValues);
        }

        private void OnDisable()
        {
            _range.RemoveListener(UpdateValues);
            _delay.RemoveListener(UpdateValues);
            _handleN.RemoveListener(UpdateValues);
            _handleK.RemoveListener(UpdateValues);
        }

        private float ScaledN => _handleN.Value * 2000 - 1000;
        private float ScaledK => _handleK.Value + 0.5f;

        private int[] _delayOffsets = new int[] { 0, 5000, 10000 };

        private void UpdateValues()
        {
            float range = ScaledK * (_range.Value - 1000) + ScaledN + 1000;
            float offset = _delayOffsets[_delay.Value];

            Vector3 position = transform.localPosition;
            position.z = Mathf.Lerp(_minHeight, _maxHeight, (range - offset) / 5000);
            transform.localPosition = position;
        }
    }
}
