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
            _range.AddListener(ChangeValueRange);
            _delay.AddListener(ChangeValueDelay);
            _handleN.AddListener(ChangeValueN);
            _handleK.AddListener(ChangeValueK);

            ChangeValueRange(_range.Value);
        }

        private void OnDisable()
        {
            _range.RemoveListener(ChangeValueRange);
            _delay.RemoveListener(ChangeValueDelay);
            _handleN.RemoveListener(ChangeValueN);
            _handleK.RemoveListener(ChangeValueK);
        }

        private void ChangeValueRange(float value)
        {
            UpdateValues();
        }

        private void ChangeValueDelay(int value)
        {
            UpdateValues();
        }

        private void ChangeValueN(float value)
        {
            UpdateValues();
        }

        private void ChangeValueK(float value)
        {
            UpdateValues();
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
