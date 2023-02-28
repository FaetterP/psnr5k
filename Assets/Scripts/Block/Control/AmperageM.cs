using UnityEngine;

namespace Assets.Scripts.Block.Control
{
    class AmperageM : ControlStrategy
    {
        private Transform _center;

        public void Init(Transform center)
        {
            _center = center;
        }

        private void Start()
        {
            _center.localEulerAngles = new Vector3(24, 0, 180);
        }
    }
}
