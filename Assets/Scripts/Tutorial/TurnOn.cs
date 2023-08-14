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
        [SerializeField] private HandleRotateLimitation _sector;
        [SerializeField] private HandleStep _delay;
        [SerializeField] private Azimuth _azimuth;
        [SerializeField] private HandleRotate _brightness;
        [SerializeField] private HandleRotate _focus;
        [SerializeField] private HandleRotate _arrows;
        [SerializeField] private HandleRotate _width;

        private int _step;
        private string[] _messages = new string[] {
            "РАБОТА в верхнее положение",
            "КОНТРОЛЬ в положение 6",
            "КОНТРОЛЬ в положение ТОК 1",
            "Ручкой ОТРАЖАТЕЛЬ установить стрелку в зеленый сектор",
            "КОНТРОЛЬ в положение ТОК 2",
            "КОНТРОЛЬ в положение М",
            "КОНТРОЛЬ в положение 1",
            "Ручкой ОТРАЖАТЕЛЬ установить стрелку в -3",
            "КОНТРОЛЬ в положение М",
            "Ручкой ОТРАЖАТЕЛЬ установить стрелку в зеленый сектор",
            "КОНТРОЛЬ в положение АПЧ",
            "БИССЕКТРИСА в положение 0",
            "СЕКТОР на 12",
            "ЗАДЕРЖКА в положение 0",
            "АЗИМУТ на себя до упора",
            "Ручками ЯРКОСТЬ и ФОКУС установить оптимальное изображение линий развертки",
            "Ручками ШИРИНА и <-> установить границы перемещения правой линии развертки от левого края экрана до второй риски справа" };

        private bool _workValue;
        private int _controlValue;
        private float _reflectorValue;
        private int _bisectorValue;
        private int _sectorValue;
        private int _delayValue;

        private int _azimuthValue;
        private float _brightnessValue;
        private float _focusValue;
        private float _widthValue;
        private float _arrowsValue;

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
            _sector.AddListener(SetSectorValue);
            _delay.AddListener(SetDelayValue);
            _azimuth.AddListener(SetAzimuthValue);
            _brightness.AddListener(SetBrightnessValue);
            _focus.AddListener(SetFocusValue);
            _arrows.AddListener(SetArrowsValue);
            _width.AddListener(SetWidthValue);
        }
        
        private void OnDisable()
        {
            _work.RemoveListener(SetWorkValue);
            _control.RemoveListener(SetControlValue);
            _reflector.RemoveListener(SetReflectorValue);
            _bisector.RemoveListener(SetBisectorValue);
            _sector.RemoveListener(SetSectorValue);
            _delay.RemoveListener(SetDelayValue);
            _azimuth.RemoveListener(SetAzimuthValue);
            _brightness.RemoveListener(SetBrightnessValue);
            _focus.RemoveListener(SetFocusValue);
            _arrows.RemoveListener(SetArrowsValue);
            _width.RemoveListener(SetWidthValue);
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
                case 3:
                    if (_reflectorValue > 20)
                    {
                        _step++;
                        _display.ShowMessage(_messages[_step]);
                    }
                    break;
                case 4:
                    if (_controlValue == 4)
                    {
                        _step++;
                        _display.ShowMessage(_messages[_step]);
                    }
                    break;
                case 5:
                    if (_controlValue == 5)
                    {
                        _step++;
                        _display.ShowMessage(_messages[_step]);
                    }
                    break;
                case 6:
                    if (_controlValue == 3)
                    {
                        _step++;
                        _display.ShowMessage(_messages[_step]);
                    }
                    break;
                case 7:
                    if (Mathf.Abs(_reflectorValue - 20) < 10)
                    {
                        _step++;
                        _display.ShowMessage(_messages[_step]);
                    }
                    break;
                case 8:
                    if (_controlValue == 5)
                    {
                        _step++;
                        _display.ShowMessage(_messages[_step]);
                    }
                    break;
                case 9:
                    if (_reflectorValue > 20)
                    {
                        _step++;
                        _display.ShowMessage(_messages[_step]);
                    }
                    break;
                case 10:
                    if (_controlValue == 2)
                    {
                        _step++;
                        _display.ShowMessage(_messages[_step]);
                    }
                    break;
                case 11:
                    if (Mathf.Abs(_bisectorValue - 0) < 2)
                    {
                        _step++;
                        _display.ShowMessage(_messages[_step]);
                    }
                    break;
                case 12:
                    if (_sectorValue == 12)
                    {
                        _step++;
                        _display.ShowMessage(_messages[_step]);
                    }
                    break;
                case 13:
                    if (_delayValue == 0)
                    {
                        _step++;
                        _display.ShowMessage(_messages[_step]);
                    }
                    break;
                case 14:
                    if (_azimuthValue == 2)
                    {
                        _step++;
                        _display.ShowMessage(_messages[_step]);
                    }
                    break;
                case 15:
                    if (_focusValue == 50 && Mathf.Abs(_brightnessValue - 40) < 25)
                    {
                        _step++;
                        _display.ShowMessage(_messages[_step]);
                    }
                    break;
                case 16:
                    if (Mathf.Abs(_arrowsValue - 0) < 0.1 && Mathf.Abs(_widthValue - 2.5f) < 0.2)
                    {
                        _display.FinishTutorial();
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
        
        private void SetSectorValue(int value)
        {
            _sectorValue = value;
            CheckFields();
        }

        private void SetDelayValue(int value)
        {
            _delayValue = value;
            CheckFields();
        }
        
        private void SetAzimuthValue(int value)
        {
            _azimuthValue = value;
            CheckFields();
        }
        
        private void SetBrightnessValue(float value)
        {
            _brightnessValue = value;
            CheckFields();
        }
        
        private void SetFocusValue(float value)
        {
            _focusValue = value;
            CheckFields();
        }
        
        private void SetWidthValue(float value)
        {
            _widthValue = value;
            CheckFields();
        }
        
        private void SetArrowsValue(float value)
        {
            _arrowsValue = value;
            CheckFields();
        }
    }
}
