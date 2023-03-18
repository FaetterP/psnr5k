using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    [RequireComponent(typeof(LineRenderer))]
    class FocusLine : MonoBehaviour
    {
        [SerializeField] private HandleRotate _focusHandle;
        private LineRenderer _thisLineRenderer;
        private float _maxSize;

        private void Awake()
        {
            _thisLineRenderer = GetComponent<LineRenderer>();
            _maxSize = _thisLineRenderer.startWidth * 2;
        }

        private void OnEnable()
        {
            _focusHandle.AddListener(ChangeFocusValue);
        }

        private void OnDisable()
        {
            _focusHandle.RemoveListener(ChangeFocusValue);
        }

        private void ChangeFocusValue(float value)
        {
            float width = Mathf.Lerp(0, _maxSize, value / 100);
            _thisLineRenderer.startWidth = width;
            _thisLineRenderer.endWidth = width;
        }
    }
}
