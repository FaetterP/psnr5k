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
            new Step<TurnOn>("Установить переключатель \"Работа\" в верхнее положение.", (ctx) => ctx._work.Value ),
            new Step<TurnOn>("Установить переключатель \"Контроль\" в положение 6.", (ctx) => ctx._control.Value == 1 ),
            new Step<TurnOn>("Установить переключатель \"Контроль\" в положение \"Ток\" 1.", (ctx) => ctx._control.Value == 3 ),
            new Step<TurnOn>("Ручкой \"Отражатель\" установить стрелку в зеленый сектор.", (ctx) => ctx._reflector.Value > 0.2 ),
            new Step<TurnOn>("Установить переключатель \"Контроль\" в положение \"Ток 2\".", (ctx) => ctx._control.Value == 4 ),
            new Step<TurnOn>("Установить переключатель \"Контроль\" в положение \"Ток М\".", (ctx) => ctx._control.Value == 5 ),
            new Step<TurnOn>("Установить переключатель \"Контроль\" в положение \"Ток 1\".", (ctx) => ctx._control.Value == 3 ),
            new Step<TurnOn>("Ручкой \"Отражатель\" установить стрелку в положение -3.", (ctx) => Mathf.Abs(ctx._reflector.Value - 20) < 10 ),
            new Step<TurnOn>("Установить переключатель \"Контроль\" в положение \"Ток М\".", (ctx) => ctx._control.Value == 5 ),
            new Step<TurnOn>("Ручкой \"Отражатель\" установить стрелку в зеленый сектор.", (ctx) => ctx._reflector.Value > 20 ),
            new Step<TurnOn>("Установить переключатель \"Контроль\" в положение \"Напряжение АПЧ\".", (ctx) => ctx._control.Value == 2 ),
            new Step<TurnOn>("Установить переключатель \"Биссектриса\" в положение 0.", (ctx) => Mathf.Abs(ctx._bisector.Value - 0) < 2 ),
            new Step<TurnOn>("Установить \"Сектор\" на 12.", (ctx) => ctx._sector.Value == 12 ),
            new Step<TurnOn>("Переключатель \"Задержка\" в положение 0.", (ctx) => ctx._delay.Value == 0 ),
            new Step<TurnOn>("Оттянуть \"Азимут\" на себя до упора.", (ctx) => ctx._azimuth.Status == Azimuth.Mode.Sector ),
            new Step<TurnOn>("Ручками \"Яркость\" и \"Фокус\" установить оптимальное изображение линий развертки.", (ctx) => Math.Abs(ctx._focus.Value-0.5)<0.01 && Mathf.Abs(ctx._brightness.Value - 0.85f) < 0.2 ),
            new Step<TurnOn>("Ручками \"Ширина\" и \"←→\" установить границы перемещения правой линии развертки от левого края экрана до второй чёрточки справа.", (ctx) => Mathf.Abs(ctx._arrows.Value - 0.23f) < 0.02 && Mathf.Abs(ctx._width.Value - 0.58f) < 0.02 )
        };

        protected override Step<TurnOn>[] Steps => _steps;

        protected override TurnOn This => this;

        private void OnEnable()
        {
            _work.AddListener(CheckFields);
            _control.AddListener(CheckFields);
            _reflector.AddListener(CheckFields);
            _bisector.AddListener(CheckFields);
            _sector.AddListener(CheckFields);
            _delay.AddListener(CheckFields);
            _azimuth.AddListener(CheckFields);
            _brightness.AddListener(CheckFields);
            _focus.AddListener(CheckFields);
            _arrows.AddListener(CheckFields);
            _width.AddListener(CheckFields);
        }

        private void OnDisable()
        {
            _work.RemoveListener(CheckFields);
            _control.RemoveListener(CheckFields);
            _reflector.RemoveListener(CheckFields);
            _bisector.RemoveListener(CheckFields);
            _sector.RemoveListener(CheckFields);
            _delay.RemoveListener(CheckFields);
            _azimuth.RemoveListener(CheckFields);
            _brightness.RemoveListener(CheckFields);
            _focus.RemoveListener(CheckFields);
            _arrows.RemoveListener(CheckFields);
            _width.RemoveListener(CheckFields);
        }
    }
}
