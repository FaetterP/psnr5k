using Assets.Scripts.Switches;
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
        [SerializeField] private HandleRotate _azimuthRotate;
        [SerializeField] private Azimuth _azimuth;
        [SerializeField] private HandleRotateLimitation _bisector;
        [SerializeField] private float _heightSpeed;
        [SerializeField] private float _speedMultiplier = 0.5f;
        [SerializeField] private Block _block;

        private bool _blockIsActive;

        private float _speedValue;
        private int _azimuthStatus;
        private float _azimuthValue;

        private int _sectorValue = 1;
        private int _bisectorValue;

        private float _currentAngle = 40;
        private float _targetAngle;

        private float _currentHeight;
        private float _targetHeight;

        private EventFloat e_onChangeAngle = new EventFloat();

        public float CurrentHeight => _currentHeight;

        private void Update()
        {
            if (_blockIsActive == false)
                return;

            if (_azimuthStatus == 1)
                return;

            float rotateAngle = Math.Sign(_targetAngle - _currentAngle) * _speedValue * Time.deltaTime;
            transform.localEulerAngles += new Vector3(0, rotateAngle, 0);
            _currentAngle += rotateAngle;
            e_onChangeAngle.Invoke(_currentAngle);

            if (_azimuthStatus == 0 && Math.Abs(_currentAngle - _targetAngle) < 1.5)
            {
                ResolveTarget();
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
            _block.AddListenerLaunchEnd(ChangeIsBlockActive);
        }

        private void OnDisable()
        {
            _speed.RemoveListener(ChangeSpeed);
            _epsilon.RemoveListener(ChangeEpsilon);
            _sector.RemoveListener(ChangeSector);
            _azimuth.RemoveListener(ChangeAzimuthStatus);
            _azimuthRotate.RemoveListener(ChangeAzimuthValue);
            _bisector.RemoveListener(ChangeBisectorValue);
            _block.RemoveListenerLaunchEnd(ChangeIsBlockActive);
        }

        private void ChangeSpeed(bool value)
        {
            _speedValue = value ? 4 : 8;
            _speedValue *= _speedMultiplier;
        }

        private void ChangeEpsilon(int value)
        {
            if (_blockIsActive == false)
                return;

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
            //ResolveTarget();
        }

        private void ChangeAzimuthStatus(int value)
        {
            _azimuthStatus = value;
            if (_azimuthStatus == 2)
            {
                _targetAngle = _azimuthValue;
            }
        }

        private void ChangeAzimuthValue(float value)
        {
            _azimuthValue = value;
            if (_azimuthStatus == 2)
            {
                _targetAngle = _azimuthValue;
            }
        }

        private void ChangeBisectorValue(int value)
        {
            _bisectorValue = value;
            //ResolveTarget();
        }

        private void ChangeIsBlockActive()
        {
            _blockIsActive = true;
        }

        private void ResolveTarget()
        {
            _sectorValue *= -1;
            _targetAngle = 100 + _bisectorValue + _sectorValue;
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
