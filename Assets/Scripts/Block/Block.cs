using Assets.Scripts.Switches;
using Assets.Scripts.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Block
{
    class Block : MonoBehaviour
    {
        [SerializeField] private float _timeLaunch;
        [SerializeField] private float _timeLight;
        [SerializeField] private Lever _work;

        private UnityEvent e_onLaunchEnd = new UnityEvent();
        private UnityEvent<float> e_changingLightIntensity = new UnityEvent<float>();

        private Coroutine _turningOnCoroutine;

        private void OnEnable()
        {
            _work.AddListener(TurningOn);
        }

        private void OnDisable()
        {
            _work.RemoveListener(TurningOn);
        }

        private IEnumerator StartLaunchingCoroutine()
        {
            yield return new WaitForSeconds(_timeLaunch);
            e_onLaunchEnd.Invoke();
            float timer = 0;
            while (timer < _timeLight)
            {
                e_changingLightIntensity.Invoke(timer / _timeLight);
                yield return new WaitForEndOfFrame();
                timer += Time.deltaTime;
            }
        }

        public void AddListenerLaunchEnd(UnityAction action)
        {
            e_onLaunchEnd.AddListener(action);
        }

        public void RemoveListenerLaunchEnd(UnityAction action)
        {
            e_onLaunchEnd.RemoveListener(action);
        }

        public void AddListenerLight(UnityAction<float> action)
        {
            e_changingLightIntensity.AddListener(action);
        }

        public void RemoveListenerLight(UnityAction<float> action)
        {
            e_changingLightIntensity.RemoveListener(action);
        }

        private void TurningOn(bool value)
        {
            if (value)
            {
                _turningOnCoroutine = StartCoroutine(StartLaunchingCoroutine());
            }
            else
            {
                if (_turningOnCoroutine != null)
                {
                    StopCoroutine(_turningOnCoroutine);
                }
            }
        }
    }
}
