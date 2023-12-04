using UnityEngine;

namespace Assets.Scripts.Utilities
{
    class MovableCamera : MonoBehaviour
    {
        [SerializeField] private float _speed = 1;
        [SerializeField] private float _rotateSpeed = 1;
        [SerializeField] private KeyCode _keyZoomIn;
        [SerializeField] private KeyCode _keyZoomOut;
        [SerializeField] private KeyCode _rotateLeft;
        [SerializeField] private KeyCode _rotateRight;

        private void Update()
        {
            float scaleRotate = Time.deltaTime * _rotateSpeed;
            Vector3 added = Vector3.zero;

            if (Input.GetKey(_keyZoomIn))
                added += transform.forward * _speed;
            if (Input.GetKey(_keyZoomOut))
                added += -transform.forward * _speed;

            if (Input.GetKey(_rotateRight))
                transform.Rotate(new Vector3(0, -scaleRotate, 0));
            if (Input.GetKey(_rotateLeft))
                transform.Rotate(new Vector3(0, scaleRotate, 0));

            float horizontal = Input.GetAxis(Constants.Horizontal);
            float vertical = Input.GetAxis(Constants.Vertical);
            added += transform.right * horizontal * _speed;
            added += transform.up * vertical * _speed;

            transform.position += added * Time.deltaTime;
        }
    }
}
