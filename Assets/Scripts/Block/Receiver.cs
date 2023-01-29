using Assets.Scripts.Switches;
using System;
using UnityEngine;

namespace Assets.Scripts.Block
{
    class Receiver : MonoBehaviour
    {
        [SerializeField] private HoldLever _epsilon;
        [SerializeField] private int _minHeight, _maxHeight;
        [SerializeField] private HandleRotateLimitation _sector;
        [SerializeField] private Lever _speed;
        [SerializeField] private Azimuth _azimuth;
        [SerializeField] private HandleRotate _azimuthRotate;
        [SerializeField] private HandleRotateLimitation _bisector;
        [SerializeField] private float _heightSpeed;
        private int _epsilonValue;

        private int _speedValue;
        private int _azimuthStatus;
        private int _azimuthValue;

        private int _sectorValue = 1;
        private int _bisectorValue;

        private float _currentAngle;
        private float _targetAngle;

        private float _currentHeight;
        private float _targetHeight;

        private void Update()
        {
            switch (_azimuthStatus)
            {
                case 0:
                    float rotateDirection = Math.Sign(_targetAngle - _currentAngle) * _speedValue * Time.deltaTime;
                    transform.localEulerAngles += new Vector3(0, rotateDirection, 0);
                    _currentAngle += rotateDirection;

                    if (Math.Abs(_currentAngle - _targetAngle) <= 1.5f * _speedValue)
                    {
                        ResolveTarget();
                    }
                    break;
                case 1:
                    break;
                case 2:
                    transform.localEulerAngles = new Vector3(0, 180 + _azimuthValue / 3, 0);
                    break;
            }

            if (Math.Abs(_currentHeight - _targetHeight) >= 1.5)
            {
                int heightDirection = Math.Sign(_targetHeight - _currentHeight);
                float deltaHeight = heightDirection * _heightSpeed * Time.deltaTime;
                _currentHeight += deltaHeight;
                transform.localEulerAngles += new Vector3(deltaHeight, 0, 0);
            }
        }

        private void OnEnable()
        {
            _speed.AddListener(ChangeSpeed);
            _epsilon.AddListener(ChangeEpsilon);
            _sector.AddListener(ChangeSector);
            _azimuth.AddListener(ChangeAzimuthStatus);
            _azimuthRotate.AddListener(ChangeAzimuthValue);
            _bisector.AddListener(ChangeBisectorValue);
        }

        private void OnDisable()
        {
            _speed.RemoveListener(ChangeSpeed);
            _epsilon.RemoveListener(ChangeEpsilon);
            _sector.RemoveListener(ChangeSector);
            _azimuth.RemoveListener(ChangeAzimuthStatus);
            _azimuthRotate.RemoveListener(ChangeAzimuthValue);
            _bisector.RemoveListener(ChangeBisectorValue);
        }

        private void ChangeSpeed(bool value)
        {
            _speedValue = value ? 2 : 4;
        }

        private void ChangeEpsilon(int value)
        {
            float newValue = _targetHeight + value;
            if (newValue < -60 || newValue > 60)
            {
                return;
            }
            _targetHeight = newValue;
        }

        private void ChangeSector(int value)
        {
            _sectorValue = value * 4 * Math.Sign(_sectorValue);
            ResolveTarget();
        }

        private void ChangeAzimuthStatus(int value)
        {
            _azimuthStatus = value;
        }

        private void ChangeAzimuthValue(int value)
        {
            _azimuthValue = value;
        }

        private void ChangeBisectorValue(int value)
        {
            _bisectorValue = value;
            ResolveTarget();
        }

        private void ResolveTarget()
        {
            _sectorValue = -_sectorValue;
            _targetAngle = _sectorValue;
            _targetAngle += _bisectorValue;
        }
    }
}
