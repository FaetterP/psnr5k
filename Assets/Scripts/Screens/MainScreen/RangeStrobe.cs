﻿using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    [RequireComponent(typeof(MeshRenderer))]
    class RangeStrobe : MonoBehaviour
    {
        [SerializeField] private HandleRotate _handleRange;
        [SerializeField] private HandleRotate _handleN;
        [SerializeField] private HandleStep _handleDelay;
        [SerializeField] private float _minHeight, _maxHeight;
        private float _valueRange, _valueDelay, _valueN;
        private MeshRenderer _thisMeshRenderer;

        private void Awake()
        {
            _thisMeshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnEnable()
        {
            _handleRange.AddListener(ChangeValueRange);
            _handleDelay.AddListener(ChangeValueDelay);
            _handleN.AddListener(ChangeValueN);

            ChangeValueRange(_handleRange.CurrentValue);
        }

        private void OnDisable()
        {
            _handleRange.RemoveListener(ChangeValueRange);
            _handleDelay.RemoveListener(ChangeValueDelay);
            _handleN.RemoveListener(ChangeValueN);
        }

        private void ChangeValueRange(float value)
        {
            _valueRange = value;
            UpdateValues();
        }

        private void ChangeValueDelay(int value)
        {
            _valueDelay = value;
            UpdateValues();
        }

        private void ChangeValueN(float value)
        {
            _valueN = value;
            UpdateValues();
        }

        private void UpdateValues()
        {
            if (_valueRange < 5000 && _valueDelay == 0)
            {
                float z = Mathf.Lerp(_minHeight, _maxHeight, 1f * _valueRange / 5000 + _valueN / 100 / 2.5f);

                Vector3 position = transform.localPosition;
                position.z = z;
                transform.localPosition = position;
            }
            else if (_valueRange < 10000 && _valueDelay == 1)
            {
                float z = Mathf.Lerp(_minHeight, _maxHeight, 1f * (_valueRange - 5000) / 5000 + _valueN / 100 / 2.5f);

                Vector3 position = transform.localPosition;
                position.z = z;
                transform.localPosition = position;
            }
            else if (_valueRange < 15000 && _valueDelay == 2)
            {
                float z = Mathf.Lerp(_minHeight, _maxHeight, 1f * (_valueRange - 10000) / 5000 + _valueN / 100 / 2.5f);

                Vector3 position = transform.localPosition;
                position.z = z;
                transform.localPosition = position;
            }
        }
    }
}
