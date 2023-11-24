using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Block.Control
{
    class Amperage1 : ControlStrategy
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
            float angle = 40 - 40 * _reflector.Value / 100;
            UpdateAngle(angle);
        }
    }
}