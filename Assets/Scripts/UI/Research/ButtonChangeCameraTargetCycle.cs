using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.UI.Research
{
    class ButtonChangeCameraTargetCycle : MonoBehaviour
    {
        [SerializeField] private SwitchCameraTarget _switch;
        [SerializeField] private int _startIndex;

        private int _index;

        private void Awake()
        {
            _index = _startIndex;
        }

        private void OnMouseDown()
        {
            _index++;
            _index %= _switch.GetCount();
            _switch.ChangeTarget(_index);
        }
    }
}
