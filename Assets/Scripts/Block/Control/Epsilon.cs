using UnityEngine;

namespace Assets.Scripts.Block.Control
{
    class Epsilon : ControlStrategy
    {
        private Receiver _receiver;

        public void Init(Receiver receiver)
        {
            _receiver = receiver;
        }

        private void Update()
        {
            // [-60; 60] -> [40; -40]
            float angle = _receiver.CurrentHeight * 2 / -3;
            UpdateAngle(angle);
        }
    }
}
