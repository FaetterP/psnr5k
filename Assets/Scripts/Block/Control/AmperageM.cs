using Assets.Scripts.Switches;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Block.Control
{
    class AmperageM : ControlStrategy
    {
        private Transform _center;
        private HandleRotate _reflector;
        private Coroutine _dropCoroutine;

        public void Init(Transform center, HandleRotate reflector)
        {
            _center = center;
            _reflector = reflector;
        }

        private void Start()
        {
            _dropCoroutine = StartCoroutine(StartDrop());

        }

        private void OnDestroy()
        {
            StopCoroutine(_dropCoroutine);
        }

        IEnumerator StartDrop()
        {
            while (true)
            {
                UpdateAngle(-20);
                yield return new WaitForSeconds(10f);

                if (_reflector.CurrentValue >= 80 && _reflector.CurrentValue <= 90)
                {
                    UpdateAngle(40);
                    yield return new WaitForSeconds(2f);
                }
            }
        }
    }
}
