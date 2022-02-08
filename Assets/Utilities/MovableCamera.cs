using UnityEngine;

namespace Assets.Utilities
{
    class MovableCamera : MonoBehaviour
    {
        [SerializeField] private Transform _center;
        [SerializeField] private KeyCode _keyZoomIn;
        [SerializeField] private KeyCode _keyZoomOut;

        private float _currentHorizontal = 0;
        private float _currentVertical = 0;

        private void Update()
        {
            if (Input.GetKey(_keyZoomIn))
                transform.localPosition += new Vector3(0, 0, 0.1f);

            if (Input.GetKey(_keyZoomOut))
                transform.localPosition += new Vector3(0, 0, -0.1f);

            float addedHorizontal = Input.GetAxis("Horizontal");
            float addedVertical = Input.GetAxis("Vertical");

            if (addedHorizontal == 0 && addedVertical == 0)
                return;

            _currentVertical -= addedVertical;
            _currentHorizontal -= addedHorizontal;
            _center.transform.eulerAngles = new Vector3(_currentVertical, _currentHorizontal, 0);
        }
    }
}
