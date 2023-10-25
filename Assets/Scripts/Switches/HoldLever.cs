using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Switches
{
    [RequireComponent(typeof(HighlightedObject))]
    class HoldLever : MonoBehaviour
    {
        [SerializeField] private Transform _center;
        [SerializeField] private Vector3 _angleUp;
        [SerializeField] private Vector3 _angleDown;
        [SerializeField] private Vector3 _angleDefault;

        private HighlightedObject _thisHighlightedObject;
        private int _status;
        private UnityEvent<int> e_onValueChanged = new UnityEvent<int>();

        public int Value => _status;

        private void Awake()
        {
            _thisHighlightedObject = GetComponent<HighlightedObject>();
            _status = 0;
        }

        private void Update()
        {
            if (_thisHighlightedObject.IsActive)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    _center.transform.localEulerAngles = _angleUp;
                    _status = 1;
                }
                else if (Input.GetKey(KeyCode.Mouse1))
                {
                    _center.transform.localEulerAngles = _angleDown;
                    _status = -1;
                }
                else
                {
                    _center.transform.localEulerAngles = _angleDefault;
                    _status = 0;
                }
                e_onValueChanged.Invoke(_status);
            }
        }

        public void AddListener(UnityAction<int> action)
        {
            e_onValueChanged.AddListener(action);
        }

        public void RemoveListener(UnityAction<int> action)
        {
            e_onValueChanged.RemoveListener(action);
        }
    }
}
