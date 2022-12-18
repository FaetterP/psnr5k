using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    [RequireComponent(typeof(MeshRenderer))]
    class RangeStrobe : MonoBehaviour
    {
        [SerializeField] private HandleRotate _handleRange;
        [SerializeField] private HandleStep _handleDelay;
        [SerializeField] private float _minHeight, _maxHeight;
        private int _valueRange, _valueDelay;
        private MeshRenderer _thisMeshRenderer;

        private void Awake()
        {
            _thisMeshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnEnable()
        {
            _handleRange.AddListener(ChangeValueRange);
            _handleDelay.AddListener(ChangeValueDelay);
        }

        private void OnDisable()
        {
            _handleRange.RemoveListener(ChangeValueRange);
            _handleDelay.RemoveListener(ChangeValueDelay);
        }

        private void ChangeValueRange(int value)
        {
            _valueRange = value;
            UpdateValues();
        }

        private void ChangeValueDelay(int value)
        {
            _valueDelay = value;
            UpdateValues();
        }

        private void UpdateValues()
        {
            if (_valueRange < 5000 && _valueDelay == 0)
            {
                _thisMeshRenderer.enabled = true;
                float z = Mathf.Lerp(_minHeight, _maxHeight, 1f * _valueRange / 5000);

                Vector3 position = transform.localPosition;
                position.z = z;
                transform.localPosition = position;
            }
            else if (_valueRange < 10000 && _valueDelay == 1)
            {
                _thisMeshRenderer.enabled = true;
                float z = Mathf.Lerp(_minHeight, _maxHeight, 1f * (_valueRange - 5000) / 5000);

                Vector3 position = transform.localPosition;
                position.z = z;
                transform.localPosition = position;
            }
            else if (_valueRange < 15000 && _valueDelay == 2)
            {
                _thisMeshRenderer.enabled = true;
                float z = Mathf.Lerp(_minHeight, _maxHeight, 1f * (_valueRange - 10000) / 5000);

                Vector3 position = transform.localPosition;
                position.z = z;
                transform.localPosition = position;
            }
            else
            {
                _thisMeshRenderer.enabled = false;
            }
        }
    }
}
