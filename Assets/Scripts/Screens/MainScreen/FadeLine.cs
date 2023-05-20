using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    [RequireComponent(typeof(LineRenderer))]
    class FadeLine : MonoBehaviour
    {
        [SerializeField] private HandleRotate _handle;
        [SerializeField] [ColorUsage(true, true)] private Color _min;
        [SerializeField] [ColorUsage(true, true)] private Color _max;

        private LineRenderer _thisLineRenderer;

        private void Awake()
        {
            _thisLineRenderer = GetComponent<LineRenderer>();
        }

        private void OnEnable()
        {
            _handle.AddListener(ChangeColor);
        }

        private void OnDisable()
        {
            _handle.RemoveListener(ChangeColor);
        }

        private void ChangeColor(float value)
        {
            Color color = Color.Lerp(_min, _max, Mathf.Pow(value / 100f,2));
            _thisLineRenderer.material.color = color;
            _thisLineRenderer.material.SetColor("_EmissionColor", color);
        }
    }
}
