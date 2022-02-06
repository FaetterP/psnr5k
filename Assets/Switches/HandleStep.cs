﻿using Assets.Utilities;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Switches
{
    [RequireComponent(typeof(HighlightedObject))]
    [RequireComponent(typeof(AudioSource))]
    class HandleStep : MonoBehaviour
    {
        [SerializeField] private Vector3[] _angles;
        [SerializeField] private AudioClip _audioRotate;
        [SerializeField] private AudioClip _audioStuck;
        [SerializeField] private int _startIndex;

        private HighlightedObject _thisHighlightedObject;
        private AudioSource _thisAudioSource;
        private int _index;
        private EventInt e_onValueChanged = new EventInt();

        private void Awake()
        {
            _thisHighlightedObject = GetComponent<HighlightedObject>();
            _thisAudioSource = GetComponent<AudioSource>();
            _index = _startIndex;
        }

        private void Start()
        {
            transform.eulerAngles = _angles[_startIndex];
        }

        private void Update()
        {
            if (_thisHighlightedObject.IsActive == false)
                return;

            float mw = Input.GetAxis("Mouse ScrollWheel");
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
            transform.eulerAngles = _angles[_index];

            e_onValueChanged.Invoke(_index);
            _thisAudioSource.PlayOneShot(_audioRotate);

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
