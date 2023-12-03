using Assets.Scripts.Block;
using Assets.Scripts.Switches;
using Assets.Scripts.Utilities;
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
            new Step<Calibration>("Переключатель \"Задержка\" в положение К.", (ctx) => ctx._delay.Value == 3 ),
            new Step<Calibration>("Ручкой \"-о-\" совместить подвижные метки с неподвижными.", (ctx) => true ),
            new Step<Calibration>("Переключатель \"Задержка\" в положение 0.", (ctx) => ctx._delay.Value == 0 ),
            new Step<Calibration>("Ручкой \"Дальность\" установить на шкале 1000.", (ctx) => ctx._range.Value == 1000 ),
            new Step<Calibration>("Ручкой \"Н\" метку строба на отметку 1000м.", (ctx) => ctx._calibrationN.Value == 0.5 ),
            new Step<Calibration>("Переключатель \"Задержка\" в положение 5.", (ctx) => ctx._delay.Value == 1 ),
            new Step<Calibration>("Ручкой \"Дальность\" установить на шкале 9000.", (ctx) => ctx._range.Value == 9000 ),
            new Step<Calibration>("Ручкой \"_П_\" совместить метку строба с отметкой 9000м.", (ctx) => ctx._calibrationK.Value == 0.5 ),
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
