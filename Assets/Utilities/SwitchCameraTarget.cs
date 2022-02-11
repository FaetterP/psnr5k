using UnityEngine;

namespace Assets.Utilities
{
    class SwitchCameraTarget : MonoBehaviour
    {
        [SerializeField] private Transform[] _targets;
        [SerializeField] private Transform _anchor;

        public void ChangeTarget(int index)
        {
            _anchor.position = _targets[index].position;
        }

        public int GetCount()
        {
            return _targets.Length;
        }
    }
}
