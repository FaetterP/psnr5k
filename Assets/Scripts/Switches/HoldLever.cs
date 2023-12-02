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
        [Header("Debug")] // TODO подписаться на событие об изменении
        [SerializeField] private int ViewValue;

        private HighlightedObject _thisHighlightedObject;
        private int _status;
        private UnityEvent e_onValueChanged = new UnityEvent();

        public int Value => _status;

        private void Awake()
        {
            _thisHighlightedObject = GetComponent<HighlightedObject>();
            _status = 0;
            ViewValue = _status;
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
                ViewValue = _status;
                e_onValueChanged.Invoke();
            }
        }

        public void AddListener(UnityAction action)
        {
            e_onValueChanged.AddListener(action);
        }

        public void RemoveListener(UnityAction action)
        {
            e_onValueChanged.RemoveListener(action);
        }
    }
}
