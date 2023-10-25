using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Block.Control
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
            ReflectorChangedHandler(_reflector.Value);
        }

        private void OnDestroy()
        {
            _reflector.RemoveListener(ReflectorChangedHandler);
        }

        private void ReflectorChangedHandler(float value)
        {
            float angle = 15 - 30 * value / 100;
            UpdateAngle(angle);
        }
    }
}
