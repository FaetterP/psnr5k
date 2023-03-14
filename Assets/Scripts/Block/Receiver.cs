﻿using Assets.Scripts.Switches;
using Assets.Scripts.Utilities;
using System;
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField] private float _speedMultiplier = 0.5f;

        private float _speedValue;
        private int _azimuthStatus;
        private int _azimuthValue;

        private int _sectorValue = 1;
        private int _bisectorValue;

        private float _currentAngle = 40;
        private float _targetAngle;

        private float _currentHeight;
        private float _targetHeight;

        private EventFloat e_onChangeAngle = new EventFloat();

        private void Update()
        {
            switch (_azimuthStatus)
            {
                case 0:
                    float rotateDirection = Math.Sign(_targetAngle - _currentAngle) * _speedValue * Time.deltaTime;
                    transform.localEulerAngles += new Vector3(0, rotateDirection, 0);
                    _currentAngle += rotateDirection;
                    e_onChangeAngle.Invoke(_currentAngle);

                    if (Math.Abs(_currentAngle - _targetAngle) <= 1.5f * _speedValue)
                    {
                        ResolveTarget();
                    }
                    break;
                case 1:
                    break;
                case 2:
                    if (Math.Abs(_targetAngle - _currentAngle) < 1)
                    {
                        return;
                    }
                    rotateDirection = Math.Sign(_targetAngle - _currentAngle) * _speedValue * Time.deltaTime;
                    transform.localEulerAngles += new Vector3(0, rotateDirection, 0);
                    _currentAngle += rotateDirection;

                    e_onChangeAngle.Invoke(_currentAngle);
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
            _speedValue = value ? 4 : 8;
            _speedValue *= _speedMultiplier;
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
            _sectorValue = value * 5 * Math.Sign(_sectorValue);
            ResolveTarget();
        }

        private void ChangeAzimuthStatus(int value)
        {
            _azimuthStatus = value;
        }

        private void ChangeAzimuthValue(float value)
        {
            _targetAngle = value;
        }

        private void ChangeBisectorValue(int value)
        {
            _bisectorValue = value;
            ResolveTarget();
        }

        private void ResolveTarget()
        {
            _sectorValue *= -1;
            _targetAngle = 100 + _sectorValue;
        }

        public void AddListener(UnityAction<float> action)
        {
            e_onChangeAngle.AddListener(action);
        }

        public void RemoveListener(UnityAction<float> action)
        {
            e_onChangeAngle.RemoveListener(action);
        }
    }
}
