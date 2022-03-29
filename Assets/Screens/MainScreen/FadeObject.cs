using Assets.Switches;
using UnityEngine;

namespace Assets.Screens.MainScreen
{
    [RequireComponent(typeof(MeshRenderer))]
    class FadeObject : MonoBehaviour
    {
        [SerializeField] private HandleRotate _handle;
        [SerializeField] private Color _min;
        [SerializeField] private Color _max;

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

        private void ChangeColor(int value)
        {
            _thisMeshRenderer.material.color = Color.Lerp(_min, _max, value / 100f);
        }
    }
}
