using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    class FocusCircle : MonoBehaviour
    {
        [SerializeField] private HandleRotate _focusHandle;
        private Vector3 _maxSize;

        private void Awake()
        {
            _maxSize = transform.localScale * 2;
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
            transform.localScale = Vector3.Lerp(Vector3.zero, _maxSize, value / 100);
        }
    }
}
