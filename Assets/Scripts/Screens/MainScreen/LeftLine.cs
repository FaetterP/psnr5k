using Assets.Scripts.Block;
using Assets.Scripts.Switches;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    [RequireComponent(typeof(LineRenderer))]
    class LeftLine : MonoBehaviour
    {
        [SerializeField] private HandleRotate _width;
        [SerializeField] private HandleRotate _arrows;
        [SerializeField] private Receiver _receiver;

        [SerializeField] private Vector3 _leftArrows;
        [SerializeField] private Vector3 _rightArrows;
        [SerializeField] private Vector3 _leftWidth;
        [SerializeField] private Vector3 _rightWidth;

        [SerializeField] private GameObject _circles;

        [SerializeField] private Block.Block _block;
        [SerializeField] private AnimationCurve _lightActivationCurve;
        [SerializeField][ColorUsage(true, true)] private Color _minColor;

        private LineRenderer _thisLineRenderer;
        private float _angle;
        private Color _maxColor;

        private void Awake()
        {
            _thisLineRenderer = GetComponent<LineRenderer>();
            _circles.gameObject.SetActive(false);

            _maxColor = _thisLineRenderer.material.GetColor(Constants.EmissionColor);
            _thisLineRenderer.enabled = false;
        }

        private void OnEnable()
        {
            _arrows.AddListener(ApplyReceiverAngle);
            _width.AddListener(ApplyReceiverAngle);
            _receiver.AddListener(ReceiverAngleChanged);
            _block.AddListenerLight(BlockIntensityHandler);
            _block.AddListenerLaunchEnd(EnableLine);
        }

        private void OnDisable()
        {
            _arrows.RemoveListener(ApplyReceiverAngle);
            _width.RemoveListener(ApplyReceiverAngle);
            _receiver.RemoveListener(ReceiverAngleChanged);
            _block.RemoveListenerLight(BlockIntensityHandler);
            _block.RemoveListenerLaunchEnd(EnableLine);
        }

        private void EnableLine()
        {
            _circles.gameObject.SetActive(true);
            _thisLineRenderer.enabled = true;
            _thisLineRenderer.material.color = _minColor;
        }

        private void BlockIntensityHandler(float value)
        {
            Color color = Color.Lerp(_minColor, _maxColor, _lightActivationCurve.Evaluate(value));
            _thisLineRenderer.material.color = color;
            _thisLineRenderer.material.SetColor(Constants.EmissionColor, color);
        }

        private void ReceiverAngleChanged(float value)
        {
            _angle = value;
            ApplyReceiverAngle();
        }

        private void ApplyReceiverAngle()
        {
            float offset = _angle * _width.Value * 3 / 1000 - 3f / 10;
            offset += _arrows.Value * 0.2f;
            offset = Mathf.Clamp(offset, -1.1f, 1.1f);

            _thisLineRenderer.SetPosition(0, new Vector3(0, offset, -1));
            _thisLineRenderer.SetPosition(1, new Vector3(0, offset, 1));

            _circles.transform.localPosition = new Vector3(0, offset, 0);
        }
    }
}
