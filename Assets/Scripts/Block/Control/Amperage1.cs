using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Block.Control
{
    class Amperage1 : ControlStrategy
    {
        private HandleRotate _reflectorHandle;

        public void Init(HandleRotate reflectorHandle)
        {
            _reflectorHandle = reflectorHandle;
            _reflectorHandle.AddListener(Rotate);
        }

        private void Start()
        {
            Rotate(_reflectorHandle.CurrentValue);
        }

        private void OnDestroy()
        {
            _reflectorHandle.RemoveListener(Rotate);
        }

        private void Rotate(float value)
        {
            float angle = 40 - 40 * value / 100;
            UpdateAngle(angle);
        }
    }
}