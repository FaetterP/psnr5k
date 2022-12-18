using UnityEngine;

namespace Assets.Scripts.Utilities
{
    class SwitchCameraTarget : MonoBehaviour
    {
        [SerializeField] private Transform[] _targets;
        [SerializeField] private Transform _anchor;

        private void Start()
        {
            _anchor.position = _targets[0].position;
        }

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
