using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    [RequireComponent(typeof(Renderer))]
    class TurningOnCircles : MonoBehaviour
    {
        [SerializeField] private Block.Block _block;
        [SerializeField] private AnimationCurve _lightActivationCurve;
        private Renderer _thisRenderer;
        [ColorUsage(true, true)]
        private Color _maxColor;

        private void Awake()
        {
            _thisRenderer = GetComponent<Renderer>();
            _thisRenderer.enabled = false;
        }

        private void OnEnable()
        {
            _block.AddListenerLaunchEnd(EnableRenderer);
            _block.AddListenerLight(ChangeIntencity);
        }

        private void OnDisable()
        {
            _block.RemoveListenerLaunchEnd(EnableRenderer);
            _block.RemoveListenerLight(ChangeIntencity);
        }

        private void EnableRenderer()
        {
            _thisRenderer.enabled = true;
        }

        private void ChangeIntencity(float value)
        {
            Color color = Color.Lerp(Color.clear, _maxColor, _lightActivationCurve.Evaluate(value));
            _thisRenderer.material.SetColor("_EmissionColor", color);
        }
    }
}
