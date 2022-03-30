using System;
using System.Collections.Generic;

namespace DynamicObjects
{
    internal sealed class EventBus
    {
        private readonly Dictionary<EventName, Event> eventMap;

        private readonly Dictionary<EventKey, object> eventMap1;

        internal EventBus()
        {
            this.eventMap = new Dictionary<EventName, Event>();
            this.eventMap1 = new Dictionary<EventKey, object>();
        }

        internal void CreateEvent(EventName name)
        {
            if (!this.eventMap.ContainsKey(name))
            {
                this.eventMap.Add(name, new Event());
            }
        }

        internal void AddListener(EventName name, Action callback)
        {
            if (!this.eventMap.TryGetValue(name, out var @event))
            {
                @event = new Event();
                this.eventMap.Add(name, @event);
            }

            @event.OnEvent += callback;
        }

        internal void CreateEvent<T>(EventName name)
        {
            var key = new EventKey(name, typeof(T));
            if (!this.eventMap1.ContainsKey(key))
            {
                this.eventMap1.Add(key, new Event<T>());
            }
        }

        internal void AddListener<T>(EventName name, Action<T> callback)
        {
            var key = new EventKey(name, typeof(T));
            
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

        internal void RemoveListener(EventName name, Action callback)
        {
            if (this.eventMap.TryGetValue(name, out var @event))
            {
                @event.OnEvent -= callback;
            }
        }

        internal void RemoveListener<T>(EventName name, Action<T> callback)
        {
            var key = new EventKey(name, typeof(T));
            if (!this.eventMap1.TryGetValue(key, out var value))
            {
                return;
            }

            var @event = (Event<T>) value;
            @event.OnEvent -= callback;
        }

        internal void InvokeEvent(EventName name)
        {
            if (this.eventMap.TryGetValue(name, out var @event))
            {
                @event.Invoke();
            }
        }

        internal void InvokeEvent<T>(EventName name, T data)
        {
            var key = new EventKey(name, typeof(T));
            if (!this.eventMap1.TryGetValue(key, out var value))
            {
                return;
            }

            var @event = (Event<T>) value;
            @event.Invoke(data);
        }
    }
}