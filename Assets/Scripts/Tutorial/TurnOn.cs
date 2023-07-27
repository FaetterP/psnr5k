using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Tutorial
{
    class TurnOn : MonoBehaviour
    {
        [SerializeField] private DisplayTutorial _display;
        [SerializeField] private Lever _work;
        [SerializeField] private HandleStep _control;
        [SerializeField] private HandleRotate _reflector;
        [SerializeField] private HandleRotateLimitation _bisector;
        [SerializeField] private HandleStep _delay;

        private int _step;
        private string[] _messages = new string[] { "Работа вверх", "контроль 6", "контроль ток 1" };

        private bool _workValue;
        private int _controlValue;
        private float _reflectorValue;
        private int _bisectorValue;
        private int _delayValue;

        private void Start()
        {
            _display.ShowMessage(_messages[0]);
        }

        private void OnEnable()
        {
            _work.AddListener(SetWorkValue);
            _control.AddListener(SetControlValue);
            _reflector.AddListener(SetReflectorValue);
            _bisector.AddListener(SetBisectorValue);
            _delay.AddListener(SetDelayValue);
        }

        private void CheckFields()
        {
            switch (_step)
            {
                case 0:
                    if (_workValue)
                    {
                        _step++;
                        _display.ShowMessage(_messages[_step]);
                    }
                    break;
                case 1:
                    if (_controlValue == 1)
                    {
                        _step++;
                        _display.ShowMessage(_messages[_step]);
                    }
                    break;
                case 2:
                    if (_controlValue == 3)
                    {
                        _step++;
                        _display.ShowMessage(_messages[_step]);
                    }
                    break;
            }
        }

        private void SetWorkValue(bool value)
        {
            _workValue = value;
            CheckFields();
        }

        private void SetControlValue(int value)
        {
            _controlValue = value;
            CheckFields();
        }

        private void SetReflectorValue(float value)
        {
            _reflectorValue = value;
            CheckFields();
        }

        private void SetBisectorValue(int value)
        {
            _bisectorValue = value;
            CheckFields();
        }

        private void SetDelayValue(int value)
        {
            _delayValue = value;
            CheckFields();
        }
    }
}
