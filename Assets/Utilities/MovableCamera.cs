using UnityEngine;

namespace Assets.Utilities
{
    class MovableCamera : MonoBehaviour
    {
        private float _currentHorizontal = 0;
        private float _currentVertical = 0;

        private void Update()
        {
            float addedHorizontal = Input.GetAxis("Horizontal");
            float addedVertical = Input.GetAxis("Vertical");

            if (addedHorizontal == 0 && addedVertical == 0)
                return;

            _currentVertical -= addedVertical;
            _currentHorizontal -= addedHorizontal;
            transform.eulerAngles = new Vector3(_currentVertical, _currentHorizontal, 0);
        }
    }
}
