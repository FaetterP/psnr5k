using Assets.Utilities;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Switches
{
    [RequireComponent(typeof(HighlightedObject))]
    [RequireComponent(typeof(AudioSource))]
    class HandleRotate : MonoBehaviour
    {
        [SerializeField] private Transform _center;
        [SerializeField] private Vector3 _rotationSpeed;
        [SerializeField] private int _min;
        [SerializeField] private int _max;
        [SerializeField] private int _step;
        [SerializeField] private int _startValue;
        [SerializeField] private AudioClip _audioRotate;
        [SerializeField] private AudioClip _audioStuck;

        private HighlightedObject _thisHighlightedObject;
        private AudioSource _thisAudioSource;

        private int _currentValue;
        private EventInt e_onValueChanged = new EventInt();

        private void Awake()
        {
            _thisHighlightedObject = GetComponent<HighlightedObject>();
            _thisAudioSource = GetComponent<AudioSource>();
            _currentValue = _startValue;
        }

        private void Update()
        {
            if (_thisHighlightedObject.IsActive == false)
                return;

            float mw = Input.GetAxis("Mouse ScrollWheel");
            if (mw == 0)
                return;

            int sign = Math.Sign(mw);
            if (_currentValue + sign * _step < _min || _currentValue + sign * _step > _max)
            {
                _thisAudioSource.PlayOneShot(_audioStuck);
                return;
            }

            _center.transform.Rotate(_rotationSpeed * sign);
            _currentValue += sign * _step;
            _thisAudioSource.PlayOneShot(_audioRotate);
            e_onValueChanged.Invoke(_currentValue);
        }

        public void AddListener(UnityAction<int> action)
        {
            e_onValueChanged.AddListener(action);
        }

        public void RemoveListener(UnityAction<int> action)
        {
            e_onValueChanged.RemoveListener(action);
        }
    }
}
