using Assets.Scripts.Block;
using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    [RequireComponent(typeof(LineRenderer))]
    class LeftLine : MonoBehaviour
    {
        [SerializeField] private HandleRotate _handleWidth;
        [SerializeField] private HandleRotate _handleArrows;
        [SerializeField] private Receiver _receiver;

        [SerializeField] private Vector3 _leftArrows;
        [SerializeField] private Vector3 _rightArows;
        [SerializeField] private Vector3 _leftWidth;
        [SerializeField] private Vector3 _rightWidth;

        private int _arrowsValue;
        private int _widthValue;
        private LineRenderer _thisLineRenderer;

        private void Awake()
        {
            _thisLineRenderer = GetComponent<LineRenderer>();
        }

        private void OnEnable()
        {
            _handleArrows.AddListener(SetArrowsValue);
            _handleWidth.AddListener(SetWidthValue);
            _receiver.AddListener(ApplyReceiverAngle);
        }

        private void OnDisable()
        {
            _handleArrows.RemoveListener(SetArrowsValue);
            _handleWidth.RemoveListener(SetWidthValue);
            _receiver.RemoveListener(ApplyReceiverAngle);
        }

        private void SetArrowsValue(int value)
        {
            _arrowsValue = value;
            UpdatePosition();
        }

        private void SetWidthValue(int value)
        {
            _widthValue = value;
            UpdatePosition();
        }

        private void ApplyReceiverAngle(float value)
        {
            float offset = value * 3 / 1000 - 3f / 10;

            _thisLineRenderer.SetPosition(0, new Vector3(0, offset, -1));
            _thisLineRenderer.SetPosition(1, new Vector3(0, offset, 1));
        }


        private void UpdatePosition()
        {
            transform.localPosition = Vector3.Lerp(_leftArrows, _rightArows, _arrowsValue / 100f);
            transform.localPosition += Vector3.Lerp(_leftWidth, _rightWidth, _widthValue / 40f);
        }
    }
}
