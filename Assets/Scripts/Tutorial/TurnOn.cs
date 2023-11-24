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
            new Step<TurnOn>("РАБОТА в верхнее положение", (t) => { return t._work.Value; }),
            new Step<TurnOn>("КОНТРОЛЬ в положение 6", (t) => { return t._control.Value == 1; }),
            new Step<TurnOn>("КОНТРОЛЬ в положение ТОК 1", (t) => { return t._control.Value == 3; }),
            new Step<TurnOn>("Ручкой ОТРАЖАТЕЛЬ установить стрелку в зеленый сектор", (t) => { return t._reflector.Value > 20; }),
            new Step<TurnOn>("КОНТРОЛЬ в положение ТОК 2", (t) => { return t._control.Value == 4; }),
            new Step<TurnOn>("КОНТРОЛЬ в положение М", (t) => { return t._control.Value == 5; }),
            new Step<TurnOn>("КОНТРОЛЬ в положение 1", (t) => { return t._control.Value == 3; }),
            new Step<TurnOn>("Ручкой ОТРАЖАТЕЛЬ установить стрелку в -3", (t) => { return Mathf.Abs(t._reflector.Value - 20) < 10; }),
            new Step<TurnOn>("КОНТРОЛЬ в положение М", (t) => { return t._control.Value == 5; }),
            new Step<TurnOn>("Ручкой ОТРАЖАТЕЛЬ установить стрелку в зеленый сектор", (t) => { return t._reflector.Value > 20; }),
            new Step<TurnOn>("КОНТРОЛЬ в положение АПЧ", (t) => { return t._control.Value == 2; }),
            new Step<TurnOn>("БИССЕКТРИСА в положение 0", (t) => { return Mathf.Abs(t._bisector.Value - 0) < 2; }),
            new Step<TurnOn>("СЕКТОР на 12", (t) => { return t._sector.Value == 12; }),
            new Step<TurnOn>("ЗАДЕРЖКА в положение 0", (t) => { return t._delay.Value == 0; }),
            new Step<TurnOn>("АЗИМУТ на себя до упора", (t) => { return t._azimuth.Status == 2; }),
            new Step<TurnOn>("Ручками ЯРКОСТЬ и ФОКУС установить оптимальное изображение линий развертки", (t) => { return t._focus.Value == 50 && Mathf.Abs(t._brightness.Value - 40) < 25; }),
            new Step<TurnOn>("Ручками ШИРИНА и <-> установить границы перемещения правой линии развертки от левого края экрана до второй риски справа", (t) => { return Mathf.Abs(t._arrows.Value - 0) < 0.1 && Mathf.Abs(t._width.Value - 2.5f) < 0.2; })
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
