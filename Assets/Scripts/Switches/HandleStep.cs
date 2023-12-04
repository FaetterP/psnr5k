using Assets.Scripts.Utilities;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Switches
{
    [RequireComponent(typeof(HighlightedObject))]
    [RequireComponent(typeof(AudioSource))]
    class HandleStep : MonoBehaviour
    {
        [SerializeField] private Transform _center;
        [SerializeField] private Vector3[] _angles;
        [SerializeField] private AudioClip _audioRotate;
        [SerializeField] private AudioClip _audioStuck;
        [SerializeField] private int _startIndex;
        [Header("Debug")]
        [SerializeField] private int ViewValue;

        private HighlightedObject _thisHighlightedObject;
        private AudioSource _thisAudioSource;
        private int _index;
        private UnityEvent e_onValueChanged = new UnityEvent();

        public int Value => _index;

        private void Awake()
        {
            _thisHighlightedObject = GetComponent<HighlightedObject>();
            _thisAudioSource = GetComponent<AudioSource>();
            _index = _startIndex;
            ViewValue = _index;
        }

        private void Start()
        {
            _center.transform.localEulerAngles = _angles[_startIndex];
        }

        private void Update()
        {
            if (_thisHighlightedObject.IsActive == false)
                return;

            float mw = Input.GetAxis(Constants.MouseScrollWheel);
            if (mw == 0)
                return;

            int sign = Math.Sign(mw);
            if (_index + sign < 0 || _index + sign > _angles.Length - 1)
            {
                _thisAudioSource.PlayOneShot(_audioStuck);
                return;
            }

            _index += sign;
            _index = Math.Min(Math.Max(_index, 0), _angles.Length - 1);
            ViewValue = _index;

            _center.transform.localEulerAngles = _angles[_index];

            e_onValueChanged.Invoke();
            _thisAudioSource.PlayOneShot(_audioRotate);

        }

        public void AddListener(UnityAction action)
        {
            e_onValueChanged.AddListener(action);
        }

        public void RemoveListener(UnityAction action)
        {
            e_onValueChanged.RemoveListener(action);
        }
    }
}
