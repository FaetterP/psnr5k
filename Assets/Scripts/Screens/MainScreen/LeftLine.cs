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

        [SerializeField] private GameObject _circles;

        private float _arrowsValue;
        private float _widthValue;
        private LineRenderer _thisLineRenderer;
        private float _angle;

        private void Awake()
        {
            _thisLineRenderer = GetComponent<LineRenderer>();
        }

        private void OnEnable()
        {
            _handleArrows.AddListener(SetArrowsValue);
            _handleWidth.AddListener(SetWidthValue);
            _receiver.AddListener(UpdateAngle);

            _widthValue = _handleWidth.CurrentValue;
            _arrowsValue = _handleArrows.CurrentValue;
        }

        private void OnDisable()
        {
            _handleArrows.RemoveListener(SetArrowsValue);
            _handleWidth.RemoveListener(SetWidthValue);
            _receiver.RemoveListener(UpdateAngle);
        }

        private void SetArrowsValue(float value)
        {
            _arrowsValue = value;
            ApplyReceiverAngle();
        }

        private void SetWidthValue(float value)
        {
            _widthValue = value;
            ApplyReceiverAngle();
        }

        private void UpdateAngle(float value)
        {
            _angle = value;
            ApplyReceiverAngle();
        }

        private void ApplyReceiverAngle()
        {
            float offset = _angle * _widthValue * 3 / 1000 - 3f / 10;
            offset += _arrowsValue * 0.2f;
            Debug.Log($"{_widthValue} {_arrowsValue} {offset}");
            _thisLineRenderer.SetPosition(0, new Vector3(0, offset, -1));
            _thisLineRenderer.SetPosition(1, new Vector3(0, offset, 1));

            _circles.transform.localPosition = new Vector3(0, offset, 0);
        }
    }
}
