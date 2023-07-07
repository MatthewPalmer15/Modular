namespace Modular.Core
{
    [Serializable]
    public class ModularBindableBase
    {
        [NonSerialized]
        private EventHandler _NonSerializableHandlers = delegate { };

        [NonSerialized]
        private EventHandler _SerializableHandlers = delegate { };

        public event EventHandler IsDirtyChanged
        {
            add
            {
                if (value.Method.IsPublic && value.Method.DeclaringType != null && (value.Method.DeclaringType.IsSerializable || value.Method.IsStatic))
                {
                    _SerializableHandlers = (EventHandler)Delegate.Combine(_SerializableHandlers, value);
                }
                else
                {
                    _NonSerializableHandlers = (EventHandler)Delegate.Combine(_NonSerializableHandlers, value);
                }
            }
            remove
            {
                if (value.Method.IsPublic && value.Method.DeclaringType != null && (value.Method.DeclaringType.IsSerializable || value.Method.IsStatic))
                {
                    if (Delegate.Remove(_SerializableHandlers, value) is EventHandler RemovedHandler)
                    {
                        _SerializableHandlers = RemovedHandler;
                    }
                }
                else
                {
                    if (Delegate.Remove(_NonSerializableHandlers, value) is EventHandler RemovedHandler)
                    {
                        _NonSerializableHandlers = RemovedHandler;
                    }
                }
            }
        }

        protected virtual void OnIsDirtyChanged()
        {
            EventHandler nonSerializableHandlers = _NonSerializableHandlers;
            EventHandler serializableHandlers = _SerializableHandlers;

            nonSerializableHandlers?.Invoke(this, EventArgs.Empty);
            serializableHandlers?.Invoke(this, EventArgs.Empty);
        }
    }

}
