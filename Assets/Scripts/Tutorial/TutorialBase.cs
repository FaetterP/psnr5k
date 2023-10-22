using UnityEngine;

namespace Assets.Scripts.Tutorial
{
    abstract class TutorialBase<T> : MonoBehaviour
    {
        [SerializeField] private DisplayTutorial _display;

        private int _index;

        protected abstract Step<T>[] Steps { get; }
        protected abstract T This { get; }

        private void Start()
        {
            _display.ShowMessage(Steps[0].Message);
        }

        protected void CheckFields()
        {
            if (_index >= Steps.Length) return;

            if (Steps[_index].Action(This))
            {
                _index++;
                if (_index >= Steps.Length)
                {
                    _display.FinishTutorial();
                    return;
                }
                _display.ShowMessage(Steps[_index].Message);
            }
        }
    }
}