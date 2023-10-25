using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    [RequireComponent(typeof(Renderer))]
    class TurningOnCircles : MonoBehaviour
    {
        [SerializeField] private Block.Block _block;
        [SerializeField] private AnimationCurve _lightActivationCurve;
        [ColorUsage(true, true)][SerializeField] private Color _minColor;
        [ColorUsage(true, true)][SerializeField] private Color _maxColor;
        private Renderer _thisRenderer;

        private void Awake()
        {
            _thisRenderer = GetComponent<Renderer>();
        }

        private void OnEnable()
        {
            _block.AddListenerLaunchEnd(EnableRenderer);
            _block.AddListenerLight(ChangeIntensity);
        }

        private void OnDisable()
        {
            _block.RemoveListenerLaunchEnd(EnableRenderer);
            _block.RemoveListenerLight(ChangeIntensity);
        }

        private void EnableRenderer()
        {
            _thisRenderer.enabled = true;
            _thisRenderer.material.color = _minColor;
        }

        private void ChangeIntensity(float value)
        {
            Color color = Color.Lerp(_minColor, _maxColor, _lightActivationCurve.Evaluate(value));
            _thisRenderer.material.color = color;
            _thisRenderer.material.SetColor("_EmissionColor", color);
        }
    }
}
