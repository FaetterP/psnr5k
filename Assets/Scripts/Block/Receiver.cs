using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Block
{
    class Receiver : MonoBehaviour
    {
        [SerializeField] private HoldLever _epsilon;
        [SerializeField] private int _minHeight, _maxHeight;
        [SerializeField] private HandleRotateLimitation _sector;
        [SerializeField] private AnimationCurve _rotateCurve;
        [SerializeField] private Lever _speed;
        [SerializeField] private Azimuth _azimuth;
        [SerializeField] private HandleRotate _azimuthRotate;
        [SerializeField] private HandleRotateLimitation _bisector;
        private int _epsilonValue;
        private int _rotateAmplitude;
        private float _currentHeight;
        private int _speedValue;
        private int _azimuthStatus;
        private int _azimuthValue;
        private int _bisectorValue;

        private void Update()
        {
            switch (_azimuthStatus)
            {
                case 0:
                    int constVal = 100;
                    int r = _rotateAmplitude * constVal;
                    int time = ((int)(Time.realtimeSinceStartup * constVal * _speedValue)) % r;
                    float angle = 180 + _rotateCurve.Evaluate(1f * time / (r)) * _rotateAmplitude;
                    transform.localEulerAngles = new Vector3(0, angle + _bisectorValue, 0);

                    if (_currentHeight + _epsilonValue < _minHeight) { return; }
                    if (_currentHeight + _epsilonValue > _maxHeight) { return; }

                    transform.Rotate(new Vector3(_currentHeight, 0, 0));
                    _currentHeight += _epsilonValue;
                    break;
                case 1:
                    break;
                case 2:
                    transform.localEulerAngles = new Vector3(0, 180 + _azimuthValue / 3, 0);
                    break;
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
        }

        private void ChangeEpsilon(int value)
        {
            _epsilonValue = value;
        }

        private void ChangeSector(int value)
        {
            _rotateAmplitude = value * 4;
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
        }
    }
}
