using System;

namespace Assets.Scripts.Tutorial
{
    class Step<T>
    {
        private string _message;
        private Predicate<T> _action;

        public Predicate<T> Action => _action;
        public string Message => _message;

        public Step(string message, Predicate<T> action)
        {
            _message = message;
            _action = action;
        }
    }
}