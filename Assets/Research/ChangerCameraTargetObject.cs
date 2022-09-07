using Assets.Utilities;
using UnityEngine;

namespace Assets.Research
{
    class ChangerCameraTargetObject : MonoBehaviour
    {
        [SerializeField] private SwitchCameraTarget _camera;
        [SerializeField] private int _targetNumber;

        private void OnMouseDown()
        {
            _camera.ChangeTarget(_targetNumber);
        }
    }
}
