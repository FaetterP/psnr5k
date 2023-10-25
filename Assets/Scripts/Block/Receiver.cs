using Assets.Scripts.Switches;
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
        [SerializeField] private HandleRotateLimitation _bisector;
        [SerializeField] private float _heightSpeed;
        [SerializeField] private float _speedMultiplier = 0.5f;
        [SerializeField] private Block _block;

        private float _rotationSpeed;

        private int _sectorOffset = 1;

        private float _currentAngle = 40;
        private float _targetAngle;

        private float _currentHeight;
        private float _targetHeight;

        private UnityEvent<float> e_onChangeAngle = new UnityEvent<float>();

        public float CurrentHeight => _currentHeight;

        private void Update()
        {
            if (_block.IsLaunched == false)
                return;

            if (_azimuth.Status == 1)
                return;

            float rotateAngle = Math.Sign(_targetAngle - _currentAngle) * _rotationSpeed * Time.deltaTime;
            transform.localEulerAngles += new Vector3(0, rotateAngle, 0);
            _currentAngle += rotateAngle;
            e_onChangeAngle.Invoke(_currentAngle);

            if (_azimuth.Status == 0 && Math.Abs(_currentAngle - _targetAngle) < 1.5)
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
            _speed.AddListener(SpeedChangedHandler);
            _epsilon.AddListener(EpsilonCHangedHandler);
            _sector.AddListener(SectorChangedHandler);
            _azimuth.AddListener(AzimuthStatusChangedHandler);
            _azimuth.HandleRotate.AddListener(AzimuthValueChangedHandler);
            _bisector.AddListener(BisectorChangedHandler);
        }

        private void OnDisable()
        {
            _speed.RemoveListener(SpeedChangedHandler);
            _epsilon.RemoveListener(EpsilonCHangedHandler);
            _sector.RemoveListener(SectorChangedHandler);
            _azimuth.RemoveListener(AzimuthStatusChangedHandler);
            _azimuth.HandleRotate.RemoveListener(AzimuthValueChangedHandler);
            _bisector.RemoveListener(BisectorChangedHandler);
        }

        private void BisectorChangedHandler(int value)
        {
            ResolveTarget();
        }

        private void AzimuthStatusChangedHandler(int value)
        {
            ResolveTarget();
        }

        private void AzimuthValueChangedHandler(float value)
        {
            ResolveTarget();
        }

        private void EpsilonCHangedHandler(int value)
        {
            if (_block.IsLaunched == false)
                return;

            float newValue = _targetHeight + value;
            if (newValue < -60 || newValue > 60)
                return;

            _targetHeight = newValue;
        }

        private void SectorChangedHandler(int value)
        {
            _sectorOffset = value * 5 * Math.Sign(_sectorOffset);
        }

        private void SpeedChangedHandler(bool isSpeedUp)
        {
            _rotationSpeed = isSpeedUp ? 4 : 8;
            _rotationSpeed *= _speedMultiplier;
        }

        private void ResolveTarget()
        {
            if (_azimuth.Status == 2)
            {
                _targetAngle = _azimuth.HandleRotate.Value;
            }

            _sectorOffset *= -1;
            _targetAngle = 100 + _bisector.Value + _sectorOffset;
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
