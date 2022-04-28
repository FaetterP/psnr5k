using Assets.Switches;
using UnityEngine;

namespace Assets.Screens.MainScreen
{
    class LeftLine : MonoBehaviour
    {
        [SerializeField] private HandleRotate _handleWidth;
        [SerializeField] private HandleRotate _handleArrows;

        [SerializeField] private Vector3 _leftArrows;
        [SerializeField] private Vector3 _rightArows;
        [SerializeField] private Vector3 _leftWidth;
        [SerializeField] private Vector3 _rightWidth;

        private int _arrowsValue;
        private int _widthValue;

        private void OnEnable()
        {
            _handleArrows.AddListener(SetArrowsValue);
            _handleWidth.AddListener(SetWidthValue);
        }

        private void OnDisable()
        {
            _handleArrows.RemoveListener(SetArrowsValue);
            _handleWidth.RemoveListener(SetWidthValue);
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


        private void UpdatePosition()
        {
            Debug.Log(_widthValue/40f);
            transform.localPosition = Vector3.Lerp(_leftArrows, _rightArows, _arrowsValue / 100f);
            transform.localPosition += Vector3.Lerp(_leftWidth, _rightWidth, _widthValue / 40f);
        }
    }
}
