using System;

namespace DynamicObjects
{
    internal sealed class Event
    {
        internal event Action OnEvent;

        internal void Invoke()
        {
            this.OnEvent?.Invoke();
        }
    }
    
    internal sealed class Event<T>
    {
        internal event Action<T> OnEvent;

        internal void Invoke(T data)
        {
            this.OnEvent?.Invoke(data);
        }
    }
}