using Assets.Utilities;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Switches
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

        private HandleRotate _thisHandleRotate;
        private AudioSource _thisAudioSource;
        private HighlightedObject _thisHighlightedObject;
        private Vector3 _startPosition;
        private int _status;

        private EventInt e_onValueChanged = new EventInt();

        private void Awake()
        {
            _thisHandleRotate = GetComponent<HandleRotate>();
            _thisAudioSource = GetComponent<AudioSource>();
            _thisHighlightedObject = GetComponent<HighlightedObject>();
            _startPosition = _center.transform.localPosition;
            _status = 0;
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
                    else if (_azimuthClip.IsPressed)
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

            _thisHandleRotate.enabled = _status == 0;
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
