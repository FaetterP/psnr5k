using Assets.Scripts.Switches;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    [RequireComponent(typeof(LineRenderer))]
    class FadeLine : MonoBehaviour
    {
        [SerializeField] private HandleRotate _brightness;
        [SerializeField][ColorUsage(true, true)] private Color _min;
        [SerializeField][ColorUsage(true, true)] private Color _max;

        private LineRenderer _thisLineRenderer;

        private void Awake()
        {
            _thisLineRenderer = GetComponent<LineRenderer>();
        }

        private void OnEnable()
        {
            _brightness.AddListener(BrightnessChangedHandler);
        }

        private void OnDisable()
        {
            _brightness.RemoveListener(BrightnessChangedHandler);
        }

        private void BrightnessChangedHandler()
        {
            Color color = Color.Lerp(_min, _max, Mathf.Pow(_brightness.Value, 3));
            _thisLineRenderer.material.color = color;
            _thisLineRenderer.material.SetColor(Constants.EmissionColor, color);
        }
    }
}
