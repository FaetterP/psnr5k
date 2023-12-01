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
                if (Input.GetKey(_keyPullUp))
                    ChangeMode(Mode.Manual);
                else if (Input.GetKey(_keyPullDown))
                {
                    if (_status == Mode.Manual)
                        ChangeMode(Mode.Middle);
                    else if (_azimuthClip.Value)
                        ChangeMode(Mode.Sector);
                }
            }
        }

        private void ChangeMode(Mode status)
        {
            _status = status;

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
