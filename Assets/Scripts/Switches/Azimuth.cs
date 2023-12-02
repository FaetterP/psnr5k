using Assets.Scripts.Utilities;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Switches
{
    [RequireComponent(typeof(HandleRotate))]
    [RequireComponent(typeof(HighlightedObject))]
    [RequireComponent(typeof(HandleRotate))]
    [RequireComponent(typeof(AudioSource))]
    class Azimuth : MonoBehaviour
    {
        public enum Mode
        {
            Sector = 0, Middle, Manual
        }

        [SerializeField] private Transform _center;
        [SerializeField] private AzimuthClip _azimuthClip;
        [SerializeField] private Vector3[] _offsets;
        [SerializeField] private KeyCode _keyPullUp;
        [SerializeField] private KeyCode _keyPullDown;
        [SerializeField] private AudioClip _audioPull;
        [SerializeField] private HandleRotate _thisHandleRotate;
        [SerializeField] private Mode _startMode;
        [Header("Debug")]
        [SerializeField] private Mode ViewValue;

        private AudioSource _thisAudioSource;
        private HighlightedObject _thisHighlightedObject;
        private Vector3 _startPosition;
        private Mode _status;

        private UnityEvent e_onValueChanged = new UnityEvent();

        public HandleRotate HandleRotate => _thisHandleRotate;
        public Mode Status => _status;

        private void Awake()
        {
            _thisHandleRotate = GetComponent<HandleRotate>();
            _thisAudioSource = GetComponent<AudioSource>();
            _thisHighlightedObject = GetComponent<HighlightedObject>();
            _startPosition = _center.transform.localPosition;
            _status = _startMode;
            ViewValue = _startMode;
        }

        private void Start()
        {
            ChangeMode(_startMode);
            e_onValueChanged.Invoke();
        }

        private void Update()
        {
            if (_thisHighlightedObject.IsActive)
            {
                if (Input.GetKeyDown(_keyPullUp))
                    ChangeMode(Mode.Sector);
                else if (Input.GetKeyDown(_keyPullDown))
                {
                    if (_status == Mode.Middle && _azimuthClip.Value)
                        ChangeMode(Mode.Manual);
                    else
                        ChangeMode(Mode.Middle);
                }
            }
        }

        private void ChangeMode(Mode status)
        {
            _status = status;
            ViewValue = status;

            _azimuthClip.Disclip();
            _thisAudioSource.PlayOneShot(_audioPull);

            _center.transform.localPosition = _startPosition + _offsets[(int)_status];

            e_onValueChanged.Invoke();
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
