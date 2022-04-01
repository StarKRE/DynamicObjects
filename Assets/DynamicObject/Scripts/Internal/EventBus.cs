using System;
using System.Collections.Generic;

namespace DynamicObjects
{
    internal sealed class EventBus
    {
        private Dictionary<string, Event> _eventMap1;

        private Dictionary<Key, object> _eventMap2;

        private Dictionary<string, Event> eventMap1
        {
            get
            {
                if (_eventMap1 == null)
                {
                    _eventMap1 = new Dictionary<string, Event>();
                }

                return _eventMap1;
            }
        }

        private Dictionary<Key, object> eventMap2
        {
            get
            {
                if (_eventMap2 == null)
                {
                    _eventMap2 = new Dictionary<Key, object>();
                }

                return _eventMap2;
            }
        }
        
        internal void DefineEvent(string name)
        {
            if (!this.eventMap1.ContainsKey(name))
            {
                this.eventMap1.Add(name, new Event());
            }
        }

        internal void DefineEvent<T>(string name)
        {
            var key = new Key(name, typeof(T));
            if (!this.eventMap2.ContainsKey(key))
            {
                this.eventMap2.Add(key, new Event<T>());
            }
        }

        internal void DisposeEvent(string name)
        {
            this.eventMap1.Remove(name);
        }

        internal void DisposeEvent<T>(string name)
        {
            var key = new Key(name, typeof(T));
            this.eventMap2.Remove(key);
        }
        
        internal void AddListener(string name, Action callback)
        {
            if (!this.eventMap1.TryGetValue(name, out var @event))
            {
                @event = new Event();
                this.eventMap1.Add(name, @event);
            }

            @event.OnEvent += callback;
        }

        internal void AddListener<T>(string name, Action<T> callback)
        {
            var key = new Key(name, typeof(T));

            Event<T> @event;
            if (!this.eventMap2.TryGetValue(key, out var value))
            {
                @event = new Event<T>();
                this.eventMap2.Add(key, @event);
            }
            else
            {
                @event = (Event<T>) value;
            }

            @event.OnEvent += callback;
        }

        internal void RemoveListener(string name, Action callback)
        {
            if (this.eventMap1.TryGetValue(name, out var @event))
            {
                @event.OnEvent -= callback;
            }
        }

        internal void RemoveListener<T>(string name, Action<T> callback)
        {
            var key = new Key(name, typeof(T));
            if (!this.eventMap2.TryGetValue(key, out var value))
            {
                return;
            }

            var @event = (Event<T>) value;
            @event.OnEvent -= callback;
        }

        internal void InvokeEvent(string name)
        {
            if (this.eventMap1.TryGetValue(name, out var @event))
            {
                @event.Invoke();
            }
        }

        internal void InvokeEvent<T>(string name, T data)
        {
            var key = new Key(name, typeof(T));
            if (this.eventMap2.TryGetValue(key, out var value))
            {
                var @event = (Event<T>) value;
                @event.Invoke(data);
            }
        }

        ///Key
        private readonly struct Key
        {
            private readonly string name;

            private readonly Type type;

            public Key(string name, Type type)
            {
                this.name = name;
                this.type = type;
            }

            public override bool Equals(object obj)
            {
                return obj is Key other && Equals(other);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((name != null ? name.GetHashCode() : 0) * 397) ^ (type != null ? type.GetHashCode() : 0);
                }
            }

            private bool Equals(Key other)
            {
                return name == other.name && type == other.type;
            }
        }

        ///Events
        private sealed class Event
        {
            internal event Action OnEvent;

            internal void Invoke()
            {
                this.OnEvent?.Invoke();
            }
        }

        private sealed class Event<T>
        {
            internal event Action<T> OnEvent;

            internal void Invoke(T data)
            {
                this.OnEvent?.Invoke(data);
            }
        }
    }
}