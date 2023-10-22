using Assets.Scripts.Switches;
using System;
using UnityEngine;

namespace Assets.Scripts.Tutorial
{
    class TurnOff : TutorialBase<TurnOff>
    {

        [SerializeField] private Lever _work;
        [SerializeField] private HandleStep _control;
        [SerializeField] private HandleRotate _reflector;
        [SerializeField] private HandleStep _delay;
        [SerializeField] private Azimuth _azimuth;
        [SerializeField] private HandleRotate _brightness;

        private Step<TurnOff>[] _steps = new Step<TurnOff>[] {
            new Step<TurnOff>("ручку АЗИМУТ на себя до упора и её вращением установить на шкале АЗИМУТ положение 00-00", (t) => { return t._azimuthValue == 0; }),
            new Step<TurnOff>("установить переключатель КОНТРОЛЬ в положение ε", (t) => { return t._controlValue == 6; }),
            new Step<TurnOff>("установить переключателем +-ε угол места 0 по шкале прибора КОНТРОЛЬ", (t) => { return t._controlValue == 1; }),
            new Step<TurnOff>("установить переключатель КОНТРОЛЬ в положение 24", (t) => { return t._controlValue == 3; }),
            new Step<TurnOff>("Ручкой ОТРАЖАТЕЛЬ установить стрелку в зеленый сектор", (t) => { return t._reflectorValue > 20; }),
            new Step<TurnOff>("повернуть ручку ЯРКОСТЬ в крайнее левое положение", (t) => { return t._brightnessValue == 0; }),
            new Step<TurnOff>("установить переключатель РАБОТА в нижнее положение", (t) => { return t._workValue == false; }),
            new Step<TurnOff>("установить переключатель ЗАДЕРЖКА в положение 0", (t) => { return t._delayValue == 0; }),
        };

        private bool _workValue;
        private int _controlValue;
        private float _reflectorValue;
        private int _delayValue;
        private int _azimuthValue;
        private float _brightnessValue;

        protected override Step<TurnOff>[] Steps => _steps;

        protected override TurnOff This => this;

        private void OnEnable()
        {
            _work.AddListener(SetWorkValue);
            _control.AddListener(SetControlValue);
            _reflector.AddListener(SetReflectorValue);
            _delay.AddListener(SetDelayValue);
            _azimuth.AddListener(SetAzimuthValue);
            _brightness.AddListener(SetBrightnessValue);
        }

        private void OnDisable()
        {
            _work.RemoveListener(SetWorkValue);
            _control.RemoveListener(SetControlValue);
            _reflector.RemoveListener(SetReflectorValue);
            _delay.RemoveListener(SetDelayValue);
            _azimuth.RemoveListener(SetAzimuthValue);
            _brightness.RemoveListener(SetBrightnessValue);
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
    }
}
