using Assets.Scripts.Switches;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    class RangeText : MonoBehaviour
    {
        [SerializeField] private HandleRotate _range;
        [SerializeField] private TextMeshPro _staticPart;
        [SerializeField] private TextMeshPro _movedPartUp;
        [SerializeField] private TextMeshPro _movedPartDown;
        [SerializeField] private float _height;

        private void OnEnable()
        {
            _range.AddListener(RangeChangedHandler);
        }

        private void OnDisable()
        {
            _range.RemoveListener(RangeChangedHandler);
        }

        private void RangeChangedHandler(float value)
        {
            _staticPart.text = ((int)value / 100).ToString();
            if (_staticPart.text.Length == 1)
                _staticPart.text = "0" + _staticPart.text;

            int decimalPart = (int)value % 100;
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
