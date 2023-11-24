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
        [SerializeField] private Transform _center;
        [SerializeField] private AzimuthClip _azimuthClip;
        [SerializeField] private Vector3[] _offsets;
        [SerializeField] private KeyCode _keyPullUp;
        [SerializeField] private KeyCode _keyPullDown;
        [SerializeField] private AudioClip _audioPull;
        [SerializeField] private HandleRotate _thisHandleRotate;
        private AudioSource _thisAudioSource;
        private HighlightedObject _thisHighlightedObject;
        private Vector3 _startPosition;
        private int _status;

        private UnityEvent e_onValueChanged = new UnityEvent();

        public HandleRotate HandleRotate => _thisHandleRotate;
        public int Status => _status;

        private void Awake()
        {
            _thisHandleRotate = GetComponent<HandleRotate>();
            _thisAudioSource = GetComponent<AudioSource>();
            _thisHighlightedObject = GetComponent<HighlightedObject>();
            _startPosition = _center.transform.localPosition;
            _status = 0;
        }

        private void Start()
        {
            e_onValueChanged.Invoke();
        }

        private void Update()
        {
            if (_thisHighlightedObject.IsActive)
            {
                if (Input.GetKey(_keyPullUp))
                    Move(2);
                else if (Input.GetKey(_keyPullDown))
                {
                    if (_status == 2)
                        Move(1);
                    else if (_azimuthClip.Value)
                        Move(0);
                }
            }
        }

        private void Move(int status)
        {
            _status = Math.Min(Math.Max(status, 0), _offsets.Length - 1);

            _azimuthClip.Disclip();
            _thisAudioSource.PlayOneShot(_audioPull);

            _center.transform.localPosition = _startPosition + _offsets[_status];

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
