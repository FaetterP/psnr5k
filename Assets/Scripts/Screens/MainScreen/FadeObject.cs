using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    [RequireComponent(typeof(MeshRenderer))]
    class FadeObject : MonoBehaviour
    {
        [SerializeField] private HandleRotate _handle;
        [SerializeField] [ColorUsage(true, true)] private Color _min;
        [SerializeField] [ColorUsage(true, true)] private Color _max;

        private MeshRenderer _thisMeshRenderer;

        private void Awake()
        {
            _thisMeshRenderer = GetComponent<MeshRenderer>();
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
            _thisMeshRenderer.material.color = Color.Lerp(_min, _max, value / 100f);
            _thisMeshRenderer.material.SetColor("_EmissionColor", Color.Lerp(_min, _max, value / 100f));
        }
    }
}
