using Assets.Scripts.Switches;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Block.Control
{
    class AmperageM : ControlStrategy
    {
        private HandleRotate _reflector;
        private Coroutine _dropCoroutine;

        public void Init(HandleRotate reflector)
        {
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

        private IEnumerator StartDrop()
        {
            while (true)
            {
                UpdateAngle(-20);
                yield return new WaitForSeconds(10f);

                if (_reflector.Value >= 80 && _reflector.Value <= 90)
                {
                    UpdateAngle(40);
                    yield return new WaitForSeconds(2f);
                }
            }
        }
    }
}
