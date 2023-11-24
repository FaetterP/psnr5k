using Assets.Scripts.Block;
using Assets.Scripts.Switches;
using UnityEngine;

namespace Assets.Scripts.Tutorial
{
    class Calibration : TutorialBase<Calibration>
    {
        [SerializeField] private HandleStep _delay;
        [SerializeField] private HandleRotate _range;
        [SerializeField] private HandleRotate _calibrationN;
        [SerializeField] private HandleRotate _calibrationK;

        private Step<Calibration>[] _steps = new Step<Calibration>[] {
            new Step<Calibration>("Переключатель ЗАДЕРЖКА в положение К", (t) => { return t._delay.Value == 3; }),
            new Step<Calibration>("Ручкой -о- совместить подвижные метки с неподвижными", (t) => { return true; }),
            new Step<Calibration>("ЗАДЕРЖКА в положение 0", (t) => { return t._delay.Value==0; }),
            new Step<Calibration>("Ручкой ДАЛЬНОСТЬ установить на шкале ДАЛЬНОСТЬ 1000", (t) => { return t._range.Value == 1000; }),
            new Step<Calibration>("Ручкой Н метку строба на первую 1000м отметку", (t) => { return t._calibrationN.Value==0; }),
            new Step<Calibration>("ЗАДЕРЖКА в положение 5", (t) => { return t._delay.Value == 1; }),
            new Step<Calibration>("Ручкой ДАЛЬНОСТЬ установить на шкале ДАЛЬНОСТЬ 9000", (t) => { return t._range.Value == 9000; }),
            new Step<Calibration>("Ручкой К совместить метку строба с 9000м отметкой", (t) => { return t._calibrationK.Value == 0; }),
        };

        protected override Step<Calibration>[] Steps => _steps;

        protected override Calibration This => this;

        private void OnEnable()
        {
            _delay.AddListener(CheckFields);
            _range.AddListener(CheckFields);
            _calibrationK.AddListener(CheckFields);
            _calibrationN.AddListener(CheckFields);
        }

        private void OnDisable()
        {
            _delay.RemoveListener(CheckFields);
            _range.RemoveListener(CheckFields);
            _calibrationK.RemoveListener(CheckFields);
            _calibrationN.RemoveListener(CheckFields);
        }
    }
}
