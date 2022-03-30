using System;
using System.Collections.Generic;

namespace DynamicObjects
{
    internal sealed class EventBus
    {
        private readonly Dictionary<string, Event> eventMap;

        private readonly Dictionary<Key, object> eventMap1;

        internal EventBus()
        {
            this.eventMap = new Dictionary<string, Event>();
            this.eventMap1 = new Dictionary<Key, object>();
        }

        internal void AddEvent(string name)
        {
            if (!this.eventMap.ContainsKey(name))
            {
                this.eventMap.Add(name, new Event());
            }
            else
            {
                throw new Exception($"Event {name}() is already added");
            }
        }

        internal void AddEvent<T>(string name)
        {
            var key = new Key(name, typeof(T));
            if (!this.eventMap1.ContainsKey(key))
            {
                this.eventMap1.Add(key, new Event<T>());
            }
            else
            {
                throw new Exception($"Event {name}({typeof(T).Name}) is already added");
            }
        }

        internal void RemoveEvent(string name)
        {
            this.eventMap.Remove(name);
        }

        internal void RemoveEvent<T>(string name)
        {
            var key = new Key(name, typeof(T));
            this.eventMap1.Remove(key);
        }

        internal void AddListener(string name, Action callback)
        {
            if (!this.eventMap.TryGetValue(name, out var @event))
            {
                @event = new Event();
                this.eventMap.Add(name, @event);
            }

            @event.OnEvent += callback;
        }
        
        internal void AddListener<T>(string name, Action<T> callback)
        {
            var key = new Key(name, typeof(T));

            Event<T> @event;
            if (!this.eventMap1.TryGetValue(key, out var value))
            {
                @event = new Event<T>();
                this.eventMap1.Add(key, @event);
            }
            else
            {
                @event = (Event<T>) value;
            }

            @event.OnEvent += callback;
        }

        internal void RemoveListener(string name, Action callback)
        {
            if (this.eventMap.TryGetValue(name, out var @event))
            {
                @event.OnEvent -= callback;
            }
        }

        internal void RemoveListener<T>(string name, Action<T> callback)
        {
            var key = new Key(name, typeof(T));
            if (!this.eventMap1.TryGetValue(key, out var value))
            {
                return;
            }

            var @event = (Event<T>) value;
            @event.OnEvent -= callback;
        }

        internal void InvokeEvent(string name)
        {
            if (this.eventMap.TryGetValue(name, out var @event))
            {
                @event.Invoke();
            }
        }

        internal void InvokeEvent<T>(string name, T data)
        {
            var key = new Key(name, typeof(T));
            if (!this.eventMap1.TryGetValue(key, out var value))
            {
                return;
            }

            var @event = (Event<T>) value;
            @event.Invoke(data);
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