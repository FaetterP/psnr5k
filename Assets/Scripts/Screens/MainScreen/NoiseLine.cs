using Assets.Scripts.Block;
using Assets.Scripts.Screens.MainScreen.Splashes;
using Assets.Scripts.Screens.MainScreen.NoiseStrategies;
using Assets.Scripts.Switches;
using Assets.Scripts.Utilities;
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
        [SerializeField] private HandleRotate _YPCh;
        [SerializeField] private Azimuth _azimuth;
        [SerializeField] private HandleRotate _videoA;
        [SerializeField] private Receiver _receiver;
        [SerializeField] private Block.Block _block;
        [SerializeField] private AnimationCurve _lightActivationCurve;
        [SerializeField] private Lever _SDC;
        [SerializeField] private Transform _leftLine;
        [SerializeField] private SplashSettings[] _splashSettings;
        [SerializeField][ColorUsage(true, true)] private Color _minColor; // TODO: remove
        [ColorUsage(true, true)] private Color _maxColor; // TODO

        private LineRenderer _thisLineRenderer;
        private float _noiseAmplitude;
        private Vector3 _downPoint;
        private Vector3 _upPoint;
        private Vector3[] _baseLayer;
        private Vector3[] _splashLayer;
        private Vector3[] _noiseLayer;

        private INoiseStrategy _noiseStrategy = new StateNoise();
        private Splash[] _splashes;

        private void Awake()
        {
            _thisLineRenderer = GetComponent<LineRenderer>();
            _downPoint = _thisLineRenderer.GetPosition(0);
            _upPoint = _thisLineRenderer.GetPosition(1);

            _thisLineRenderer.positionCount = _countNodes + 1;
            _baseLayer = new Vector3[_countNodes + 1];
            _splashLayer = new Vector3[_countNodes + 1];
            _noiseLayer = new Vector3[_countNodes + 1];

            _maxColor = _thisLineRenderer.material.GetColor(Constants.EmissionColor);
            _thisLineRenderer.enabled = false;

            _splashes = new Splash[_splashSettings.Length];
        }

        private void Start()
        {
            ResetBaseLayer();

            _splashes = new Splash[_splashSettings.Length];

            for (int i = 0; i < _splashes.Length; i++)
            {
                float size = _splashSettings[i].Width;
                float range = _splashSettings[i].Range;
                float maxAmplitude = _splashSettings[i].MaxAmplitude;
                float azimuth = _splashSettings[i].Azimuth;
                AudioClip audioClip = _splashSettings[i].AudioClip;

                switch (_splashSettings[i].SplashType)
                {
                    case SplashType.Sin:
                        _splashes[i] = new SplashSin(size, maxAmplitude, range, azimuth, _leftLine, audioClip, gameObject.AddComponent<AudioSource>());
                        break;
                    case SplashType.Triangle:
                        _splashes[i] = new SplashTriangle(size, maxAmplitude, range, azimuth, _leftLine, audioClip, gameObject.AddComponent<AudioSource>());
                        break;
                }
            }
        }

        private void OnEnable()
        {
            _YPCh.AddListener(UpdateNoise);
            _receiver.AddListener(ReceiverAngleChangedHandler);
            _block.AddListenerLight(IntensityChangedHandler);
            _block.AddListenerLaunchEnd(EnableLine);
            _SDC.AddListener(SDCChangedHandler);
            _videoA.AddListener(UpdateNoise);

            StartCoroutine(NoiseCoroutine());
        }

        private void OnDisable()
        {
            _YPCh.RemoveListener(UpdateNoise);
            _receiver.RemoveListener(ReceiverAngleChangedHandler);
            _block.RemoveListenerLight(IntensityChangedHandler);
            _block.RemoveListenerLaunchEnd(EnableLine);
            _SDC.RemoveListener(SDCChangedHandler);
            _videoA.RemoveListener(UpdateNoise);
        }

        private void UpdateNoise()
        {
            _noiseAmplitude = _YPCh.Value * _videoA.Value * _amplitudeMax / 2;
        }

        private void ResetBaseLayer()
        {
            for (int i = 0; i <= _countNodes; i++)
            {
                Vector3 resultPoint = Vector3.Lerp(_downPoint, _upPoint, 1f * i / _countNodes);
                _thisLineRenderer.SetPosition(i, resultPoint);

                _baseLayer[i] = resultPoint;
            }
        }

        private void EnableLine()
        {
            _thisLineRenderer.enabled = true;
            _thisLineRenderer.material.color = _minColor;
        }

        private void IntensityChangedHandler(float value)
        {
            Color color = Color.Lerp(_minColor, _maxColor, _lightActivationCurve.Evaluate(value));
            _thisLineRenderer.material.color = color;
            _thisLineRenderer.material.SetColor(Constants.EmissionColor, color);
        }

        private void ReceiverAngleChangedHandler(float value)
        {
            ResetSplashes();
            AddSplashes();
        }

        private void SDCChangedHandler()
        {
            _noiseLayer = new Vector3[_countNodes + 1];
            if (_SDC.Value)
            {
                _noiseStrategy = new SDCNoise();
            }
            else
            {
                _noiseStrategy = new StateNoise();
            }
        }

        private IEnumerator NoiseCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_delay);
                _noiseStrategy.generateNoise(_noiseLayer, _noiseAmplitude);
                CombineLayers();
            }
        }

        private void ResetSplashes()
        {
            for (int i = 0; i <= _countNodes; i++)
            {
                _splashLayer[i] = Vector3.zero;
            }
        }

        private void AddSplashes()
        {
            // чем больше, тем точнее нужно попасть азимутом
            // возможно, нужно вынести в настройки всплеска
            // TODO: переделать
            float sensitive = 10;
            float heightMultiplier = 1f / 1000;

            foreach (Splash splash in _splashes)
            {
                float amplitude = Mathf.Max(0, splash.Azimuth - Mathf.Abs(_azimuth.HandleRotate.Value - splash.Azimuth) * sensitive) * heightMultiplier;

                if (amplitude == 0)
                {
                    splash.SetActive(false, 0, 0);
                    continue;
                }

                int countPointsInSplash = (int)(_countNodes * splash.Width / (_upPoint.z - _downPoint.z));
                float[] splashPoints = new float[countPointsInSplash];

                splash.GenerateSplash(splashPoints);

                float offset = splash.Range / 2500 - 1 - 2 * Mathf.Floor(splash.Range / 5000);
                float scale = (_upPoint.z + offset - splash.Width / 2) / (_upPoint.z - _downPoint.z);
                int indexStart = (int)(_countNodes * scale) + 1;
                float multiplier = _videoA.Value * amplitude * 2 * splash.MaxAmplitude;

                for (int i = 0; i < countPointsInSplash; i++)
                {
                    if (i + indexStart >= _countNodes || i + indexStart < 0) continue;

                    _splashLayer[i + indexStart] = new Vector3(0, splashPoints[i] * multiplier, 0);
                }
                splash.SetActive(true, offset, multiplier / 3);
            }
        }

        private void CombineLayers()
        {
            for (int i = 0; i < _thisLineRenderer.positionCount; i++)
            {
                Vector3 resultVector = _baseLayer[i] + _splashLayer[i] + _noiseLayer[i];
                _thisLineRenderer.SetPosition(i, resultVector);
            }
        }
    }
}
