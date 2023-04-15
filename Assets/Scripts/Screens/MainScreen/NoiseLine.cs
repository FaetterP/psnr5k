﻿using Assets.Scripts.Block;
using Assets.Scripts.Screens.MainScreen.NoiseStrategies;
using Assets.Scripts.Switches;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    [RequireComponent(typeof(LineRenderer))]
    class NoiseLine : MonoBehaviour
    {
        [SerializeField] private float _delay;
        [SerializeField] private int _countNodes;
        [SerializeField] private float _amplitudeMax;
        [SerializeField] private HandleRotate _handleYPCh;
        [SerializeField] private HandleRotate _handleAzimuth;
        [SerializeField] private HandleRotate _videoA;
        [SerializeField] private Receiver _receiver;
        [SerializeField] private Block.Block _block;
        [SerializeField] private AnimationCurve _lightActivationCurve;
        [SerializeField] private Lever _leverSDC;
        private LineRenderer _thisLineRenderer;
        private float _amplitude;
        private float _azimuth;
        private Vector3 _downPoint;
        private Vector3 _upPoint;
        private System.Random rnd = new System.Random();
        private Vector3[] _baseLayer;
        private Vector3[] _bulgeLayer;
        private Vector3[] _noiseLayer;
        [ColorUsage(true, true)]
        private Color _maxColor;
        [SerializeField] [ColorUsage(true, true)] private Color _minColor;
        private NoiseStrategy _noiseStrategy = new StateNoise();

        private void Awake()
        {
            _thisLineRenderer = GetComponent<LineRenderer>();
            _downPoint = _thisLineRenderer.GetPosition(0);
            _upPoint = _thisLineRenderer.GetPosition(1);

            _thisLineRenderer.positionCount = _countNodes + 1;
            _baseLayer = new Vector3[_countNodes + 1];
            _bulgeLayer = new Vector3[_countNodes + 1];
            _noiseLayer = new Vector3[_countNodes + 1];

            _maxColor = _thisLineRenderer.material.GetColor("_EmissionColor");
            _thisLineRenderer.enabled = false;
        }

        private void Start()
        {
            ResetPoints();
        }

        private void OnEnable()
        {
            _handleYPCh.AddListener(ChangeNoiseAmplitude);
            //_handleAzimuth.AddListener(ChangeAzimuth);
            _receiver.AddListener(ChangeReceiverAngle);
            _block.AddListenerLight(ChangeIntencity);
            _block.AddListenerLaunchEnd(EnableLine);
            _leverSDC.AddListener(ChangeSDC);

            EnableNoise();
        }

        private void OnDisable()
        {
            _handleYPCh.RemoveListener(ChangeNoiseAmplitude);
            //_handleAzimuth.RemoveListener(ChangeAzimuth);
            _receiver.RemoveListener(ChangeReceiverAngle);
            _block.RemoveListenerLight(ChangeIntencity);
            _block.RemoveListenerLaunchEnd(EnableLine);
            _leverSDC.RemoveListener(ChangeSDC);
        }

        private void EnableLine()
        {
            _thisLineRenderer.enabled = true;
            _thisLineRenderer.material.color = _minColor;
        }

        private void ChangeIntencity(float value)
        {
            Color color = Color.Lerp(_minColor, _maxColor, _lightActivationCurve.Evaluate(value));
            _thisLineRenderer.material.SetColor("_EmissionColor", color);
        }

        private void ChangeNoiseAmplitude(float value)
        {
            _amplitude = _amplitudeMax * value / 8;
        }

        private void ChangeAzimuth(int value)
        {
            _azimuth = value;
            float targetAzimuth = 110;
            float amplitudeMultiplier = Mathf.Max(0, -Mathf.Abs(_azimuth - targetAzimuth) * 10 + targetAzimuth) / 100;

            float yOffset = 0.0f;
            float amplitude = 0.1f * amplitudeMultiplier;
            float range = 0.2f;

            ResetBulge();
            AddBulge(yOffset, amplitude, range);
        }

        public void EnableNoise()
        {
            StartCoroutine(NoiseCoroutine());
        }

        private IEnumerator NoiseCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_delay);
                _noiseStrategy.generateNoise(_noiseLayer, _amplitude);
                CombineLayers();
            }
        }

        private void ResetPoints()
        {
            for (int i = 0; i <= _countNodes; i++)
            {
                Vector3 resultPoint = Vector3.Lerp(_downPoint, _upPoint, 1f * i / _countNodes);
                _thisLineRenderer.SetPosition(i, resultPoint);

                _baseLayer[i] = resultPoint;
            }
        }

        private void ResetBulge()
        {
            for (int i = 0; i <= _countNodes; i++)
            {
                _bulgeLayer[i] = Vector3.zero;
            }
        }

        private void AddBulge(float yOffset, float amplitude, float range)
        {
            int countPointsInBulge = (int)(_countNodes * range / (_upPoint.z - _downPoint.z));
            float h = Mathf.PI / (countPointsInBulge - 1);
            Vector3[] bulgePoints = new Vector3[countPointsInBulge];

            for (int i = 0; i < countPointsInBulge; i++)
            {
                bulgePoints[i] = new Vector3(0, -Mathf.Sin(h * i), 0);
                bulgePoints[i] *= amplitude;
            }

            float scale = (_upPoint.z - yOffset) / (_upPoint.z - _downPoint.z);
            int indexStart = (int)(_countNodes * scale);

            for (int i = 0; i < countPointsInBulge; i++)
            {
                if (i + indexStart >= _countNodes || i + indexStart < 0)
                {
                    continue;
                }

                _bulgeLayer[i + indexStart] -= bulgePoints[i];
            }
        }

        private void CombineLayers()
        {
            for (int i = 0; i < _thisLineRenderer.positionCount; i++)
            {
                Vector3 resultVector = _baseLayer[i] + _bulgeLayer[i] + _noiseLayer[i];
                _thisLineRenderer.SetPosition(i, resultVector);
            }
        }

        private void ChangeReceiverAngle(float value)
        {
            ChangeAzimuth((int)value);
        }

        private void ChangeSDC(bool value)
        {
            _noiseLayer = new Vector3[_countNodes + 1];
            if (value)
            {
                _noiseStrategy = new SDCNoise();
            }
            else
            {
                _noiseStrategy = new StateNoise();
            }
        }
    }
}
