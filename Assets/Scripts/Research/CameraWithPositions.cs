using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Research
{
    [RequireComponent(typeof(Camera))]
    public class CameraWithPositions : MonoBehaviour
    {
        [SerializeField] private Transform _bigItemsPoint;
        [SerializeField] private Transform _tubeItemsPoint;
        [SerializeField] private Transform _showBigItemsPoint;

        private Stack<Transform> _stack = new Stack<Transform>();
        private static CameraWithPositions s_instance;

        public static CameraWithPositions Instance => s_instance;

        public bool GoBack() // TODO: decompose
        {
            if (_stack.Count > 0)
            {
                GoToPoint(_stack.Pop());
                return true;
            }

            return false;
        }

        private void Awake()
        {
            s_instance = this;
        }

        private void Start()
        {
            GoToPoint(_bigItemsPoint);
        }

        private void OnEnable() // TODO: remove FindObjectByType
        {
            foreach (BigObject obj in FindObjectsOfType<BigObject>())
            {
                obj.AddListenerOnClicked(GoToShowBigObject);
            }
            FindObjectOfType<Tube>().AddListenerOnClicked(GoToTube);
        }

        private void OnDisable() // TODO: remove FindObjectByType
        {
            foreach (BigObject obj in FindObjectsOfType<BigObject>())
            {
                obj.RemoveListenerOnClicked(GoToShowBigObject);
            }
            FindObjectOfType<Tube>().RemoveListenerOnClicked(GoToTube);
        }

        private void GoToShowBigObject()
        {
            _stack.Push(_bigItemsPoint);
            GoToPoint(_showBigItemsPoint);
        }

        private void GoToTube()
        {
            _stack.Push(_bigItemsPoint);
            GoToPoint(_tubeItemsPoint);
        }

        private void GoToPoint(Transform point)
        {
            transform.SetPositionAndRotation(point.position, point.rotation);
        }
    }
}
