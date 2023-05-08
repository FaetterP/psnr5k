﻿using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Block.Control
{
    class Amperage1 : ControlStrategy
    {
        private Transform _center;
        private HandleRotate _reflectorHandle;

        public void Init(Transform center, HandleRotate reflectorHandle)
        {
            _center = center;
            _reflectorHandle = reflectorHandle;
            _reflectorHandle.AddListener(Rotate);
            Rotate(_reflectorHandle.CurrentValue);
        }

        private void OnDestroy()
        {
            _reflectorHandle.RemoveListener(Rotate);
        }

        private void Rotate(float value)
        {
            float angle = 40 - 40 * value / 100;
            _center.localEulerAngles = new Vector3(angle, 0, 0);
        }
    }
}