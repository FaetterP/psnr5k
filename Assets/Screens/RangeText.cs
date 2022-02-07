using Assets.Switches;
using TMPro;
using UnityEngine;

namespace Assets.Screens
{
    class RangeText : MonoBehaviour
    {
        [SerializeField] private HandleRotate _handle;
        [SerializeField] private TextMeshPro _staticPart;
        [SerializeField] private TextMeshPro _movedPartUp;
        [SerializeField] private TextMeshPro _movedPartDown;
        [SerializeField] private float _height;

        private void OnEnable()
        {
            _handle.AddListener(ChangeValues);
        }

        private void OnDisable()
        {
            _handle.RemoveListener(ChangeValues);
        }

        private void ChangeValues(int value)
        {
            _staticPart.text = (value / 100).ToString();
            if (_staticPart.text.Length == 1)
                _staticPart.text = "0" + _staticPart.text;

            int decimalPart = value % 100;
            if (decimalPart == 0)
            {
                _movedPartDown.text = "";
                _movedPartUp.text = "00";

                Vector3 position = _movedPartUp.transform.position;
                position.y = _staticPart.transform.position.y;
                _movedPartUp.transform.position = position;
            }
            else
            {
                int down = (decimalPart / 10) * 10;
                int up = ((decimalPart / 10) + 1) * 10;

                if (down == 0)
                    _movedPartDown.text = "00";
                else
                    _movedPartDown.text = down.ToString();

                if (up == 100)
                    _movedPartUp.text = "00";
                else
                    _movedPartUp.text = up.ToString();

                Vector3 position = _movedPartUp.transform.position;
                position.y = _staticPart.transform.position.y + _height * (up - decimalPart);
                _movedPartUp.transform.position = position;

                position = _movedPartDown.transform.position;
                position.y = _staticPart.transform.position.y - _height * (decimalPart - down);
                _movedPartDown.transform.position = position;
            }
        }
    }
}
