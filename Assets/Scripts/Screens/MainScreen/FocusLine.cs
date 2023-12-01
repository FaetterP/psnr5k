using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    [RequireComponent(typeof(LineRenderer))]
    class FocusLine : MonoBehaviour
    {
        [SerializeField] private HandleRotate _focus;
        private LineRenderer _thisLineRenderer;
        private float _maxSize;

        private void Awake()
        {
            _thisLineRenderer = GetComponent<LineRenderer>();
            _maxSize = _thisLineRenderer.startWidth + 0.008f * 20;
        }

        private void OnEnable()
        {
            _focus.AddListener(ChangeFocusValue);
        }

        private void OnDisable()
        {
            _focus.RemoveListener(ChangeFocusValue);
        }

        private void ChangeFocusValue()
        {
            float width = Mathf.Lerp(0, _maxSize, Mathf.Abs(_focus.Value * 2 - 1) + 0.1f);
            _thisLineRenderer.startWidth = width;
            _thisLineRenderer.endWidth = width;
        }
    }
}
