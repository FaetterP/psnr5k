using Assets.Utilities;
using UnityEngine;

namespace Assets.UI.Research
{
    class ButtonChangeCameraTarget : MonoBehaviour
    {
        [SerializeField] private SwitchCameraTarget _switch;
        [SerializeField] private int _index;

        private void OnMouseDown()
        {
            _switch.ChangeTarget(_index);
        }
    }
}
