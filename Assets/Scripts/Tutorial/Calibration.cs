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
            _delay.AddListener(SetDelayValue);
            _range.AddListener(SetRangeValue);
            _calibrationK.AddListener(SetCalibrationKValue);
            _calibrationN.AddListener(SetCalibrationNValue);
        }

        private void OnDisable()
        {
            _delay.RemoveListener(SetDelayValue);
            _range.RemoveListener(SetRangeValue);
            _calibrationK.RemoveListener(SetCalibrationKValue);
            _calibrationN.RemoveListener(SetCalibrationNValue);
        }

        private void SetCalibrationNValue(float value)
        {
            Debug.Log(_calibrationN.Value);
            CheckFields();
        }

        private void SetCalibrationKValue(float value)
        {
            CheckFields();
        }


        private void SetDelayValue(int value)
        {
            CheckFields();
        }

        private void SetRangeValue(float value)
        {
            CheckFields();
        }
    }
}
