using Assets.Scripts.Block;
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
        [SerializeField] private Receiver _receiver;

        private Step<TurnOff>[] _steps = new Step<TurnOff>[] {
            new Step<TurnOff>("ручку АЗИМУТ на себя до упора и её вращением установить на шкале АЗИМУТ положение 00-00", (t) => { return t._azimuth.Status == 0; }),
            new Step<TurnOff>("установить переключатель КОНТРОЛЬ в положение ε", (t) => { return t._control.Value == 6; }),
            new Step<TurnOff>("установить переключателем +-ε угол места 0 по шкале прибора КОНТРОЛЬ", (t) => { return Mathf.Abs(t._receiver.CurrentHeight-0)<=10; }),
            new Step<TurnOff>("установить переключатель КОНТРОЛЬ в положение 24", (t) => { return t._control.Value == 0; }),
            new Step<TurnOff>("Ручкой ОТРАЖАТЕЛЬ установить стрелку в зеленый сектор", (t) => { return t._reflector.Value > 20; }),
            new Step<TurnOff>("повернуть ручку ЯРКОСТЬ в крайнее левое положение", (t) => { return t._brightness.Value == 0; }),
            new Step<TurnOff>("установить переключатель РАБОТА в нижнее положение", (t) => { return t._work.Value == false; }),
            new Step<TurnOff>("установить переключатель ЗАДЕРЖКА в положение 0", (t) => { return t._delay.Value == 0; }),
        };

        protected override Step<TurnOff>[] Steps => _steps;

        protected override TurnOff This => this;

        private void OnEnable()
        {
            _work.AddListener(CheckFields);
            _control.AddListener(CheckFields);
            _reflector.AddListener(CheckFields);
            _delay.AddListener(CheckFields);
            _azimuth.AddListener(CheckFields);
            _brightness.AddListener(CheckFields);
        }

        private void OnDisable()
        {
            _work.RemoveListener(CheckFields);
            _control.RemoveListener(CheckFields);
            _reflector.RemoveListener(CheckFields);
            _delay.RemoveListener(CheckFields);
            _azimuth.RemoveListener(CheckFields);
            _brightness.RemoveListener(CheckFields);
        }
    }
}
