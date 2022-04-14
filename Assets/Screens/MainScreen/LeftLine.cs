using Assets.Switches;
using UnityEngine;

namespace Assets.Screens.MainScreen
{
    class LeftLine : MonoBehaviour
    {
        [SerializeField] private HandleRotate _handleWidth;
        [SerializeField] private HandleRotate _handleArrows;

        [SerializeField] private Vector3 _left;
        [SerializeField] private Vector3 _right;

        private void OnEnable()
        {
            _handleArrows.AddListener(Change);
        }

        private void OnDisable()
        {
            _handleArrows.RemoveListener(Change);
        }


        private void Change(int value)
        {
            float v = value / 100f;
            transform.localPosition = Vector3.Lerp(_left, _right, v);
        }
    }
}
