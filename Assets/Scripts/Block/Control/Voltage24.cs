using UnityEngine;

namespace Assets.Scripts.Block.Control
{
    class Voltage24 : ControlStrategy
    {
        private Transform _center;

        public void Init(Transform center)
        {
            _center = center;
        }

        private void Start()
        {
            UpdateAngle(-15);
        }
    }
}
