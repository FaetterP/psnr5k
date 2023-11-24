using Assets.Scripts.Block;
using Assets.Scripts.Screens.MainScreen.Bulges;
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
        [SerializeField] private HandleRotate _YPCh;
        [SerializeField] private Azimuth _azimuth;
        [SerializeField] private HandleRotate _videoA;
        [SerializeField] private Receiver _receiver;
        [SerializeField] private Block.Block _block;
        [SerializeField] private AnimationCurve _lightActivationCurve;
        [SerializeField] private Lever _SDC;
        [SerializeField] private Transform _leftLine;
        [SerializeField] private BulgeScriptableObject[] _bulgesRaw;
        private LineRenderer _thisLineRenderer;
        private float _noiseAmplitude;
        private Vector3 _downPoint;
        private Vector3 _upPoint;
        private Vector3[] _baseLayer;
        private Vector3[] _bulgeLayer;
        private Vector3[] _noiseLayer;
        [ColorUsage(true, true)] private Color _maxColor;
        [SerializeField][ColorUsage(true, true)] private Color _minColor;
        private NoiseStrategy _noiseStrategy = new StateNoise();
        private Bulge[] _bulges;

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

            _bulges = new Bulge[3];
        }

        private void Start()
        {
            ResetPoints();

            _bulges = new Bulge[_bulgesRaw.Length];

            for (int i = 0; i < _bulges.Length; i++)
            {
                float size = _bulgesRaw[i].Width;
                float range = _bulgesRaw[i].Range;
                float maxAmplitude = _bulgesRaw[i].MaxAmplitude;
                float azimuth = _bulgesRaw[i].Azimuth;
                AudioClip audioClip = _bulgesRaw[i].AudioClip;

                switch (_bulgesRaw[i].BulgeType)
                {
                    case BulgeType.Sin:
                        _bulges[i] = new BulgeSin(size, maxAmplitude, range, azimuth, _leftLine, audioClip, gameObject.AddComponent<AudioSource>());
                        break;
                    case BulgeType.Triangle:
                        _bulges[i] = new BulgeTriangle(size, maxAmplitude, range, azimuth, _leftLine, audioClip, gameObject.AddComponent<AudioSource>());
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

            EnableNoise();
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

        private void EnableLine()
        {
            _thisLineRenderer.enabled = true;
            _thisLineRenderer.material.color = _minColor;
        }

        private void IntensityChangedHandler(float value)
        {
            Color color = Color.Lerp(_minColor, _maxColor, _lightActivationCurve.Evaluate(value));
            _thisLineRenderer.material.color = color;
            _thisLineRenderer.material.SetColor("_EmissionColor", color);
        }

        private void ReceiverAngleChangedHandler(float value)
        {
            ResetBulge();
            AddBulge();
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
            // _noiseStrategy = _SDC.Value?new SDCNoise():new StateNoise();
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
                _noiseStrategy.generateNoise(_noiseLayer, _noiseAmplitude);
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

        private void AddBulge()
        {
            foreach (Bulge bulge in _bulges)
            {
                float amplitudeMultiplier = Mathf.Max(0, -Mathf.Abs(_azimuth.HandleRotate.Value - bulge.Azimuth) * 10 + bulge.Azimuth) / 100;
                float amplitude = 0.1f * amplitudeMultiplier;

                if (amplitude == 0)
                {
                    bulge.SetActive(false, 0, 0);
                    continue;
                }

                int countPointsInBulge = (int)(_countNodes * bulge.Width / (_upPoint.z - _downPoint.z));
                float[] bulgePoints = new float[countPointsInBulge];

                bulge.GenerateBulge(bulgePoints);

                float offset = bulge.Range / 2500 - 1 - 2 * Mathf.Floor(bulge.Range / 5000);
                float scale = (_upPoint.z + offset - bulge.Width / 2) / (_upPoint.z - _downPoint.z);
                int indexStart = (int)(_countNodes * scale) + 1;
                float multiplier = _videoA.Value * amplitude * 2 * bulge.MaxAmplitude;

                for (int i = 0; i < countPointsInBulge; i++)
                {
                    if (i + indexStart >= _countNodes || i + indexStart < 0)
                    {
                        continue;
                    }

                    float value = bulgePoints[i] * multiplier;
                    _bulgeLayer[i + indexStart] = new Vector3(0, value, 0);
                }
                bulge.SetActive(true, offset, multiplier);
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

        private void UpdateNoise()
        {
            _noiseAmplitude = _YPCh.Value / 8 * _videoA.Value / 100 * _amplitudeMax / 2;
        }
    }
}
