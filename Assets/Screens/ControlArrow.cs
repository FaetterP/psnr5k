using Assets.Switches;
using UnityEngine;

namespace Assets.Screens
{
    class ControlArrow : MonoBehaviour
    {
        [SerializeField] private HandleStep _voltageHandle;
        [SerializeField] private HandleRotate _reflectorHandle;
        [SerializeField] private Transform _center;

        private void OnEnable()
        {
            _voltageHandle.AddListener(Rotate);
        }

        private void OnDisable()
        {
            _voltageHandle.RemoveListener(Rotate);
        }

        private void Rotate(int value)
        {
            _center.localEulerAngles = new Vector3(-48,0,0) + new Vector3(-14,0,0) * value;
        }
    }
}
