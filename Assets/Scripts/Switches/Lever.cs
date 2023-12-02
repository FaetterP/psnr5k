using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Utilities;
using UnityEngine.Events;

namespace Assets.Scripts.Switches
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(HighlightedObject))]
    class Lever : MonoBehaviour
    {
        [SerializeField] protected Transform _center;
        [SerializeField] protected Vector3 _enableAngle;
        [SerializeField] protected Vector3 _disableAngle;
        [SerializeField] protected AudioClip _audioClick;
        [SerializeField] private bool _isEnabledAtStart = false;

        protected HighlightedObject _thisHighlightedObject;
        protected AudioSource _thisAudioSource;

        protected UnityEvent e_onValueChanged = new UnityEvent();
        protected Dictionary<bool, Vector3> _angles = new Dictionary<bool, Vector3>();
        protected bool _isPressed;
        public bool Value => _isPressed;

        private void Awake()
        {
            _thisAudioSource = GetComponent<AudioSource>();
            _thisHighlightedObject = GetComponent<HighlightedObject>();
            _angles.Add(false, _disableAngle);
            _angles.Add(true, _enableAngle);
            _isPressed = _isEnabledAtStart;
        }

        private void Start()
        {
            _center.transform.localEulerAngles = _angles[_isPressed];
            e_onValueChanged.Invoke();
        }

        private void OnMouseDown()
        {
            if (_thisHighlightedObject.IsActive)
            {
                _isPressed = !_isPressed;
                _center.transform.localEulerAngles = _angles[_isPressed];
                _thisAudioSource.PlayOneShot(_audioClick);

                e_onValueChanged.Invoke();
            }
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
