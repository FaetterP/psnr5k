using Assets.Scripts.Switches;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Screens
{
    class AzimuthText : MonoBehaviour
    {
        [SerializeField] private HandleRotate _handle;
        [SerializeField] private TextMeshPro _staticPart;
        [SerializeField] private TextMeshPro _movedPartUp;
        [SerializeField] private TextMeshPro _movedPartDown;
        [SerializeField] private float _height;
        [SerializeField] private Vector3 _width;
        [SerializeField] private Vector3 _widthBetween;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Vector3 _offsetBetween;
        [SerializeField] private LineRenderer _lineUp;
        [SerializeField] private LineRenderer _lineDown;
        [SerializeField] private LineRenderer _lineBetween;

        private void OnEnable()
        {
            _handle.AddListener(ChangeValues);
            ChangeValues(_handle.CurrentValue);
        }

        private void OnDisable()
        {
            _handle.RemoveListener(ChangeValues);
        }

        private void ChangeValues(float value)
        {
            value = value / 200f * 81;
            if (value < 40.5)
            {
                value += 19.5f;
            }
            else
            {
                value -= 40.5f;
            }
            value *= 100;
            value = (int)value;

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

                _lineUp.SetPosition(0, _movedPartUp.transform.position + _offset);
                _lineUp.SetPosition(1, _movedPartUp.transform.position + _offset + _width);

                _lineDown.SetPosition(0, _movedPartDown.transform.position + _offset);
                _lineDown.SetPosition(1, _movedPartDown.transform.position + _offset + _width);

                Vector3 between = _movedPartDown.transform.position + _movedPartUp.transform.position;
                between /= 2;

                _lineBetween.SetPosition(0, between + _offsetBetween);
                _lineBetween.SetPosition(1, between + _offsetBetween + _widthBetween);
            }
        }
    }
}
