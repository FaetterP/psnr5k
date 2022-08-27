using System.Collections;
using UnityEngine;

namespace Assets.Screens.MainScreen
{
    [RequireComponent(typeof(LineRenderer))]
    class NoiseLine : MonoBehaviour
    {
        [SerializeField] private float _delay;
        [SerializeField] private int _countNodes;
        [SerializeField] private float _amplitude;
        private LineRenderer _thisLineRenderer;
        private Vector3 _downPoint;
        private Vector3 _upPoint;
        private System.Random rnd = new System.Random();

        private void Awake()
        {
            _thisLineRenderer = GetComponent<LineRenderer>();
            _downPoint = _thisLineRenderer.GetPosition(0);
            _upPoint = _thisLineRenderer.GetPosition(1);
        }

        private void OnEnable()
        {
            EnableNoise();
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
                GenerateNoise();
            }
        }

        private void GenerateNoise()
        {
            _thisLineRenderer.positionCount = _countNodes + 1;
            for (int i = 0; i <= _countNodes; i++)
            {
                Vector3 resultPoint = Vector3.Lerp(_downPoint, _upPoint, 1f * i / _countNodes);
                resultPoint.y += ((float)rnd.NextDouble() - 0.5f) * _amplitude;
                _thisLineRenderer.SetPosition(i, resultPoint);
            }
        }
    }
}
