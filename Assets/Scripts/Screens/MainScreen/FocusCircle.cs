using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen
{
    class FocusCircle : MonoBehaviour
    {
        [SerializeField] private HandleRotate _focus;
        private Vector3 _maxSize;

        private void Awake()
        {
            _maxSize = transform.localScale * 2;
        }

        private void OnEnable()
        {
            _focus.AddListener(FocusChangedHandler);
        }

        private void OnDisable()
        {
            _focus.RemoveListener(FocusChangedHandler);
        }

        private void FocusChangedHandler()
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, _maxSize, _focus.Value / 100);
        }
    }
}
