using Assets.Switches;
using UnityEngine;

namespace Assets.Block
{
    class Receiver : MonoBehaviour
    {
        [SerializeField] private HoldLever _epsilon;
        [SerializeField] private int _minHeight, _maxHeight;
        [SerializeField] private HandleRotateLimitation _sector;
        [SerializeField] private AnimationCurve _rotateCurve;
        [SerializeField] private Lever _speed;
        private int _epsilonValue;
        private int _rotateAmplitude;
        private float _currentHeight;
        private int _speedValue;

        private void Awake()
        {

        }

        private void Update()
        {
            int constVal = 100;
            int r = _rotateAmplitude * constVal;
            int time = ((int)(Time.realtimeSinceStartup * constVal * _speedValue)) % r;
            float angle = _rotateCurve.Evaluate(1f * time / (r)) * _rotateAmplitude;
            transform.localEulerAngles = new Vector3(0, angle, 0);

            if (_currentHeight + _epsilonValue < _minHeight) { return; }
            if (_currentHeight + _epsilonValue > _maxHeight) { return; }

            transform.Rotate(new Vector3(_currentHeight, 0, 0));
            _currentHeight += _epsilonValue;
        }

        private void OnEnable()
        {
            _speed.AddListener(ChangeSpeed);
            _epsilon.AddListener(ChangeEpsilon);
            _sector.AddListener(ChangeSector);
        }

        private void OnDisable()
        {
            _speed.RemoveListener(ChangeSpeed);
            _epsilon.RemoveListener(ChangeEpsilon);
            _sector.RemoveListener(ChangeSector);
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
    }
}
