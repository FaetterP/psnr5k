using Assets.Scripts.Switches;
using System;
using UnityEngine;

namespace Assets.Scripts.Block.Control
{
    class ControlArrow : MonoBehaviour
    {
        [SerializeField] private HandleStep _voltageHandle;
        [SerializeField] private HandleRotate _reflectorHandle;
        [SerializeField] private Transform _center;
        [SerializeField] private Receiver _receiver;
        [SerializeField] private float _speed;

        private float _currentAngle;
        private float _targetAngle;

        private int _voltageValue;
        private ControlStrategy _currentStrategy;

        private void Awake()
        {
            _currentAngle = _center.transform.localRotation.x;
        }

        private void OnEnable()
        {
            _voltageHandle.AddListener(SetVoltageValue);
        }

        private void OnDisable()
        {
            _voltageHandle.RemoveListener(SetVoltageValue);
        }

        private void UpdateStrategy()
        {
            if (_currentStrategy != null)
            {
                _currentStrategy.RemoveListenerOnAngleChanged(UpdateAngleHandler);
            }
            Destroy(_currentStrategy);

            switch (_voltageValue)
            {
                case 0:
                    _currentStrategy = gameObject.AddComponent<Voltage24>();
                    break;
                case 1:
                    _currentStrategy = gameObject.AddComponent<Voltage6>();
                    break;
                case 2:
                    _currentStrategy = gameObject.AddComponent<VoltageAPCh>();
                    break;
                case 3:
                    _currentStrategy = gameObject.AddComponent<Amperage1>();
                    (_currentStrategy as Amperage1).Init(_reflectorHandle);
                    break;
                case 4:
                    _currentStrategy = gameObject.AddComponent<Amperage2>();
                    (_currentStrategy as Amperage2).Init(_reflectorHandle);
                    break;
                case 5:
                    _currentStrategy = gameObject.AddComponent<AmperageM>();
                    (_currentStrategy as AmperageM).Init(_reflectorHandle);
                    break;
                case 6:
                    _currentStrategy = gameObject.AddComponent<Epsilon>();
                    (_currentStrategy as Epsilon).Init(_receiver);
                    break;
            }

            _currentStrategy.AddListenerOnAngleChanged(UpdateAngleHandler);
        }

        private void SetVoltageValue(int value)
        {
            _voltageValue = value;
            UpdateStrategy();
        }

        private void UpdateAngleHandler(float angle)
        {
            _targetAngle = angle;
        }

        private void Update()
        {
            int sign = Math.Sign(_targetAngle - _currentAngle);
            float rotateAngle = Time.deltaTime * _speed * sign;

            _center.transform.Rotate(new Vector3(rotateAngle, 0, 0));
            _currentAngle += rotateAngle;
        }
    }
}

