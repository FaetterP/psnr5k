using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.Research
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
