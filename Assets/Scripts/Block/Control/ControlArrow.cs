using Assets.Scripts.Switches;
using UnityEngine;
using System;

namespace Assets.Scripts.Block.Control
{
    class ControlArrow : MonoBehaviour
    {
        [SerializeField] private HandleStep _voltageHandle;
        [SerializeField] private HandleRotate _reflectorHandle;
        [SerializeField] private Transform _center;

        private int _voltageValue;
        private int _reflectorValue;
        private ControlStrategy _currentStrategy;

        private void OnEnable()
        {
            _voltageHandle.AddListener(SetVoltageValue);
            _reflectorHandle.AddListener(SetReflectorValue);
        }

        private void OnDisable()
        {
            _voltageHandle.RemoveListener(SetVoltageValue);
            _reflectorHandle.RemoveListener(SetReflectorValue);
        }

        private void Rotate()
        {
            Destroy(_currentStrategy);

            switch (_voltageValue)
            {
                case 0:
                    //_currentStrategy = gameObject.AddComponent<>();
                    break;
                case 1:
                    _center.localEulerAngles = new Vector3(30, 0, 180);
                    break;
                case 2:
                    _center.localEulerAngles = new Vector3(24, 0, 180);
                    break;
                case 3:
                    _center.localEulerAngles = new Vector3(-28 + _reflectorValue * 34 / 100, 0, 180);
                    break;
                case 4:
                    _center.localEulerAngles = new Vector3(0, 0, 180);
                    break;
                case 5:
                    _center.localEulerAngles = new Vector3(24, 0, 180);
                    break;
                case 6:
                    _center.localEulerAngles = new Vector3(-15, 0, 180);
                    break;
            }
        }

        private void SetVoltageValue(int value)
        {
            _voltageValue = value;
            Rotate();
        }

        private void SetReflectorValue(int value)
        {
            _reflectorValue = value;
            Rotate();
        }
    }
}

