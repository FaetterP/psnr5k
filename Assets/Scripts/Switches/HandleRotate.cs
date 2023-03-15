using Assets.Scripts.Utilities;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Switches
{
    [RequireComponent(typeof(HighlightedObject))]
    [RequireComponent(typeof(AudioSource))]
    class HandleRotate : MonoBehaviour
    {
        [SerializeField] private Transform _center;
        [SerializeField] private Vector3 _rotationSpeed;
        [SerializeField] private float _min;
        [SerializeField] private float _max;
        [SerializeField] private float _step;
        [SerializeField] private KeyCode _fastKey;
        [SerializeField] private float _stepFast;
        [SerializeField] private float _startValue;
        [SerializeField] private AudioClip _audioRotate;
        [SerializeField] private AudioClip _audioStuck;

        private HighlightedObject _thisHighlightedObject;
        private AudioSource _thisAudioSource;

        private float _currentValue = 40;
        private EventFloat e_onValueChanged = new EventFloat();

        public float CurrentValue => _currentValue;

        private void Awake()
        {
            _thisHighlightedObject = GetComponent<HighlightedObject>();
            _thisAudioSource = GetComponent<AudioSource>();
            _currentValue = _startValue;
        }

        private void Start()
        {
            e_onValueChanged.Invoke(_startValue);
        }

        private void Update()
        {
            if (_thisHighlightedObject.IsActive == false)
                return;

            float mw = Input.GetAxis("Mouse ScrollWheel");
            if (mw == 0)
                return;

            int sign = Math.Sign(mw);
            float step = Input.GetKey(_fastKey) ? _stepFast : _step;
            if (_currentValue + sign * step < _min || _currentValue + sign * step > _max)
            {
                _thisAudioSource.PlayOneShot(_audioStuck);
                return;
            }

            _center.transform.Rotate(_rotationSpeed * sign);
            _currentValue += sign * step;
            _thisAudioSource.PlayOneShot(_audioRotate);
            e_onValueChanged.Invoke(_currentValue);
        }

        public void AddListener(UnityAction<float> action)
        {
            e_onValueChanged.AddListener(action);
        }

        public void RemoveListener(UnityAction<float> action)
        {
            e_onValueChanged.RemoveListener(action);
        }
    }
}
