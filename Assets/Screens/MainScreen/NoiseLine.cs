using Assets.Switches;
using System.Collections;
using UnityEngine;

namespace Assets.Screens.MainScreen
{
    [RequireComponent(typeof(LineRenderer))]
    class NoiseLine : MonoBehaviour
    {
        [SerializeField] private float _delay;
        [SerializeField] private int _countNodes;
        [SerializeField] private float _amplitudeMax;
        [SerializeField] private HandleRotate _handle;
        private LineRenderer _thisLineRenderer;
        private float _amplitude;
        private Vector3 _downPoint;
        private Vector3 _upPoint;
        private System.Random rnd = new System.Random();
        private Vector3[] _baseLayer;
        private Vector3[] _bulgeLayer;
        private Vector3[] _noiseLayer;

        private void Awake()
        {
            _thisLineRenderer = GetComponent<LineRenderer>();
            _downPoint = _thisLineRenderer.GetPosition(0);
            _upPoint = _thisLineRenderer.GetPosition(1);

            _thisLineRenderer.positionCount = _countNodes + 1;
            _baseLayer = new Vector3[_countNodes + 1];
            _bulgeLayer = new Vector3[_countNodes + 1];
            _noiseLayer = new Vector3[_countNodes + 1];
        }

        private void Start()
        {
            ResetPoints();
            AddBulge(0.000f, 0.0001f, 0.0002f);
        }

        private void OnEnable()
        {
            _handle.AddListener(ChangeValues);
            EnableNoise();
        }

        private void OnDisable()
        {
            _handle.RemoveListener(ChangeValues);
        }

        private void ChangeValues(int value)
        {
            _amplitude = _amplitudeMax * value / 8;
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
                AddNoise(_amplitude);
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
                //_bulgeLayer[i] = resultPoint;
                //_noiseLayer[i] = resultPoint;
            }
        }

        private void AddNoise(float amplitude)
        {
            for (int i = 0; i <= _countNodes; i++)
            {
                float noiseValue = (-(float)rnd.NextDouble()) * amplitude;
                Vector3 currentPoint = _noiseLayer[i];
                currentPoint.y = noiseValue;
                _noiseLayer[i] = currentPoint;
            }
        }

        private void AddBulge(float yOffset, float amplitude, float range)
        {
            int countPointsInBulge = (int)(_countNodes * range / (_upPoint.z - _downPoint.z));
            float h = Mathf.PI / (countPointsInBulge - 1);
            Vector3[] bulgePoints = new Vector3[countPointsInBulge];

            for (int i = 0; i < countPointsInBulge; i++)
            {
                bulgePoints[i] = new Vector3(0, Mathf.Sin(h * i), 0);
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
    }
}
