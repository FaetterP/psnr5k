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

        private void UpdateValues()
        {
            float range = _handleK.Value * (_range.Value - 1000) / 100f + _handleN.Value + 1000;

            if (range < 5000 && _delay.Value == 0)
            {
                float z = Mathf.Lerp(_minHeight, _maxHeight, 1f * range / 5000 + _handleN.Value / 100 / 2.5f);

                Vector3 position = transform.localPosition;
                position.z = z;
                transform.localPosition = position;
            }
            else if (range < 10000 && _delay.Value == 1)
            {
                float z = Mathf.Lerp(_minHeight, _maxHeight, 1f * (range - 5000) / 5000 + _handleN.Value / 100 / 2.5f);

                Vector3 position = transform.localPosition;
                position.z = z;
                transform.localPosition = position;
            }
            else if (range < 15000 && _delay.Value == 2)
            {
                float z = Mathf.Lerp(_minHeight, _maxHeight, 1f * (range - 10000) / 5000 + _handleN.Value / 100 / 2.5f);

                Vector3 position = transform.localPosition;
                position.z = z;
                transform.localPosition = position;
            }
        }
    }
}
