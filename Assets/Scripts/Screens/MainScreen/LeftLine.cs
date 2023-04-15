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

        [SerializeField] private Block.Block _block;
        [SerializeField] private AnimationCurve _lightActivationCurve;

        private float _arrowsValue;
        private float _widthValue;
        private LineRenderer _thisLineRenderer;
        private float _angle;
        [ColorUsage(true, true)]
        [SerializeField] private Color _minColor;
        private Color _maxColor;

        private void Awake()
        {
            _thisLineRenderer = GetComponent<LineRenderer>();

            _maxColor = _thisLineRenderer.material.GetColor("_EmissionColor");
            _thisLineRenderer.enabled = false;
        }

        private void OnEnable()
        {
            _handleArrows.AddListener(SetArrowsValue);
            _handleWidth.AddListener(SetWidthValue);
            _receiver.AddListener(UpdateAngle);

            _widthValue = _handleWidth.CurrentValue;
            _arrowsValue = _handleArrows.CurrentValue;

            _block.AddListenerLight(ChangeIntencity);
            _block.AddListenerLaunchEnd(EnableLine);
        }

        private void OnDisable()
        {
            _handleArrows.RemoveListener(SetArrowsValue);
            _handleWidth.RemoveListener(SetWidthValue);
            _receiver.RemoveListener(UpdateAngle);
            _block.RemoveListenerLight(ChangeIntencity);
            _block.RemoveListenerLaunchEnd(EnableLine);
        }

        private void EnableLine()
        {
            _thisLineRenderer.enabled = true;
            _thisLineRenderer.material.color = _minColor;
        }

        private void ChangeIntencity(float value)
        {
            Color color = Color.Lerp(_minColor, _maxColor, _lightActivationCurve.Evaluate(value));
            _thisLineRenderer.material.SetColor("_EmissionColor", color);
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

            offset = Mathf.Clamp(offset, -1.1f, 1.1f);
            //Debug.Log($"{_widthValue} {_arrowsValue} {offset}");
            _thisLineRenderer.SetPosition(0, new Vector3(0, offset, -1));
            _thisLineRenderer.SetPosition(1, new Vector3(0, offset, 1));

            _circles.transform.localPosition = new Vector3(0, offset, 0);
        }
    }
}
