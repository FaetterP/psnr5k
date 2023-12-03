using Assets.Scripts.Block;
using Assets.Scripts.Switches;
using Assets.Scripts.Utilities;
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
            new Step<TurnOff>("Ручку \"Азимут\" на себя до упора и её вращением установить на шкале положение 00|00.", (ctx) => ctx._azimuth.Status == Azimuth.Mode.Sector && Math.Abs(ctx._azimuth.HandleRotate.Value - 40.5) < 0.2 ),
            new Step<TurnOff>("Установить переключатель \"Контроль\" в положение ε.", (ctx) => ctx._control.Value == 6 ), // TODO добавить статус
            new Step<TurnOff>("Установить переключателем +ε/-ε угол места 0 по шкале прибора \"Контроль\".", (ctx) => Mathf.Abs(ctx._receiver.CurrentHeight - 0) <= 10),
            new Step<TurnOff>("Установить переключатель \"Контроль\" в положение 24.", (ctx) => ctx._control.Value == 0),
            new Step<TurnOff>("Ручкой \"отражатель\" установить стрелку в зеленый сектор.", (ctx) => ctx._reflector.Value > 20),
            new Step<TurnOff>("Повернуть ручку \"Яркость\" в крайнее левое положение.", (ctx) => ctx._brightness.Value == 0),
            new Step<TurnOff>("Установить переключатель \"Работа\" в нижнее положение.", (ctx) => ctx._work.Value == false),
            new Step<TurnOff>("Установить переключатель \"Задержка\" в положение 0.", (ctx) => ctx._delay.Value == 0),
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
