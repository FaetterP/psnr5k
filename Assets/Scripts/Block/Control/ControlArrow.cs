using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Block.Control
{
    class ControlArrow : MonoBehaviour
    {
        [SerializeField] private HandleStep _voltageHandle;
        [SerializeField] private HandleRotate _reflectorHandle;
        [SerializeField] private Transform _center;

        private int _voltageValue;
        private ControlStrategy _currentStrategy;

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
            Destroy(_currentStrategy);

            switch (_voltageValue)
            {
                case 0:
                    _currentStrategy = gameObject.AddComponent<Voltage24>();
                    (_currentStrategy as Voltage24).Init(_center);
                    break;
                case 1:
                    _currentStrategy = gameObject.AddComponent<Voltage6>();
                    (_currentStrategy as Voltage6).Init(_center);
                    break;
                case 2:
                    _currentStrategy = gameObject.AddComponent<VoltageAPCh>();
                    (_currentStrategy as VoltageAPCh).Init(_center);
                    break;
                case 3:
                    _currentStrategy = gameObject.AddComponent<Amperage1>();
                    (_currentStrategy as Amperage1).Init(_center, _reflectorHandle);
                    break;
                case 4:
                    _currentStrategy = gameObject.AddComponent<Amperage2>();
                    (_currentStrategy as Amperage2).Init(_center);
                    break;
                case 5:
                    _currentStrategy = gameObject.AddComponent<AmperageM>();
                    (_currentStrategy as AmperageM).Init(_center);
                    break;
                case 6:
                    _currentStrategy = gameObject.AddComponent<Epsilon>();
                    (_currentStrategy as Epsilon).Init(_center);
                    break;
            }
        }

        private void SetVoltageValue(int value)
        {
            _voltageValue = value;
            UpdateStrategy();
        }
    }
}

