using Assets.Scripts.Switches;
using System;
using UnityEngine;

namespace Assets.Scripts.Tutorial
{
    class TurnOn : TutorialBase<TurnOn>
    {
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

        private Step<TurnOn>[] _steps = new Step<TurnOn>[] {
            new Step<TurnOn>("РАБОТА в верхнее положение", (t) => { return t._workValue; }),
            new Step<TurnOn>("КОНТРОЛЬ в положение 6", (t) => { return t._controlValue == 1; }),
            new Step<TurnOn>("КОНТРОЛЬ в положение ТОК 1", (t) => { return t._controlValue == 3; }),
            new Step<TurnOn>("Ручкой ОТРАЖАТЕЛЬ установить стрелку в зеленый сектор", (t) => { return t._reflectorValue > 20; }),
            new Step<TurnOn>("КОНТРОЛЬ в положение ТОК 2", (t) => { return t._controlValue == 4; }),
            new Step<TurnOn>("КОНТРОЛЬ в положение М", (t) => { return t._controlValue == 5; }),
            new Step<TurnOn>("КОНТРОЛЬ в положение 1", (t) => { return t._controlValue == 3; }),
            new Step<TurnOn>("Ручкой ОТРАЖАТЕЛЬ установить стрелку в -3", (t) => { return Mathf.Abs(t._reflectorValue - 20) < 10; }),
            new Step<TurnOn>("КОНТРОЛЬ в положение М", (t) => { return t._controlValue == 5; }),
            new Step<TurnOn>("Ручкой ОТРАЖАТЕЛЬ установить стрелку в зеленый сектор", (t) => { return t._reflectorValue > 20; }),
            new Step<TurnOn>("КОНТРОЛЬ в положение АПЧ", (t) => { return t._controlValue == 2; }),
            new Step<TurnOn>("БИССЕКТРИСА в положение 0", (t) => { return Mathf.Abs(t._bisectorValue - 0) < 2; }),
            new Step<TurnOn>("СЕКТОР на 12", (t) => { return t._sectorValue == 12; }),
            new Step<TurnOn>("ЗАДЕРЖКА в положение 0", (t) => { return t._delayValue == 0; }),
            new Step<TurnOn>("АЗИМУТ на себя до упора", (t) => { return t._azimuthValue == 2; }),
            new Step<TurnOn>("Ручками ЯРКОСТЬ и ФОКУС установить оптимальное изображение линий развертки", (t) => { return t._focusValue == 50 && Mathf.Abs(t._brightnessValue - 40) < 25; }),
            new Step<TurnOn>("Ручками ШИРИНА и <-> установить границы перемещения правой линии развертки от левого края экрана до второй риски справа", (t) => { return Mathf.Abs(t._arrowsValue - 0) < 0.1 && Mathf.Abs(t._widthValue - 2.5f) < 0.2; })
        };

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

        protected override Step<TurnOn>[] Steps => _steps;

        protected override TurnOn This => this;

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
