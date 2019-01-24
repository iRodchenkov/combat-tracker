using System;

namespace CombatTracker.Core
{
    public class ActionResult<TError>
    {
        private readonly TError _error;

        public ActionResult()
        {
            Ok = true;
        }

        public ActionResult(TError error)
        {
            _error = error;
            Ok = false;
        }

        public TError Error
        {
            get
            {
                if (Ok) throw new InvalidOperationException();
                return _error;
            }
        }

        public bool Ok { get; }

        public ActionResult<TError> OnSuccess(Action action)
        {
            if (Ok) action();
            return this;
        }

        public ActionResult<TError> OnError(Action<TError> action)
        {
            if (!Ok) action(Error);
            return this;
        }
    }
}