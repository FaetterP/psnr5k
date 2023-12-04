using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Block
{
    class Amperage2 : ControlStrategy
    {
        private HandleRotate _reflector;

        public void Init(HandleRotate reflectorHandle)
        {
            _reflector = reflectorHandle;
            _reflector.AddListener(ReflectorChangedHandler);
        }

        private void Start()
        {
            ReflectorChangedHandler();
        }

        private void OnDestroy()
        {
            _reflector.RemoveListener(ReflectorChangedHandler);
        }

        private void ReflectorChangedHandler()
        {
            float angle = 15 - 30 * _reflector.Value / 100;
            UpdateAngle(angle);
        }
    }
}
