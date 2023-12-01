using Assets.Scripts.Switches;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    [RequireComponent(typeof(MeshRenderer))]
    class FadeObject : MonoBehaviour
    {
        [SerializeField] private HandleRotate _brightness;
        [SerializeField][ColorUsage(true, true)] private Color _min;
        [SerializeField][ColorUsage(true, true)] private Color _max;

        private MeshRenderer _thisMeshRenderer;

        private void Awake()
        {
            _thisMeshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnEnable()
        {
            _brightness.AddListener(BrightnessChangedHandler);
            BrightnessChangedHandler();
        }

        private void OnDisable()
        {
            _brightness.RemoveListener(BrightnessChangedHandler);
        }

        private void BrightnessChangedHandler()
        {
            Color color = Color.Lerp(_min, _max, Mathf.Pow(_brightness.Value, 3));
            _thisMeshRenderer.material.color = color;
            _thisMeshRenderer.material.SetColor(Constants.EmissionColor, color);
        }
    }
}
