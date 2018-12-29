using System;

namespace Core
{
    public class OperationResult<TValue, TError>
        where TValue: class
    {
        private readonly TValue _value;
        private readonly TError _error;

        public OperationResult(TValue value)
        {
            _value = value;
            Ok = true;
        }

        public OperationResult(TError error)
        {
            _error = error;
            Ok = false;
        }

        public TValue Value
        {
            get
            {
                if (Ok) return _value;
                throw new InvalidOperationException();
            }
        }

        public TError Error {
            get
            {
                if (Ok) throw new InvalidOperationException();
                return _error;
            }
        }

        public bool Ok { get; }

        public OperationResult<TValue, TError> OnSuccess(Action<TValue> action)
        {
            if (Ok) action(Value);
            return this;
        }

        public OperationResult<TValue, TError> OnError(Action<TError> action)
        {
            if (!Ok) action(Error);
            return this;
        }
    }
}
