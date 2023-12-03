using Assets.Scripts.Switches;
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
        [SerializeField] private bool _isLaunchedAtStart = false;

        private UnityEvent e_onLaunchEnd = new UnityEvent();
        private UnityEvent<float> e_changingLightIntensity = new UnityEvent<float>();

        private Coroutine _turningOnCoroutine;
        private bool _isLaunched = false;

        public bool IsLaunched => _isLaunched;

        private void Awake()
        {
            if (_isLaunchedAtStart)
            {
                e_onLaunchEnd.Invoke();
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

        private void OnEnable()
        {
            _work.AddListener(WorkChangedHandler);
        }

        private void OnDisable()
        {
            _work.RemoveListener(WorkChangedHandler);
        }

        private IEnumerator StartLaunchingCoroutine()
        {
            yield return new WaitForSeconds(_timeLaunch);

            _isLaunched = true;
            e_onLaunchEnd.Invoke();

            float timer = 0;
            while (timer < _timeLight)
            {
                e_changingLightIntensity.Invoke(timer / _timeLight);
                yield return new WaitForEndOfFrame();
                timer += Time.deltaTime;
            }
        }

        private void WorkChangedHandler()
        {
            if (_isLaunchedAtStart && _work.Value) return;

            if (_work.Value)
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
